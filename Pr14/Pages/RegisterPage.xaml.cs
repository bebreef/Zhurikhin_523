using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pr14.Pages
{

    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        public bool Register(string login, string password, string fullName = "", string email = "", string phone = "",
                             bool showMessages = true)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                if (showMessages)
                    MessageBox.Show("Логин и пароль обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Core.Context.Users.Any(u => u.Login == login.Trim()))
            {
                if (showMessages)
                    MessageBox.Show("Такой логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            var newUser = new Users
            {
                Login = login.Trim(),
                Password = password,           
                FullName = fullName?.Trim(),
                Email = email?.Trim(),
                Phone = phone?.Trim()
            };

            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();

            if (showMessages)
                MessageBox.Show("Регистрация успешна! Теперь можно войти.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService?.Navigate(new LoginPage());
            return true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register(tbLogin.Text, tbPassword.Password, tbFullName.Text, tbEmail.Text, tbPhone.Text, showMessages: true);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainPage());
        }
    }
}