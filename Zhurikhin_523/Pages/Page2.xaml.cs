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

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(tbX.Text, out double x) ||
                !double.TryParse(tbY.Text, out double y))
            {
                MessageBox.Show("Введите корректные числа в поля x и y.", "Ошибка");
                return;
            }

            double f = ComputeF(x);
            double result;

            if (Math.Abs(x - y) < 1e-10)   
            {
                result = Math.Pow(y + f, 3) + 0.5;
            }
            else if (x > y)
            {
                result = Math.Pow(f - y, 3) + Math.Atan(f);
            }
            else 
            {
                result = Math.Pow(y - f, 3) + Math.Atan(f);
            }

            tbResult.Text = result.ToString("G8");
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