using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurikhin_523
{ 
        /// <summary>
        /// Содержит математические функции из ПР №4 для вычисления значений w, d и y(x)
        /// </summary>
        public static class MathFunctions
        {
            /// <summary>
            /// Вычисляет значение функции w = |cos x − cos y|^(1 + 2 sin² y) · (1 + z + z²/2 + z³/3 + z⁴/4)
            /// </summary>
            /// <param name="x">Параметр x (в радианах)</param>
            /// <param name="y">Параметр y (в радианах)</param>
            /// <param name="z">Параметр z</param>
            /// <returns>Значение функции w</returns>
            /// <exception cref="ArgumentException">Если входные данные приводят к неопределённости</exception>
            public static double CalculateW(double x, double y, double z)
            {
                double cosX = Math.Cos(x);
                double cosY = Math.Cos(y);
                double sinY = Math.Sin(y);

                double exponent = 1 + 2 * Math.Pow(sinY, 2);
                double polyZ = 1 + z + Math.Pow(z, 2) / 2 + Math.Pow(z, 3) / 3 + Math.Pow(z, 4) / 4;

                double baseVal = Math.Abs(cosX - cosY);
                double w = Math.Pow(baseVal, exponent) * polyZ;

                return w;
            }

            /// <summary>
            /// Вычисляет значение функции d в зависимости от соотношения x и y и выбранной функции f(x)
            /// </summary>
            /// <param name="x">Значение аргумента x</param>
            /// <param name="y">Значение аргумента y</param>
            /// <param name="fType">Тип функции f: "sinh", "x2", "exp"</param>
            /// <returns>Значение функции d</returns>
            public static double CalculateD(double x, double y, string fType)
            {
                double f;
                switch (fType)
                {
                    case "sinh":
                        f = Math.Sinh(x);
                        break;
                    case "x2":
                        f = x * x;
                        break;
                    case "exp":
                        f = Math.Exp(x);
                        break;
                    default:
                        throw new ArgumentException("Неизвестный тип функции f(x)", nameof(fType));
                }

                if (Math.Abs(x - y) < 1e-10)
                {
                    return Math.Pow(y + f, 3) + 0.5;
                }
                else if (x > y)
                {
                    return Math.Pow(f - y, 3) + Math.Atan(f);
                }
                else
                {
                    return Math.Pow(y - f, 3) + Math.Atan(f);
                }
            }

            /// <summary>
            /// Вычисляет значение функции y = a · x³ + cos²(x³ − b)
            /// </summary>
            /// <param name="x">Текущее значение аргумента</param>
            /// <param name="a">Коэффициент a</param>
            /// <param name="b">Сдвиг b</param>
            /// <returns>Значение функции y(x)</returns>
            public static double CalculateY(double x, double a, double b)
            {
                double x3 = x * x * x;
                double cosTerm = Math.Cos(x3 - b);
                return a * x3 + cosTerm * cosTerm;
            }
   }
}
