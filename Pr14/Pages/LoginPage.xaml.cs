using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pr14.Pages
{
    public partial class LoginPage : Page
    {
        private static int _failedAttempts = 0;
        private string _currentCaptcha = "";

        public LoginPage()
        {
            InitializeComponent();
        }
        public bool Auth(string login, string password, string captchaInput = "", bool showMessages = true)
        {
            login = login?.Trim() ?? "";
            password = password?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                if (showMessages)
                    MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_failedAttempts >= 3)
            {
                if (string.IsNullOrWhiteSpace(captchaInput) ||
                    captchaInput.Trim().ToUpper() != _currentCaptcha.ToUpper())
                {
                    if (showMessages)
                        MessageBox.Show("Введите правильный код!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    GenerateCaptcha();
                    CaptchaPanel.Visibility = Visibility.Visible;
                    return false;
                }
            }

            var user = Core.Context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                _failedAttempts = 0;
                CaptchaPanel.Visibility = Visibility.Collapsed;

                if (showMessages)
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
                _failedAttempts++;
                if (showMessages)
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                if (_failedAttempts >= 3)
                {
                    GenerateCaptcha();
                    CaptchaPanel.Visibility = Visibility.Visible;
                }
                return false;
            }
        }

        private void GenerateCaptcha()
        {
            _currentCaptcha = GenerateRandomCode(6);
            tbCaptchaText.Text = _currentCaptcha;
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Auth(tbLogin.Text, tbPassword.Password, tbCaptcha.Text, showMessages: true);
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