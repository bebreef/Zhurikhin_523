using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhurikhin_523;

namespace UnitTestProject4
{
    [TestClass]
    public class UnitTest1
    {
        private readonly MainWindow _window = new MainWindow();

        [TestMethod]
        public void TC_06_SimpleInterest_OneMonth()
        {
            double income = _window.CalculateIncome(100000, 1, 12, false);
            Assert.AreEqual(1000.00, income);
        }

        [TestMethod]
        public void TC_07_CompoundInterest_OneMonth()
        {
            double income = _window.CalculateIncome(100000, 1, 12, true);
            Assert.AreEqual(1000.00, income);
        }

        [TestMethod]
        public void TC_08_SmallInterestRate()
        {
            double income = _window.CalculateIncome(50000, 6, 0.5, true);
            Assert.AreEqual(125.13, income);   
        }

        [TestMethod]
        public void TC_09_LargeSumAndLongTerm()
        {
            double income = _window.CalculateIncome(1000000, 60, 15, true);
            Assert.IsTrue(income > 1000000); 
        }

        [TestMethod]
        public void TC_10_CompoundVsSimple_LongTerm()
        {
            double simpleIncome = _window.CalculateIncome(200000, 36, 10, false);
            double compoundIncome = _window.CalculateIncome(200000, 36, 10, true);

            Assert.IsTrue(compoundIncome > simpleIncome,
                "При длительном сроке сложные проценты должны давать больший доход");
        }
    }
}