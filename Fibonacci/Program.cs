using System;

namespace Fibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int result = Fibonacci(5);
            Console.WriteLine($"Результат: {result}");
        }

        /// <summary>
        /// Вычисляет n-е число Фибоначчи (рекурсивная версия с итеративной реализацией внутри).
        /// </summary>
        /// <param name="n">Номер числа Фибоначчи (n ≥ 0)</param>
        /// <returns>n-е число последовательности Фибоначчи</returns>
        /// <remarks>
        /// Последовательность: 0, 1, 1, 2, 3, 5, 8, 13, ...
        /// Для n=0 возвращает 0, для n=1 возвращает 1.
        /// </remarks>
        static int Fibonacci(int n)
        {
            Console.WriteLine("The output is: ");

            if (n == 0) return 0;
            if (n == 1) return 1;

            int n1 = 0;
            int n2 = 1;
            int sum;

            for (int i = 2; i <= n; i++)
            {
                sum = n1 + n2;
                n1 = n2;
                n2 = sum;
            }

            return n2;
        }
    }
}