using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;
using System;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
        [TestMethod]
        public void Credit_WithValidPositiveAmount_IncreasesBalance()
        {
            double beginningBalance = 11.99;
            double creditAmount = 5.77;
            double expected = 17.76;

            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            account.Credit(creditAmount);

            Assert.AreEqual(expected, account.Balance, 0.001, "Баланс должен увеличиться после внесения положительной суммы");
        }

        [TestMethod]
        public void Credit_WhenAmountIsNegative_ShouldThrowArgumentOutOfRangeException()
        {

            double beginningBalance = 11.99;
            double creditAmount = -5.00;
            BankAccount account = new BankAccount("Test User", beginningBalance);

            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return;
            }

            Assert.Fail("Ожидалось исключение ArgumentOutOfRangeException при отрицательной сумме пополнения");
        }

        [TestMethod]
        public void Credit_WithZeroAmount_BalanceShouldNotChange()
        {
            double beginningBalance = 100.00;
            double creditAmount = 0.0;
            BankAccount account = new BankAccount("Zero Test", beginningBalance);

            account.Credit(creditAmount);

            Assert.AreEqual(beginningBalance, account.Balance, 0.001, "Баланс не должен измениться при пополнении на 0");
        }
    }
}