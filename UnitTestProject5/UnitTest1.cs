using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using Zhurikhin_523.Core;

namespace UnitTestProject5
{
    [TestClass]
    public class RsaCipherTests
    {
        [TestMethod]
        [DataRow("Hello", 17, 19)]
        [DataRow("TestRSA", 23, 29)]
        public void EncryptDecrypt_LatinText_RoundtripSuccessful(string originalText, int p, int q)
        {
            var (e, d, n) = RsaCipher.GenerateKeys(p, q);

            List<BigInteger> encrypted = RsaCipher.Encrypt(originalText, e, n);
            string decrypted = RsaCipher.Decrypt(encrypted, d, n);

            Assert.AreEqual(originalText, decrypted, "Дешифрованный текст не совпадает с исходным.");
            Assert.IsTrue(encrypted.Any(c => c != (BigInteger)(int)originalText[0]),
                "Зашифрованные данные должны отличаться от исходных.");
        }
        [TestMethod]
        [DataRow("Привет", 257, 263)]
        [DataRow("Шифрование", 311, 313)]
        public void EncryptDecrypt_CyrillicText_WithBigInteger(string originalText, int p, int q)
        {
            var (e, d, n) = RsaCipher.GenerateKeys(p, q);

            List<BigInteger> encrypted = RsaCipher.Encrypt(originalText, e, n);
            string decrypted = RsaCipher.Decrypt(encrypted, d, n);

            Assert.AreEqual(originalText, decrypted, "Потеря данных при шифровании Unicode-символов.");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_Throws_WhenModulusTooSmall()
        {
            char symbol = 'Я';
            var (e, d, n) = RsaCipher.GenerateKeys(7, 11);

            RsaCipher.Encrypt(symbol.ToString(), e, n);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateKeys_Throws_OnCompositeNumber()
        {
            RsaCipher.GenerateKeys(15, 13);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_Throws_OnNullInput()
        {
            var (e, d, n) = RsaCipher.GenerateKeys(11, 13);
            RsaCipher.Encrypt(null, e, n);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_Throws_OnEmptyInput()
        {
            var (e, d, n) = RsaCipher.GenerateKeys(11, 13);
            RsaCipher.Encrypt("", e, n);
        }
    }
}