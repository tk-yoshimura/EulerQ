using MultiPrecision;
using MultiPrecisionComplex;

namespace EulerQComplex {
    internal class Program {
        static void Main() {
            using StreamWriter sw = new("../../../../results/euler_q_complex_n4.csv");
            sw.WriteLine("theta,norm,q,y:=euler_q(q),v:=ln(y)*(1-q^2)");

            for (MultiPrecision<Pow2.N4> theta = 0; theta < 2; theta += 1 / 1024d) {
                Complex<Pow2.N4> p = (MultiPrecision<Pow2.N4>.CosPI(theta), MultiPrecision<Pow2.N4>.SinPI(theta));

                for (MultiPrecision<Pow2.N4> n = 0; n < 1; n += 1 / 1024d) {
                    MultiPrecision<Pow2.N4> norm = MultiPrecision<Pow2.N4>.Sqrt(n);

                    Complex<Pow2.N4> q = p * norm;
                    Complex<Pow2.N4> y = EulerQ<Pow2.N4>.Value(q);
                    Complex<Pow2.N4> v = Complex<Pow2.N4>.Log(y) * (1 - q * q);

                    sw.WriteLine($"{theta},{norm},{q},{y},{v}");
                    Console.WriteLine($"{theta}\n{norm}\n{v:e20}");
                }

                sw.Flush();
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}