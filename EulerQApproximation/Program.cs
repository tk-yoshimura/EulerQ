using MultiPrecision;

namespace EulerQApproximation {
    internal class Program {
        static void Main() {
            using StreamWriter sw = new("../../../../results/euler_q_n32.csv");
            sw.WriteLine("x,y:=euler_q(x),lny:=ln(y),v:=lny*(1-x^2)");

            for (MultiPrecision<Pow2.N32> x = -1; x <= 1; x += 1 / 4096d) {
                MultiPrecision<Pow2.N32> y = EulerQ<Pow2.N32>.Value(x);
                MultiPrecision<Pow2.N32> lny = MultiPrecision<Pow2.N32>.Log(y);
                MultiPrecision<Pow2.N32> v = lny * (1 - x * x);

                if (x == -1) {
                    v = -MultiPrecision<Pow2.N32>.Square(MultiPrecision<Pow2.N32>.PI) / 12;
                }
                else if (x == 1) { 
                    v = -MultiPrecision<Pow2.N32>.Square(MultiPrecision<Pow2.N32>.PI) / 3;
                }

                sw.WriteLine($"{x},{y},{lny},{v}");
                Console.WriteLine($"{x}\n{y:e20}\n{lny:e20}\n{v:e20}");
            }

            sw.Close();

            Console.WriteLine("END");
            Console.Read();
        }
    }
}