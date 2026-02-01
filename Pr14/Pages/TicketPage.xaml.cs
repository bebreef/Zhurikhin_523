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
using static Pr14.Pages.LoginPage;

namespace Pr14.Pages
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        private int _sessionId;
        private List<Button> _selectedButtons;
        private decimal _totalPrice;

        public TicketPage(int sessionId, List<Button> selectedButtons)
        {
            InitializeComponent();
            _sessionId = sessionId;
            _selectedButtons = selectedButtons;

            LoadConfirmation();
        }

        private void LoadConfirmation()
        {
            var session = Core.Context.Sessions
                .FirstOrDefault(s => s.Id == _sessionId);

            if (session == null)
            {
                NavigationService?.GoBack();
                return;
            }

            tbFilm.Text = $"Фильм: {session.Movies.Title}";
            tbSession.Text = $"Сеанс: {session.StartDateTime:dd.MM.yyyy HH:mm}";
            tbHall.Text = $"Зал: {session.Halls.Name}";

            var places = _selectedButtons.Select(b =>
            {
                var (row, seat) = (Tuple<int, int>)b.Tag;
                return $"Ряд {row}, место {seat}";
            });

            tbPlaces.Text = "Места: " + string.Join(", ", places);

            _totalPrice = _selectedButtons.Count * session.Price;
            tbTotalPrice.Text = $"Итого: {_totalPrice:N0} ₽";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var session = Core.Context.Sessions.First(s => s.Id == _sessionId);

            foreach (var btn in _selectedButtons)
            {
                var (row, seat) = (Tuple<int, int>)btn.Tag;

                var ticket = new Tickets
                {
                    UserId = CurrentUser.Id,
                    SessionId = _sessionId,
                    Row = row,
                    Seat = seat,
                    Price = session.Price,
                    PurchaseDate = DateTime.Now
                };

                Core.Context.Tickets.Add(ticket);
            }

            Core.Context.SaveChanges();

            MessageBox.Show($"Билеты успешно куплены! ({_selectedButtons.Count} шт.)", "Успех");
            NavigationService?.Navigate(new MainPage());
        }
    }
}