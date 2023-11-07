using MultiPrecision;
using static EulerQApproximation.Limit;

namespace EulerQApproximation {
    internal class Limit {
        static void Main() {
            using StreamWriter sw = new("../../../../results/euler_q_n32_limit.csv");
            sw.WriteLine("x,y:=euler_q(x),lny:=ln(y),v:=lny*(1-x^2),extrapolation_limit(v),extrapolation_error(v)");

            RichardsonExtrapolation<Pow2.N8> limit_neg = new(), limit_pos = new();

            for (MultiPrecision<Pow2.N8> dx = 1 / 64d; dx.Exponent >= -25; dx /= 2) {
                MultiPrecision<Pow2.N8> x = -1 + dx;
                MultiPrecision<Pow2.N8> y = EulerQ<Pow2.N8>.Value(x);
                MultiPrecision<Pow2.N8> lny = MultiPrecision<Pow2.N8>.Log(y);
                MultiPrecision<Pow2.N8> v = lny * (1 - x * x);

                limit_neg.Inject(v);

                if (dx > 1 / 256d) {
                    continue;
                }

                MultiPrecision<Pow2.N8>[] series = limit_neg.Series.ToArray();
                MultiPrecision<Pow2.N8> v_ext = 2 * series[^1] - series[^2];

                sw.WriteLine($"-1+2^{dx.Exponent},{y},{lny},{v},{v_ext},{limit_neg.Epsilon:e16}");
                Console.WriteLine($"-1+2^{dx.Exponent}\n{y:e20}\n{lny:e20}\n{v:e20}\n{v_ext:e20}\n{limit_neg.Epsilon:e4}");
                sw.Flush();
            }

            for (MultiPrecision<Pow2.N8> dx = 1 / 64d; dx.Exponent >= -25; dx /= 2) {
                MultiPrecision<Pow2.N8> x = 1 - dx;
                MultiPrecision<Pow2.N8> y = EulerQ<Pow2.N8>.Value(x);
                MultiPrecision<Pow2.N8> lny = MultiPrecision<Pow2.N8>.Log(y);
                MultiPrecision<Pow2.N8> v = lny * (1 - x * x);

                limit_pos.Inject(v);

                if (dx > 1 / 256d) {
                    continue;
                }

                MultiPrecision<Pow2.N8>[] series = limit_pos.Series.ToArray();
                MultiPrecision<Pow2.N8> v_ext = 2 * series[^1] - series[^2];

                sw.WriteLine($"1-2^{dx.Exponent},{y},{lny},{v},{v_ext},{limit_pos.Epsilon:e16}");
                Console.WriteLine($"1-2^{dx.Exponent}\n{y:e20}\n{lny:e20}\n{v:e20}\n{v_ext:e20}\n{limit_pos.Epsilon:e4}");
                sw.Flush();
            }

            sw.Close();

            Console.WriteLine("END");
            Console.Read();
        }

        internal class RichardsonExtrapolation<N> where N : struct, IConstant {
            static readonly List<MultiPrecision<N>> rs = new() { MultiPrecision<N>.NaN };
            readonly List<MultiPrecision<N>[]> values = new();

            public void Inject(MultiPrecision<N> new_value) {
                if (SeriesCount <= 0) {
                    values.Add(new MultiPrecision<N>[] { new_value });
                    return;
                }

                MultiPrecision<N>[] t = values[SeriesCount - 1], t_next = new MultiPrecision<N>[SeriesCount + 1];

                t_next[0] = new_value;

                for (int i = 1; i <= SeriesCount; i++) {
                    t_next[i] = t_next[i - 1] + (t_next[i - 1] - t[i - 1]) * R(i);
                }

                values.Add(t_next);
            }

            private static MultiPrecision<N> R(int i) {
                for (int k = rs.Count; k <= i; k++) {
                    MultiPrecision<N> r = 1d / (MultiPrecision<N>.Ldexp(1d, k * 2) - 1);

                    rs.Add(r);
                }

                return rs[i];
            }

            public IEnumerable<MultiPrecision<N>> Series {
                get {
                    for (int i = 0; i < values.Count; i++) {
                        yield return values[i][i];
                    }
                }
            }

            public MultiPrecision<N> ConvergenceValue {
                get {
                    if (SeriesCount <= 0) {
                        throw new InvalidOperationException();
                    }

                    return values[SeriesCount - 1][SeriesCount - 1];
                }
            }

            public int SeriesCount => values.Count;

            public MultiPrecision<N> Epsilon {
                get {
                    if (SeriesCount <= 1) {
                        throw new InvalidOperationException();
                    }

                    return MultiPrecision<N>.Abs(values[SeriesCount - 1][SeriesCount - 1] - values[SeriesCount - 2][SeriesCount - 2]);
                }
            }
        }
    }
}