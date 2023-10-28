using MultiPrecision;

namespace EulerQApproximation {
    public static class EulerQ<N> where N : struct, IConstant {
        public static MultiPrecision<N> Value(MultiPrecision<N> q, long maxiter = int.MaxValue) {
            if (!(MultiPrecision<N>.Abs(q) <= 1)) {
                return MultiPrecision<N>.NaN;
            }
            if (MultiPrecision<N>.Abs(q) == 1) {
                return 0;
            }

            MultiPrecision<N> y = 1, w = q;
            for (long k = 1; k <= maxiter; k++) {
                MultiPrecision<N> wm1 = 1 - w;

                y *= wm1;
                w *= q;

                if (wm1 == 1) {
                    return y;
                }
            }

            throw new ArithmeticException("Not convergence EulerQ");
        }
    }
}
