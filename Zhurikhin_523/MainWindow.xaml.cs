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

        /// <summary>
        /// Метод расчёта дохода по вкладу (CalculateIncome)
        /// Выполняет валидацию данных и расчёт по выбранной схеме процентов
        /// </summary>
        /// <param name="sum">Сумма вклада</param>
        /// <param name="termMonths">Срок в месяцах</param>
        /// <param name="ratePercent">Годовая процентная ставка</param>
        /// <param name="isCompound">true = сложные проценты, false = простые проценты</param>
        /// <returns>Доход по вкладу (округлённый до 2 знаков)</returns>
        /// <exception cref="ArgumentException">При некорректных значениях параметров</exception>
        public double CalculateIncome(double sum, int termMonths, double ratePercent, bool isCompound)
        {
            if (sum <= 0)
                throw new ArgumentException("Сумма вклада должна быть больше нуля.");

            if (termMonths <= 0)
                throw new ArgumentException("Срок вклада должен быть больше нуля.");

            if (ratePercent <= 0)
                throw new ArgumentException("Процентная ставка должна быть больше нуля.");

            double income;

            if (!isCompound)
            {
                income = sum * (ratePercent / 100.0) * (termMonths / 12.0);
            }
            else
            {
                double monthlyRate = ratePercent / 100.0 / 12;
                double totalAmount = sum * Math.Pow(1 + monthlyRate, termMonths);
                income = totalAmount - sum;
            }

            return Math.Round(income, 2); 
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить"
        /// </summary>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double sum = double.Parse(txtSum.Text.Replace(",", "."));
                int termMonths = int.Parse(txtTerm.Text);
                double ratePercent = double.Parse(txtRate.Text.Replace(",", "."));

                bool isCompound = rbCompound.IsChecked == true;

                double income = CalculateIncome(sum, termMonths, ratePercent, isCompound);

                txtResult.Text = $"Доход по вкладу = {income:F2} руб.";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, вводите только числовые значения.\n" +
                                "Для десятичных дробей используйте точку или запятую.",
                                "Ошибка формата", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}