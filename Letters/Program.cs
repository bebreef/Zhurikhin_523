using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letters
{
    internal class Program
    {
        /// <summary>
        /// Главный метод программы. Формирует имя из массива символов и выводит приветствия.
        /// </summary>
        static void Main()
        {
            char[] letters = { 'f', 'r', 'e', 'd', ' ', 's', 'm', 'i', 't', 'h' };
            string name = "";
            int[] a = new int[10];
            for (int i = 0; i < letters.Length; i++)
            {
                name += letters[i];
                a[i] = i + 1;
                SendMessage(name, a[i]);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит приветствие с именем и номером.
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="msg">Номер для счёта</param>
        static void SendMessage(string name, int msg)
        {
            Console.WriteLine("Hello, " + name + "! Count to " + msg);
        }
    }
}
