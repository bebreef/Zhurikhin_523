using System;
using System.Linq;
using System.Numerics;
using System.Windows;
using Zhurikhin_523.Core;

namespace Zhurikhin_523
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml.
    /// Обеспечивает валидацию, обработку исключений и вызов модулей шифрования/дешифрования.
    /// </summary>
    public partial class MainWindow : Window
    {
        private (BigInteger e, BigInteger d, BigInteger n) keys;
        private bool keysGenerated;

        /// <summary>
        /// Инициализация окна. Кнопки шифрования/дешифрования отключены до генерации ключей.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            keysGenerated = false;
        }

        /// <summary>
        /// Генерация ключей RSA с проверкой ввода простых чисел.
        /// </summary>
        private void btnGenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtP.Text, out int p) || !int.TryParse(txtQ.Text, out int q))
                {
                    ShowWarning("В поля p и q необходимо ввести целые числа.");
                    return;
                }

                keys = RsaCipher.GenerateKeys(p, q);
                keysGenerated = true;
                btnEncrypt.IsEnabled = true;
                btnDecrypt.IsEnabled = true;
                lblStatus.Text = $"Ключи сгенерированы: n={keys.n}, e={keys.e}, d={keys.d}";
            }
            catch (ArgumentException ex)
            {
                lblStatus.Text = "Ошибка генерации ключей.";
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Шифрование текста посимвольно с использованием открытого ключа.
        /// </summary>
        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!keysGenerated) { ShowWarning("Сначала сгенерируйте ключи!"); return; }
                if (string.IsNullOrWhiteSpace(txtInput.Text))
                {
                    ShowWarning("Введите текст для шифрования.");
                    return;
                }

                var encrypted = RsaCipher.Encrypt(txtInput.Text, keys.e, keys.n);
                txtOutput.Text = string.Join(" ", encrypted.Select(c => c.ToString()));
                lblStatus.Text = "Шифрование завершено успешно.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка шифрования.";
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Дешифрование последовательности чисел обратно в текст.
        /// </summary>
        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!keysGenerated) { ShowWarning("Сначала сгенерируйте ключи!"); return; }
                if (string.IsNullOrWhiteSpace(txtInput.Text))
                {
                    ShowWarning("Введите зашифрованные числа, разделённые пробелами.");
                    return;
                }

                var parts = txtInput.Text.Split(new[] { ' ', '\n', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var encrypted = parts.Select(p => BigInteger.Parse(p)).ToList();

                txtOutput.Text = RsaCipher.Decrypt(encrypted, keys.d, keys.n);
                lblStatus.Text = "Дешифрование завершено успешно.";
            }
            catch (FormatException)
            {
                lblStatus.Text = "Ошибка формата данных.";
                ShowError("Неверный формат чисел. Введите целые числа, разделённые пробелами.");
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка дешифрования.";
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Отображение предупреждающего сообщения.
        /// </summary>
        private void ShowWarning(string message) =>
            MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

        /// <summary>
        /// Отображение сообщения об ошибке.
        /// </summary>
        private void ShowError(string message) =>
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}