using MultiPrecision;
using MultiPrecisionDifferentiate;
using MultiPrecisionRootFinding;
using MultiPresicionDifferentiate;

namespace EulerQExpected {
    internal class MaximaFind {
        static void Main_() {
            static MultiPrecision<Pow2.N64> f(MultiPrecision<Pow2.N64> x) {
                MultiPrecision<Pow2.N64> y = EulerQ<Pow2.N64>.Value(x);

                return y;
            }

            MultiPrecision<Pow2.N64> h = MultiPrecision<Pow2.N64>.Ldexp(1, -12);

            (MultiPrecision<Pow2.N64>, MultiPrecision<Pow2.N64>) df(MultiPrecision<Pow2.N64> x) {
                Console.WriteLine(x);

                MultiPrecision<Pow2.N64> df = CenteredIntwayDifferential<Pow2.N64>.Differentiate(f, x, 1, h);
                MultiPrecision<Pow2.N64> df2 = CenteredIntwayDifferential<Pow2.N64>.Differentiate(f, x, 2, h);

                return (df, df2);
            }

            MultiPrecision<Pow2.N64> root = NewtonRaphsonFinder<Pow2.N64>.RootFind(df, "-0.4112548828125", iters: 16);

            Console.WriteLine($"q = {root}");
            Console.WriteLine($"{df(root)}");
            Console.WriteLine($"{f(root)}");

            Console.WriteLine("END");
            Console.Read();
        }
    }
}