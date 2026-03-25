using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pr14.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public bool Auth(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var user = Core.Context.Users
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.FullName ?? user.Login}!", "Успех");
                CurrentUser.Id = user.Id;
                CurrentUser.Login = user.Login;
                CurrentUser.FullName = user.FullName;
                CurrentUser.Email = user.Email;
                CurrentUser.Phone = user.Phone;
                if (NavigationService?.Content is MainPage main)
                    main.ShowProfile();

                NavigationService?.Navigate(new MainPage());
                return true;
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password;
            Auth(login, password);   
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainPage());
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegisterPage());
        }

        public static class CurrentUser
        {
            public static int Id { get; set; }
            public static string Login { get; set; }
            public static string FullName { get; set; }
            public static string Email { get; set; }
            public static string Phone { get; set; }
            public static bool IsLoggedIn => Id > 0;

            public static void Logout()
            {
                Id = 0;
                Login = null;
                FullName = null;
                Email = null;
                Phone = null;
            }
        }
    }
}