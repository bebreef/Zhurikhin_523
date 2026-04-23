using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Zhurikhin_523.Core
{
    /// <summary>
    /// Упрощённая реализация алгоритма RSA для учебных целей.
    /// Шифрование выполняется посимвольно с использованием BigInteger.
    /// </summary>
    public static class RsaCipher
    {
        /// <summary>
        /// Проверяет, является ли целое число простым.
        /// </summary>
        /// <param name="n">Число для проверки</param>
        /// <returns>True, если число простое; иначе False.</returns>
        public static bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            for (int i = 3; i * i <= n; i += 2)
                if (n % i == 0) return false;
            return true;
        }

        /// <summary>
        /// Вычисляет наибольший общий делитель (НОД) двух чисел.
        /// </summary>
        private static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            while (b != 0) { var t = b; b = a % b; a = t; }
            return a;
        }

        /// <summary>
        /// Находит модульное обратное число методом расширенного алгоритма Евклида.
        /// </summary>
        private static BigInteger ModInverse(BigInteger e, BigInteger phi)
        {
            BigInteger a = e, b = phi, u = 1, v = 0;
            while (b != 0)
            {
                var q = a / b;
                var temp = a % b; a = b; b = temp;
                temp = u - q * v; u = v; v = temp;
            }
            return (u % phi + phi) % phi;
        }

        /// <summary>
        /// Генерирует пару ключей RSA: открытый (e), закрытый (d) и модуль (n).
        /// </summary>
        /// <param name="p">Первое простое число</param>
        /// <param name="q">Второе простое число</param>
        /// <returns>Кортеж (e, d, n)</returns>
        /// <exception cref="ArgumentException">Вызывается, если p или q не являются простыми числами.</exception>
        public static (BigInteger e, BigInteger d, BigInteger n) GenerateKeys(int p, int q)
        {
            if (!IsPrime(p) || !IsPrime(q))
                throw new ArgumentException("Оба числа p и q должны быть простыми.");

            var n = new BigInteger(p * q);
            var phi = new BigInteger((p - 1) * (q - 1));

            BigInteger e = 3;
            while (Gcd(e, phi) != 1) e += 2;

            var d = ModInverse(e, phi);
            return (e, d, n);
        }

        /// <summary>
        /// Шифрует входную строку посимвольно с использованием открытого ключа.
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="e">Открытый экспонент</param>
        /// <param name="n">Модуль</param>
        /// <returns>Список зашифрованных чисел (BigInteger)</returns>
        /// <exception cref="ArgumentNullException">Текст равен null.</exception>
        /// <exception cref="ArgumentException">Текст пуст или код символа превышает модуль n.</exception>
        public static List<BigInteger> Encrypt(string text, BigInteger e, BigInteger n)
        {
            if (text == null) throw new ArgumentNullException(nameof(text), "Входной текст не может быть null.");
            if (text.Length == 0) throw new ArgumentException("Входной текст не может быть пустым.", nameof(text));

            var encrypted = new List<BigInteger>(text.Length);
            foreach (char ch in text)
            {
                if (new BigInteger(ch) >= n)
                    throw new ArgumentException($"Модуль n ({n}) слишком мал для символа '{ch}' (код {(int)ch}). Увеличьте p и q.");

                encrypted.Add(BigInteger.ModPow(ch, e, n));
            }
            return encrypted;
        }

        /// <summary>
        /// Дешифрует список чисел обратно в строку с использованием закрытого ключа.
        /// </summary>
        /// <param name="encrypted">Список зашифрованных чисел</param>
        /// <param name="d">Закрытый экспонент</param>
        /// <param name="n">Модуль</param>
        /// <returns>Восстановленный исходный текст</returns>
        /// <exception cref="ArgumentNullException">Список шифротекста равен null.</exception>
        public static string Decrypt(List<BigInteger> encrypted, BigInteger d, BigInteger n)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted), "Список шифротекста не может быть null.");

            char[] chars = new char[encrypted.Count];
            for (int i = 0; i < encrypted.Count; i++)
            {
                BigInteger decrypted = BigInteger.ModPow(encrypted[i], d, n);
                chars[i] = (char)decrypted;
            }
            return new string(chars);
        }
    }
}