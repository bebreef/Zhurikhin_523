using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace Zhurikhin_523.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            var area = chart.ChartAreas["mainArea"];
            area.AxisX.Title = "x";
            area.AxisY.Title = "y";

            var series = new Series("y(x)")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                IsValueShownAsLabel = false
            };
            chart.Series.Add(series);

            chart.Legends["legendMain"].Enabled = true;
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(tbA.Text, out double a) ||
                !double.TryParse(tbB.Text, out double b) ||
                !double.TryParse(tbX0.Text, out double x0) ||
                !double.TryParse(tbXk.Text, out double xk) ||
                !double.TryParse(tbDx.Text, out double dx))
            {
                MessageBox.Show("Все поля должны содержать корректные числа.", "Ошибка ввода");
                return;
            }

            if (Math.Abs(dx) < 1e-10)
            {
                MessageBox.Show("Шаг dx не может быть равен нулю.", "Ошибка");
                return;
            }

            tbTable.Clear();
            var sb = new StringBuilder();

            double x = x0;
            int stepCount = 0;
            const int MAX_STEPS = 5000;

            var series = chart.Series["y(x)"];
            series.Points.Clear();

            while ((dx > 0 && x <= xk + 1e-9) || (dx < 0 && x >= xk - 1e-9))
            {
                if (stepCount++ > MAX_STEPS)
                {
                    MessageBox.Show("Слишком много точек (> 5000). Увеличьте шаг dx.", "Предупреждение");
                    break;
                }

                double x3 = x * x * x;
                double cosTerm = Math.Cos(x3 - b);
                double y = a * x3 + cosTerm * cosTerm;

                sb.AppendLine($"x = {x,12:F6}   y = {y,14:G8}");

                series.Points.AddXY(x, y);

                x += dx;
            }

            tbTable.Text = sb.ToString();

            chart.ChartAreas["mainArea"].RecalculateAxesScale();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbA.Clear();
            tbB.Clear();
            tbX0.Clear();
            tbXk.Clear();
            tbDx.Clear();
            tbTable.Clear();
            chart.Series["y(x)"].Points.Clear();
        }
    }
}