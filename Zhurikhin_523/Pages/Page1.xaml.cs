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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить"
        /// Считывает значения x, y, z из текстовых полей, вызывает расчёт и выводит результат
        /// </summary>
        /// <param name="sender">Источник события (кнопка)</param>
        /// <param name="e">Аргументы события</param>
        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(tbX.Text, out double x) ||
                !double.TryParse(tbY.Text, out double y) ||
                !double.TryParse(tbZ.Text, out double z))
            {
                MessageBox.Show("Все поля должны содержать числа.", "Ошибка ввода");
                return;
            }

            if (CalculateW(x, y, z, out double result))
            {
                tbResult.Text = result.ToString("G8");
            }
            else
            {
                MessageBox.Show("Ошибка при вычислении функции w.", "Ошибка");
            }
        }

        /// <summary>
        /// Выполняет расчёт значения функции w = |cos x − cos y|^(1 + 2 sin² y) · (1 + z + z²/2 + z³/3 + z⁴/4)
        /// </summary>
        /// <param name="x">Значение параметра x (в радианах)</param>
        /// <param name="y">Значение параметра y (в радианах)</param>
        /// <param name="z">Значение параметра z</param>
        /// <param name="result">Вычисленное значение функции w (при успехе)</param>
        /// <returns>true — если расчёт выполнен успешно, false — при ошибке</returns>
        private bool CalculateW(double x, double y, double z, out double result)
        {
            result = 0;

            try
            {
                double w = MathFunctions.CalculateW(x, y, z);
                result = w;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Clear(); tbY.Clear(); tbZ.Clear(); tbResult.Clear();
        }
    }
}