using System;
using System.Text;

namespace IndependentWork2
{
    public class Product
    {
        // Приватні поля
        private readonly int _id;
        private readonly string _name;
        private readonly decimal _price;
        private readonly string _category;
        private readonly int _stockCount;

        // Публічні read-only властивості
        public int Id => _id;
        public string Name => _name;
        public decimal Price => _price;
        public string Category => _category;
        public int StockCount => _stockCount;

        // 1. Основний конструктор (приймає всі 5 параметрів)
        public Product(int id, string name, decimal price, string category, int stockCount)
        {
            _id = id;
            _name = name;
            _price = price;
            _category = category;
            _stockCount = stockCount;
        }

        // 2. Скорочений конструктор (для швидкого додавання)
        // Викликає основний конструктор за допомогою : this(...)
        public Product(int id, string name, decimal price)
            : this(id, name, price, "Uncategorized", 0)
        {
        }

        // 3. Конструктор копіювання
        // Викликає основний конструктор, передаючи дані з об'єкта-джерела
        public Product(Product other)
            : this(other.Id, other.Name, other.Price, other.Category, other.StockCount)
        {
        }

        // Перевизначення методу ToString()
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price:C}, Category: {Category}, Stock: {StockCount}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Налаштування виводу для коректного відображення символу гривні (₴)
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Створення товарів\n");

            // 1. Використання основного конструктора
            Product product1 = new Product(101, "Laptop", 35000.00m, "Electronics", 15);
            Console.WriteLine($"Товар 1 (основний конструктор): {product1}");

            // 2. Використання скороченого конструктора
            Product product2 = new Product(102, "Mouse", 800.00m);
            Console.WriteLine($"Товар 2 (скорочений конструктор): {product2}");

            // 3. Використання конструктора копіювання
            Product product3 = new Product(product1);
            Console.WriteLine($"Товар 3 (конструктор копіювання): {product3}");
        }
    }
}