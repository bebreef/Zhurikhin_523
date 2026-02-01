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
    public partial class MovieDetailsPage : Page
    {
        private int _movieId;
        public ObservableCollection<SessionViewModel> Sessions { get; set; } = new ObservableCollection<SessionViewModel>();

        public MovieDetailsPage(int movieId)
        {
            InitializeComponent();
            _movieId = movieId;
            DataContext = this;

            LoadMovie();
            LoadSessions();
        }

        private void LoadMovie()
        {
            var movie = Core.Context.Movies.FirstOrDefault(m => m.Id == _movieId);
            if (movie == null)
            {
                MessageBox.Show("Фильм не найден");
                NavigationService?.GoBack();
                return;
            }

            tbTitle.Text = movie.Title;
            tbRating.Text = movie.Rating.HasValue ? $"{movie.Rating:0.0}" : "Рейтинг отсутствует";
            tbAge.Text = movie.AgeRestriction ?? "Не указано";
            tbRelease.Text = movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value.ToString("dd.MM.yyyy") : "Дата не указана";
            tbDescription.Text = movie.Description ?? "Описание отсутствует";

            string posterPath = $"/Images/{movie.Id}.png"; 

            try
            {
                var uri = new Uri(posterPath, UriKind.Relative);
                imgPoster.Source = new BitmapImage(uri);
            }
            catch (Exception ex)
            {
                imgPoster.Source = null; 
            }
        }
        private void LoadSessions()
        {
            var sessions = Core.Context.Sessions
                .Where(s => s.MovieId == _movieId)
                .Select(s => new SessionViewModel
                {
                    Id = s.Id,
                    StartDateTime = s.StartDateTime,
                    HallName = s.Halls.Name,
                    Price = s.Price
                })
                .OrderBy(s => s.StartDateTime)
                .ToList();

            foreach (var s in sessions)
                Sessions.Add(s);

            tbNoSessions.Visibility = sessions.Any() ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SelectSession_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is int sessionId)
            {
                if (!CurrentUser.IsLoggedIn)
                {
                    MessageBox.Show("Для выбора места нужно войти в аккаунт!", "Требуется авторизация");
                    NavigationService?.Navigate(new LoginPage());
                    return;
                }

                NavigationService?.Navigate(new SessionPage(sessionId));
            }
        }
    }

    public class SessionViewModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public string HallName { get; set; }
        public decimal Price { get; set; }
    }
}