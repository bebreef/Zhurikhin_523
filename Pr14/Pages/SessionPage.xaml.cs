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

namespace Pr14.Pages
{
    public partial class SessionPage : Page
    {
        private int _sessionId;
        private Sessions _session;
        private List<Button> _selectedSeats = new List<Button>();

        public SessionPage(int sessionId)
        {
            InitializeComponent();
            _sessionId = sessionId;

            LoadSession();
            GenerateSeats();
        }

        private void LoadSession()
        {
            _session = Core.Context.Sessions
                .FirstOrDefault(s => s.Id == _sessionId);

            if (_session == null)
            {
                MessageBox.Show("Сеанс не найден");
                NavigationService?.GoBack();
                return;
            }

            tbSessionInfo.Text = $"{_session.Movies.Title} | {_session.Halls.Name} | {_session.StartDateTime:dd.MM.yyyy HH:mm}";
        }

        private void GenerateSeats()
        {
            int rows = _session.Halls.RowsCount;
            int seatsPerRow = _session.Halls.SeatsPerRow;

            var occupied = Core.Context.Tickets
                .Where(t => t.SessionId == _sessionId)
                .Select(t => new { t.Row, t.Seat })
                .ToList();

            for (int row = 1; row <= rows; row++)
            {
                var rowPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                var rowLabel = new TextBlock
                {
                    Text = $"Ряд {row}",
                    Width = 60,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White
                };
                rowPanel.Children.Add(rowLabel);

                for (int seat = 1; seat <= seatsPerRow; seat++)
                {
                    var btn = new Button
                    {
                        Content = seat.ToString(),
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(2),
                        Tag = new Tuple<int, int>(row, seat)
                    };

                    if (occupied.Any(o => o.Row == row && o.Seat == seat))
                    {
                        btn.Background = Brushes.Gray;
                        btn.IsEnabled = false;
                        btn.ToolTip = "Место занято";
                    }
                    else
                    {
                        btn.Background = Brushes.DarkGreen;
                        btn.Foreground = Brushes.White;
                        btn.Click += Seat_Click;
                    }

                    rowPanel.Children.Add(btn);
                }

                seatsPanel.Children.Add(rowPanel);
            }
        }

        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var (row, seat) = (Tuple<int, int>)btn.Tag;

            if (_selectedSeats.Contains(btn))
            {
                _selectedSeats.Remove(btn);
                btn.Background = Brushes.DarkGreen;
            }
            else
            {
                _selectedSeats.Add(btn);
                btn.Background = Brushes.LimeGreen;
            }

            tbSelectedCount.Text = _selectedSeats.Count.ToString();
            btnConfirm.IsEnabled = _selectedSeats.Count > 0;
            btnReset.IsEnabled = _selectedSeats.Count > 0;
        }

        private void ResetSelection_Click(object sender, RoutedEventArgs e)
        {
            foreach (var btn in _selectedSeats)
            {
                btn.Background = Brushes.DarkGreen;
            }

            _selectedSeats.Clear();
            tbSelectedCount.Text = "0";
            btnConfirm.IsEnabled = false;
            btnReset.IsEnabled = false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void ConfirmTicket_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new TicketPage(_sessionId, _selectedSeats));
        }
    }
}