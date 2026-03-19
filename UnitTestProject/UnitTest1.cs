using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhurikhin_523;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int res = 2 + 2;
            Assert.AreEqual(res, 4);   
            Assert.AreNotEqual(res, 5);
            Assert.IsFalse(res > 5);
            Assert.IsTrue(res < 5);
        }
        [TestMethod]
        public void CalculateW_ZeroBase_ReturnsZero()
        {
            double result = MathFunctions.CalculateW(0, 0, 10);
            Assert.AreEqual(0.0, result, 1e-10);
        }

        [TestMethod]
        public void CalculateW_BaseOne_PolyMultiplied()
        {
            double result = MathFunctions.CalculateW(0, Math.PI, 0);
            Assert.AreEqual(2.0, result, 1e-10);
        }

        [TestMethod]
        public void CalculateD_WhenXEqualsY_ReturnsCorrectValue()
        {
            double x = 4.0;
            double y = 4.0;

            double f_sinh = Math.Sinh(x);
            double expected = Math.Pow(y + f_sinh, 3) + 0.5;

            double actual = MathFunctions.CalculateD(x, y, "sinh");

            Assert.AreEqual(expected, actual, 1e-8, "Неверное значение при x == y для sinh");
        }

        [TestMethod]
        public void CalculateD_xGreaterY_CorrectBranch()
        {
            double f = Math.Sinh(3);           
            double expected = Math.Pow(f - 1, 3) + Math.Atan(f);

            double result = MathFunctions.CalculateD(3, 1, "sinh");
            Assert.AreEqual(expected, result, 1e-8);
        }

        [TestMethod]
        public void CalculateY_CosSquaredTermWorks()
        {
            double y = MathFunctions.CalculateY(1.0, 2.0, 0.0);
            double x3 = 1.0;
            double cosVal = Math.Cos(x3);
            double expected = 2.0 * x3 + (cosVal * cosVal);

            Assert.AreEqual(expected, y, 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateD_InvalidFType_Throws()
        {
            MathFunctions.CalculateD(5, 5, "invalid_type");
        }
    }
}
