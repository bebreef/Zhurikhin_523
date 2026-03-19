using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zhurikhin_523;

namespace Zhurikhin_523.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private double ComputeF(double x)
        {
            if (rbSinh.IsChecked == true) return Math.Sinh(x);
            if (rbX2.IsChecked == true) return x * x;
            if (rbExp.IsChecked == true) return Math.Exp(x);
            return 0;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить"
        /// Считывает значения x, y и выбранный тип функции f, выполняет расчёт и выводит результат
        /// </summary>
        /// <param name="sender">Источник события (кнопка)</param>
        /// <param name="e">Аргументы события</param>
        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(tbX.Text, out double x) ||
                !double.TryParse(tbY.Text, out double y))
            {
                MessageBox.Show("Введите корректные числа в поля x и y.", "Ошибка ввода");
                return;
            }

            string fType = rbSinh.IsChecked == true ? "sinh" :
                           rbX2.IsChecked == true ? "x2" :
                           rbExp.IsChecked == true ? "exp" : "sinh";

            if (CalculateD(x, y, fType, out double result))
            {
                tbResult.Text = result.ToString("G8");
            }
            else
            {
                MessageBox.Show("Ошибка при вычислении функции d.", "Ошибка");
            }
        }

        /// <summary>
        /// Выполняет расчёт значения функции d в зависимости от соотношения x и y и типа f(x).
        /// </summary>
        /// <param name="x">Значение аргумента x</param>
        /// <param name="y">Значение аргумента y</param>
        /// <param name="fType">Тип функции f: "sinh", "x2" или "exp"</param>
        /// <param name="result">Вычисленное значение функции d (при успехе)</param>
        /// <returns>true — расчёт успешен, false — произошла ошибка</returns>
        private bool CalculateD(double x, double y, string fType, out double result)
        {
            result = 0;

            try
            {
                double d = MathFunctions.CalculateD(x, y, fType);
                result = d;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Clear();
            tbY.Clear();
            tbResult.Clear();
            rbSinh.IsChecked = true;
        }
    }
}