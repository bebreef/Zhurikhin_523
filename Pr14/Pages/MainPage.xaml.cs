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

namespace Pr14.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<MovieViewModel> Movies { get; set; }
                    = new ObservableCollection<MovieViewModel>();

        private List<Movies> allMovies; 

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadMovies();
        }

        private void LoadMovies()
        {
            allMovies = Core.Context.Movies.ToList();

            foreach (var m in allMovies)
            {
                Movies.Add(new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Rating = m.Rating,
                    AgeRestriction = m.AgeRestriction,
                    ReleaseDate = m.ReleaseDate,
                    PosterPath = $"/Images/{m.Id}.png" 
                });
            }

            UpdateMovieList();
        }

        private void UpdateMovieList()
        {
            var filtered = allMovies.AsQueryable();

            string search = tbSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(search) && search != "поиск по названию...")
            {
                filtered = filtered.Where(m => m.Title.ToLower().Contains(search));
            }

            var selected = cmbSort.SelectedItem as ComboBoxItem;
            if (selected != null)
            {
                string tag = selected.Tag.ToString();

                switch (tag)
                {
                    case "TitleAsc": filtered = filtered.OrderBy(m => m.Title); break;
                    case "TitleDesc": filtered = filtered.OrderByDescending(m => m.Title); break;
                    case "RatingDesc": filtered = filtered.OrderByDescending(m => m.Rating ?? 0); break;
                    case "RatingAsc": filtered = filtered.OrderBy(m => m.Rating ?? 0); break;
                }
            }

            Movies.Clear();
            foreach (var m in filtered)
            {
                Movies.Add(new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Rating = m.Rating,
                    AgeRestriction = m.AgeRestriction,
                    ReleaseDate = m.ReleaseDate,
                    PosterPath = $"/Images/{m.Id}.png"
                });
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMovieList();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) UpdateMovieList();
        }

        private void MovieDetails_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is MovieViewModel selected)
            {
                NavigationService?.Navigate(new MovieDetailsPage(selected.Id));
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new LoginPage());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProfilePage());
        }

        public void ShowProfile()
        {
            btnLogin.Visibility = Visibility.Collapsed;
            btnProfile.Visibility = Visibility.Visible;
        }
    }

    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Rating { get; set; }
        public string AgeRestriction { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PosterPath { get; set; }
    }
}
