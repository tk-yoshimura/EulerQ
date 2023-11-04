using MultiPrecision;
using MultiPrecisionComplex;
using System.Numerics;

namespace EulerQComplex {
    public static class EulerQ<N> where N : struct, IConstant {
        public static Complex<N> Value(Complex<N> q, long maxiter = int.MaxValue) {
            if (!(q.Magnitude <= 1)) {
                return MultiPrecision<N>.NaN;
            }

            Complex<N> y = 1, w = q;
            for (long k = 1; k <= maxiter; k++) {
                Complex<N> y_prev = y;

                y *= 1 - w;
                w *= q;

                if (y == y_prev) {
                    break;
                }
            }

            return y;
        }
    }
}
