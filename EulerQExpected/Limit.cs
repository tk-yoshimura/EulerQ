using MultiPrecision;
using MultiPrecisionSeriesAcceleration;

namespace EulerQExpected {
    internal class Limit {
        static void Main() {
            using StreamWriter sw = new("../../../../results/euler_q_n8_limit.csv");
            sw.WriteLine("x,y:=euler_q(x),lny:=ln(y),v:=lny*(1-x^2),extrapolation_limit(v),extrapolation_error(v)");

            SteffiensenIterativeKAutoAccelerator<Pow2.N8> limit_neg = new(), limit_pos = new();

            for (MultiPrecision<Pow2.N8> dx = 1 / 64d; dx.Exponent >= -25; dx /= 2) {
                MultiPrecision<Pow2.N8> x = -1 + dx;
                MultiPrecision<Pow2.N8> y = EulerQ<Pow2.N8>.Value(x);
                MultiPrecision<Pow2.N8> lny = MultiPrecision<Pow2.N8>.Log(y);
                MultiPrecision<Pow2.N8> v = lny * (1 - x * x);

                limit_neg.Append(v);

                if (dx > 1 / 256d) {
                    continue;
                }

                sw.WriteLine($"-1+2^{dx.Exponent},{y},{lny},{v},{limit_neg.LastValue},{limit_neg.Error:e16}");
                Console.WriteLine($"-1+2^{dx.Exponent}\n{y:e20}\n{lny:e20}\n{v:e20}\n{limit_neg.LastValue:e20}\n{limit_neg.Error:e4}");
                sw.Flush();
            }

            for (MultiPrecision<Pow2.N8> dx = 1 / 64d; dx.Exponent >= -25; dx /= 2) {
                MultiPrecision<Pow2.N8> x = 1 - dx;
                MultiPrecision<Pow2.N8> y = EulerQ<Pow2.N8>.Value(x);
                MultiPrecision<Pow2.N8> lny = MultiPrecision<Pow2.N8>.Log(y);
                MultiPrecision<Pow2.N8> v = lny * (1 - x * x);

                limit_pos.Append(v);

                if (dx > 1 / 256d) {
                    continue;
                }

                sw.WriteLine($"1-2^{dx.Exponent},{y},{lny},{v},{limit_pos.LastValue},{limit_pos.Error:e16}");
                Console.WriteLine($"1-2^{dx.Exponent}\n{y:e20}\n{lny:e20}\n{v:e20}\n{limit_pos.LastValue:e20}\n{limit_pos.Error:e4}");
                sw.Flush();
            }

            sw.Close();

            Console.WriteLine("END");
            Console.Read();
        }
    }
}