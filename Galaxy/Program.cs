using System;
using System.Collections.Generic;

namespace Galaxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Galaxy News!");
            IterateThroughList();
            Console.ReadKey();
        }

        /// <summary>
        /// Создаёт список галактик и выводит информацию о каждой из них.
        /// </summary>
        private static void IterateThroughList()
        {
            var theGalaxies = new List<Galaxy>
            {
                new Galaxy() { Name = "Tadpole", MegaLightYears = 400, GalaxyType = new GType('S') },
                new Galaxy() { Name = "Pinwheel", MegaLightYears = 25, GalaxyType = new GType('S') },
                new Galaxy() { Name = "Cartwheel", MegaLightYears = 500, GalaxyType = new GType('L') },
                new Galaxy() { Name = "Small Magellanic Cloud", MegaLightYears = 0.2, GalaxyType = new GType('I') },
                new Galaxy() { Name = "Andromeda", MegaLightYears = 3, GalaxyType = new GType('S') },
                new Galaxy() { Name = "Maffei 1", MegaLightYears = 11, GalaxyType = new GType('E') }
            };

            foreach (Galaxy theGalaxy in theGalaxies)
            {
                Console.WriteLine($"{theGalaxy.Name} {theGalaxy.MegaLightYears}, {theGalaxy.GalaxyType.MyGType}");
            }
        }
    }

    /// <summary>
    /// Представляет галактику с названием, расстоянием и типом.
    /// </summary>
    public class Galaxy
    {
        public string Name { get; set; }
        public double MegaLightYears { get; set; }
        public GType GalaxyType { get; set; }
    }

    /// <summary>
    /// Класс, определяющий тип галактики по символу.
    /// </summary>
    public class GType
    {
        /// <summary>
        /// Создаёт экземпляр типа галактики на основе символа.
        /// </summary>
        /// <param name="type">Символ типа галактики: S, E, I, L</param>
        public GType(char type)
        {
            switch (type)
            {
                case 'S':
                    MyGType = Type.Spiral;
                    break;
                case 'E':
                    MyGType = Type.Elliptical;
                    break;
                case 'I':
                    MyGType = Type.Irregular;
                    break;
                case 'L':
                    MyGType = Type.Lenticular;
                    break;
                default:
                    MyGType = "Unknown";
                    break;
            }
        }

        /// <summary>
        /// Строковое представление типа галактики.
        /// </summary>
        public object MyGType { get; set; }

        private enum Type { Spiral, Elliptical, Irregular, Lenticular }
    }
}