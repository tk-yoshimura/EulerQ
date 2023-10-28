using EulerQFP64;

namespace EulerQFP64Test {
    [TestClass]
    public class EulerQTest {
        static readonly List<(double x, double y, double lny)> expecteds = new();

        static EulerQTest() {
            using StreamReader sr = new("../../../../results/euler_q_n32.csv");
            sr.ReadLine();

            while (!sr.EndOfStream) {
                string? line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) {
                    break;
                }

                string[] line_split = line.Split(",");
                double x = double.Parse(line_split[0]);
                double y = double.Parse(line_split[1]);
                double lny = double.Parse(line_split[2]);

                expecteds.Add((x, y, lny));
            }
        }

        [TestMethod]
        public void ValueTest() {
            foreach ((double x, double expected, _) in expecteds) {
                double actual = EulerQ.Value(x);

                Console.WriteLine($"{x},{expected},{actual}");

                Assert.AreEqual(expected, actual, expected * 3e-15 + 1e-15, $"\n{x}\n{expected}\n{actual}");
                Assert.AreEqual(expected, actual, expected * 2e-13, $"\n{x}\n{expected}\n{actual}");
            }
        }

        [TestMethod]
        public void LogValueTest() {
            foreach ((double x, _, double expected) in expecteds) {
                double actual = EulerQ.LogValue(x);

                Console.WriteLine($"{x},{expected},{actual}");

                if (double.Abs(x) < 0.125) {
                    Assert.AreEqual(expected, actual, double.Abs(expected) * 3e-16, $"\n{x}\n{expected}\n{actual}");
                }
                else {
                    Assert.AreEqual(expected, actual, double.Abs(expected) * 3e-15 + 1e-15, $"\n{x}\n{expected}\n{actual}");
                }
            }
        }

        [TestMethod]
        public void NearOneTest() {
            Assert.AreEqual(0.0, EulerQ.Value(double.BitIncrement(-1)));
            Assert.AreEqual(0.0, EulerQ.Value(double.BitDecrement(1)));

            Assert.IsTrue(double.IsFinite(EulerQ.LogValue(double.BitIncrement(-1))));
            Assert.IsTrue(double.IsFinite(EulerQ.LogValue(double.BitDecrement(1))));
        }
    }
}