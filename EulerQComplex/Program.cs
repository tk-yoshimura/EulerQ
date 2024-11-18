using MultiPrecision;
using MultiPrecisionComplex;

namespace EulerQComplex {
    internal class Program {
        static void Main() {
            List<MultiPrecision<Pow2.N4>> norms = new();
            for (MultiPrecision<Pow2.N4> norm = 0; norm < 1 / 32d; norm += 1 / 1024d) {
                norms.Add(norm);
            }
            for (MultiPrecision<Pow2.N4> n = 1 / 1024d; n < 1; n += 1 / 2048d) {
                MultiPrecision<Pow2.N4> norm = MultiPrecision<Pow2.N4>.Sqrt(n);

                norms.Add(norm);
            }

            using StreamWriter sw = new("../../../../results_disused/euler_q_complex_n4.csv");
            sw.WriteLine("# q:=norm ( cos(pi theta) + i sin(pi theta) )");
            sw.WriteLine("theta,norm,y:=euler_q(q),v:=ln(y)*(1-q^2)");

            for (MultiPrecision<Pow2.N4> theta = 0; theta < 2; theta += 1 / 2048d) {
                Complex<Pow2.N4>[] vs = new Complex<Pow2.N4>[norms.Count];

                Complex<Pow2.N4> p = (MultiPrecision<Pow2.N4>.CosPi(theta), MultiPrecision<Pow2.N4>.SinPi(theta));

                int log_k = 0;

                int idx_norm = 0;
                foreach (MultiPrecision<Pow2.N4> norm in norms) {
                    Complex<Pow2.N4> q = p * norm;
                    Complex<Pow2.N4> y = EulerQ<Pow2.N4>.Value(q);
                    Complex<Pow2.N4> lny = Complex<Pow2.N4>.Log(y);

                    // Search p.v. log
                    if (norm > 0.5 && idx_norm >= 2 && theta != 0 && theta != 1) {
                        Complex<Pow2.N4> prev_prev_norm_v = vs[idx_norm - 2];
                        Complex<Pow2.N4> prev_norm_v = vs[idx_norm - 1];

                        Complex<Pow2.N4> assume_v = prev_norm_v + (prev_norm_v - prev_prev_norm_v) * 0.75;

                        int min_log_dk = 0;
                        MultiPrecision<Pow2.N4> min_abs = MultiPrecision<Pow2.N4>.PositiveInfinity;
                        for (int log_dk = log_k - 16; log_dk <= log_k + 16; log_dk++) {
                            Complex<Pow2.N4> v = (lny + (0, 2 * log_dk * MultiPrecision<Pow2.N4>.Pi)) * (1 - q * q);
                            MultiPrecision<Pow2.N4> abs = (assume_v - v).Magnitude;

                            if (abs < min_abs) {
                                min_abs = abs;
                                min_log_dk = log_dk;
                            }
                        }

                        log_k = min_log_dk;
                    }

                    {
                        Complex<Pow2.N4> v = (lny + (0, 2 * log_k * MultiPrecision<Pow2.N4>.Pi)) * (1 - q * q);

                        sw.WriteLine($"{theta},{norm},{y},{v}");
                        Console.WriteLine($"{theta}\n{norm}\n{v:e20}\n{log_k}");

                        vs[idx_norm] = v;
                    }

                    idx_norm++;
                }

                sw.Flush();
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}