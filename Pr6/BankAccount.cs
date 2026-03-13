using System;

namespace BankAccountNS
{
    /// <summary>
    /// Класс представляет собой банковский счёт клиента
    /// Содержит информацию о владельце счёта и текущем балансе, а также методы для пополнения и снятия денежных средств
    /// </summary>
    public class BankAccount
    {
        private readonly string m_customerName;
        private double m_balance;
       
        /// <summary>
        /// Закрытый конструктор по умолчанию, запрещающий создание объекта без указания данных
        /// </summary>
        private BankAccount() { }

        /// <summary>
        /// Инициализирует новый экземпляр банковского счёта с указанием имени владельца и начального баланса
        /// </summary>
        /// <param name="customerName">ФИО или название владельца счёта</param>
        /// <param name="balance">Начальная сумма на счёте (может быть 0)</param>
        /// <exception cref="ArgumentNullException">Вбрасывается, если customerName = null</exception>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Получает имя владельца банковского счёта
        /// </summary>
        public string CustomerName
        {
            get { return m_customerName; }
        }

        /// <summary>
        /// Получает текущий баланс на счёте
        /// </summary>
        public double Balance
        {
            get { return m_balance; }
        }

        /// <summary>
        /// Снимает указанную сумму со счёта
        /// </summary>
        /// <param name="amount">Сумма, которую необходимо снять со счёта</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Вбрасывается, если:
        /// • amount меньше нуля
        /// • amount больше текущего баланса - недостаточно средств
        /// </exception>
        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        /// <summary>
        /// Пополняет счёт на указанную сумму
        /// </summary>
        /// <param name="amount">Сумма пополнения</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если amount меньше нуля
        /// </exception>
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        /// <summary>
        /// Точка входа в консольное приложение
        /// Создаёт тестовый счёт, выполняет несколько операций и выводит итоговый баланс
        /// </summary>
        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Roman Abramovich", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
            Console.ReadLine();
        }
    }
}
