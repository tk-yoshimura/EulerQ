using MultiPrecision;
using MultiPrecisionRootFinding;

namespace EulerQApproximation {
    internal class RootFind {
        static void Main() {
            static MultiPrecision<Pow2.N64> f(MultiPrecision<Pow2.N64> x) { 
                MultiPrecision<Pow2.N64> y = EulerQ<Pow2.N64>.Value(x);
                MultiPrecision<Pow2.N64> lny = MultiPrecision<Pow2.N64>.Log(y);
                MultiPrecision<Pow2.N64> v = lny * (1 - x * x);

                return v;
            }

            MultiPrecision<Pow2.N64> root = SecantFinder<Pow2.N64>.RootFind(f, -0.6793212890625);

            Console.WriteLine($"q = {root}");
            Console.WriteLine($"{f(root)}");

            Console.WriteLine("END");
            Console.Read();
        }
    }
}