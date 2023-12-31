﻿using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionCurveFitting;

namespace EulerQPade {
    internal class Program {
        static void Main() {
            List<(MultiPrecision<Pow2.N32> x, MultiPrecision<Pow2.N32> y)> expected_positives = new(), expected_negatives = new();

            using StreamReader sr = new("../../../../results_disused/euler_q_n32_65536.csv");

            sr.ReadLine();
            while (!sr.EndOfStream) {
                string? line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) {
                    break;
                }

                string[] line_split = line.Split(",");
                MultiPrecision<Pow2.N32> x = line_split[0], y = line_split[3];

                MultiPrecision<Pow2.N32> v = (x != 0) ? ((y + x) / (x * x)) : -1.5;

                if (x >= 0) {
                    expected_positives.Add((x, v));
                }
                if (x <= 0) {
                    expected_negatives.Add((-x, v));
                }
            }

            expected_negatives.Reverse();

            const bool reverse = true;

            for (int exp = 8; exp >= 4; exp--) {
                using StreamWriter sw_result = new($"../../../../results_disused/euler_q_e32_pade_exp{exp}.csv");

                foreach ((double x0, double h) in EnumRange(exp)) {
                    foreach (bool positive in new[] { true, false }) {
                        List<(MultiPrecision<Pow2.N32> x, MultiPrecision<Pow2.N32> y)> expecteds_range = positive
                            ? expected_positives.Where(item => item.x >= x0 && item.x <= x0 + h).ToList()
                            : expected_negatives.Where(item => item.x >= x0 && item.x <= x0 + h).ToList();

                        Vector<Pow2.N32> xs = !reverse
                            ? expecteds_range.Select(item => item.x - x0).ToArray()
                            : expecteds_range.Select(item => (x0 + h) - item.x).ToArray();

                        Vector<Pow2.N32> ys = expecteds_range.Select(item => item.y).ToArray();

                        if (xs.Any(x => x.val < 0)) {
                            throw new ArithmeticException("pade include negative x!");
                        }

                        for (int m = 4; m <= 54; m++) {
                            PadeFitter<Pow2.N32> pade = new(xs, ys, m, m);

                            Vector<Pow2.N32> param = pade.ExecuteFitting();
                            Vector<Pow2.N32> errs = pade.Error(param);

                            MultiPrecision<Pow2.N32> max_err = (errs / ys).Select(e => MultiPrecision<Pow2.N32>.Abs(e.val)).Max();

                            Console.WriteLine($"m={m},n={m}");
                            Console.WriteLine($"{max_err:e20}");

                            if (max_err < 2e-32) {
                                if (positive) {
                                    if (!reverse) {
                                        sw_result.WriteLine($"x0={x0}");
                                        sw_result.WriteLine($"x=[{x0},{x0 + h}]");
                                    }
                                    else {
                                        sw_result.WriteLine($"x0={x0 + h}");
                                        sw_result.WriteLine($"x=[{x0 + h},{x0}]");
                                    }
                                }
                                else {
                                    if (!reverse) {
                                        sw_result.WriteLine($"x0={-x0}");
                                        sw_result.WriteLine($"x=[{-x0},{-x0 - h}]");
                                    }
                                    else {
                                        sw_result.WriteLine($"x0={-x0 - h}");
                                        sw_result.WriteLine($"x=[{-x0 - h},{-x0}]");
                                    }
                                }

                                sw_result.WriteLine($"m={m},n={m}");
                                sw_result.WriteLine("numer");
                                foreach (var (_, val) in param[..m]) {
                                    sw_result.WriteLine(val);
                                }
                                sw_result.WriteLine("denom");
                                foreach (var (_, val) in param[m..]) {
                                    sw_result.WriteLine(val);
                                }
                                sw_result.WriteLine("hexcode");
                                for (int i = 0; i < m; i++) {
                                    sw_result.WriteLine($"({ToFP128(param[i])}, {ToFP128(param[i + m])}),");
                                }

                                sw_result.WriteLine("relative err");
                                sw_result.WriteLine($"{max_err:e20}");
                                sw_result.Flush();

                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }

        static IEnumerable<(double x0, double h)> EnumRange(int exp) {
            if (exp < 4) {
                throw new ArgumentOutOfRangeException(nameof(exp));
            }

            yield return (1 - double.ScaleB(1, -exp), double.ScaleB(1, -exp));
            yield return (1 - double.ScaleB(1, -exp + 2), 3 * double.ScaleB(1, -exp));

            exp -= 2;
            while (exp >= 1) {
                yield return (1 - double.ScaleB(1, -exp + 1), double.ScaleB(1, -exp));
                exp--;
            }
        }

        public static string ToFP128(MultiPrecision<Pow2.N32> x) {
            Sign sign = x.Sign;
            long exponent = x.Exponent;
            uint[] mantissa = x.Mantissa.Reverse().ToArray();

            string code = $"({(sign == Sign.Plus ? "+1" : "-1")}, {exponent}, 0x{mantissa[0]:X8}{mantissa[1]:X8}uL, 0x{mantissa[2]:X8}{mantissa[3]:X8}uL)";

            return code;
        }
    }
}