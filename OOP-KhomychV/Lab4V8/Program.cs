using System;

namespace OOPLab4
{
    public class Rectangle
    {
        // Приватні поля
        private double _width;
        private double _height;

        // Конструктор
        public Rectangle(double width, double height)
        {
            Width = width;   // Валідація спрацьовує у сеттерах
            Height = height;
        }

        // Властивості з валідацією (> 0)
        public double Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Ширина повинна бути більшою за 0.", nameof(value));
                _width = value;
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Висота повинна бути більшою за 0.", nameof(value));
                _height = value;
            }
        }

        // Властивість для обчислення площі
        public double Area => Width * Height;

        // Статичний член класу
        public static Rectangle DefaultRectangle => new Rectangle(1, 1);

        // Індексатор: 0 — ширина, 1 — висота
        public double this[int index]
        {
            get => index switch
            {
                0 => Width,
                1 => Height,
                _ => throw new IndexOutOfRangeException("Індекс може бути лише 0 (Ширина) або 1 (Висота).")
            };
            set
            {
                switch (index)
                {
                    case 0: Width = value; break;
                    case 1: Height = value; break;
                    default: throw new IndexOutOfRangeException("Індекс може бути лише 0 (Ширина) або 1 (Висота).");
                }
            }
        }

        // Множення прямокутника на скаляр (зміна розмірів)
        public static Rectangle operator *(Rectangle rect, double scalar)
        {
            if (scalar <= 0)
                throw new ArgumentException("Скаляр для масштабування має бути більшим за 0.", nameof(scalar));

            return new Rectangle(rect.Width * scalar, rect.Height * scalar);
        }

        public static Rectangle operator *(double scalar, Rectangle rect)
        {
            return rect * scalar;
        }

        // Оператори порівняння (порівняння за площею)
        public static bool operator ==(Rectangle r1, Rectangle r2)
        {
            if (ReferenceEquals(r1, r2)) return true;
            if (r1 is null || r2 is null) return false;
            return r1.Equals(r2);
        }

        public static bool operator !=(Rectangle r1, Rectangle r2)
        {
            return !(r1 == r2);
        }

        // Перевизначення методів класу Object
        public override bool Equals(object obj)
        {
            if (obj is Rectangle other)
            {
                // Для чисел із плаваючою крапкою використовуємо невелику похибку (Epsilon)
                return Math.Abs(Area - other.Area) < 1e-9;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Area);
        }

        public override string ToString()
        {
            return $"Rectangle [Width = {Width}, Height = {Height}, Area = {Area}]";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Демонстрація роботи класу Rectangle (Варіант 8) ===\n");

            // 1. Створення об'єктів та використання статичного члена
            Rectangle r1 = new Rectangle(4, 5);
            Rectangle r2 = new Rectangle(2, 10);
            Rectangle defaultRect = Rectangle.DefaultRectangle;

            Console.WriteLine($"Прямокутник 1: {r1}");
            Console.WriteLine($"Прямокутник 2: {r2}");
            Console.WriteLine($"За замовчуванням (Rectangle.DefaultRectangle): {defaultRect}\n");

            // 2. Демонстрація валідації властивостей
            Console.WriteLine("--- Перевірка валідації ---");
            try
            {
                Console.WriteLine("Спроба встановити ширину <= 0...");
                r1.Width = -3;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка валідації: {ex.Message}\n");
            }

            // 3. Демонстрація використання індексатора
            Console.WriteLine("--- Використання індексатора ---");
            Console.WriteLine($"r1[0] (Ширина): {r1[0]}, r1[1] (Висота): {r1[1]}");
            r1[0] = 6; // змінюємо ширину через індексатор
            Console.WriteLine($"Після змінення r1[0] = 6: {r1}\n");

            // 4. Перевантажені оператори
            Console.WriteLine("--- Перевантажений оператор множення на скаляр (*) ---");
            Rectangle scaledRect = r1 * 2.5;
            Console.WriteLine($"Множення r1 на 2.5: {scaledRect}\n");

            // 5. Порівняння за площею (==, !=, Equals)
            Console.WriteLine("--- Порівняння прямокутників за площею ---");
            // r1 (6x5, площа = 30), r2 (2x10, площа = 20)
            Console.WriteLine($"r1 ({r1.Area}) == r2 ({r2.Area}): {r1 == r2}");

            Rectangle r3 = new Rectangle(3, 10); // площа = 30
            Console.WriteLine($"Створено r3 (3x10, площа = {r3.Area})");
            Console.WriteLine($"r1 ({r1.Area}) == r3 ({r3.Area}): {r1 == r3}");
            Console.WriteLine($"r1.Equals(r3): {r1.Equals(r3)}");
        }
    }
}