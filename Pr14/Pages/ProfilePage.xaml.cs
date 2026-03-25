using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ProfilePage : Page
    {
        public ObservableCollection<TicketViewModel> Tickets { get; set; } = new ObservableCollection<TicketViewModel>();

        public string CurrentLogin => CurrentUser.Login ?? "Не авторизован";
        public string CurrentFullName => CurrentUser.FullName ?? "Не указано";
        public string CurrentEmail => CurrentUser.Email ?? "Не указано";
        public string CurrentPhone => CurrentUser.Phone ?? "Не указано";

        public ProfilePage()
        {
            InitializeComponent();
            DataContext = this;
            LoadTickets();
        }

        private void LoadTickets()
        {
            try
            {
                var userTickets = Core.Context.Tickets
                    .Where(t => t.UserId == CurrentUser.Id)
                    .ToList();  

                var ticketsView = userTickets.Select(t => new TicketViewModel
                {
                    MovieTitle = t.Sessions?.Movies?.Title ?? "Фильм удалён",
                    SessionDateTime = t.Sessions?.StartDateTime ?? DateTime.MinValue,
                    HallName = t.Sessions?.Halls?.Name ?? "Зал удалён",
                    Place = $"Ряд {t.Row}, место {t.Seat}",
                    Price = t.Price,
                    PurchaseDate = t.PurchaseDate ?? DateTime.MinValue
                })
                .OrderByDescending(t => t.PurchaseDate)
                .ToList();

                foreach (var ticket in ticketsView)
                    Tickets.Add(ticket);

                tbNoTickets.Visibility = ticketsView.Any() ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки билетов:\n{ex.Message}\n{ex.StackTrace}", "Краш");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из аккаунта?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CurrentUser.Logout();

                if (Window.GetWindow(this) is MainWindow mw && mw.MainFrame.Content is MainPage main)
                {
                    main.btnLogin.Visibility = Visibility.Visible;
                    main.btnProfile.Content = "Профиль";
                }

                NavigationService?.Navigate(new MainPage());
            }
        }
    }

    public class TicketViewModel
    {
        public string MovieTitle { get; set; }
        public DateTime SessionDateTime { get; set; }
        public string HallName { get; set; }
        public string Place { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}