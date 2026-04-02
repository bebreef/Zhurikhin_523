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

namespace Zhurikhin_523
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double sum = double.Parse(txtSum.Text.Replace(",", "."));
                int termMonths = int.Parse(txtTerm.Text);
                double ratePercent = double.Parse(txtRate.Text.Replace(",", "."));

                if (sum <= 0 || termMonths <= 0 || ratePercent <= 0)
                {
                    MessageBox.Show("Все значения должны быть положительными!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double income;

                if (rbSimple.IsChecked == true)
                {
                    income = sum * (ratePercent / 100) * (termMonths / 12.0);
                }
                else
                {
                    double monthlyRate = ratePercent / 100 / 12;
                    double totalAmount = sum * Math.Pow(1 + monthlyRate, termMonths);
                    income = totalAmount - sum;
                }

                txtResult.Text = $"Доход по вкладу = {income:F2} руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода данных: {ex.Message}\n\nИспользуйте числа (точка или запятая для десятичных).",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


