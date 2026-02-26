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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(tbX.Text, out double x) ||
                !double.TryParse(tbY.Text, out double y) ||
                !double.TryParse(tbZ.Text, out double z))
            {
                MessageBox.Show("Все поля должны содержать числа.", "Ошибка ввода");
                return;
            }

            try
            {
                double cosX = Math.Cos(x);
                double cosY = Math.Cos(y);
                double sinY = Math.Sin(y);

                double exponent = 1 + 2 * Math.Pow(sinY, 2);

                double polyZ = 1 + z + Math.Pow(z, 2) / 2 + Math.Pow(z, 3) / 3 + Math.Pow(z, 4) / 4;

                double baseVal = Math.Abs(cosX - cosY);
                double w = Math.Pow(baseVal, exponent) * polyZ;

                tbResult.Text = w.ToString("G8");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Clear(); tbY.Clear(); tbZ.Clear(); tbResult.Clear();
        }
    }
}