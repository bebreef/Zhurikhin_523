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
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();  
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrWhiteSpace(tbPassword.Password))
            {
                MessageBox.Show("Логин и пароль обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Core.Context.Users.Any(u => u.Login == tbLogin.Text.Trim()))
            {
                MessageBox.Show("Такой логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new Users
            {
                Login = tbLogin.Text.Trim(),
                Password = tbPassword.Password,
                FullName = tbFullName.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                Phone = tbPhone.Text.Trim()
            };

            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();

            MessageBox.Show("Регистрация успешна! Теперь можно войти.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService?.Navigate(new LoginPage());
        }
    }
}
    