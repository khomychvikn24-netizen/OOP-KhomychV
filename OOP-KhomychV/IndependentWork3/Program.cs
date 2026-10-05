using System;
using System.Text;

namespace IndependentWork3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("САМОСТІЙНА РОБОТА №3: АНАЛІЗ ІНКАПСУЛЯЦІЇ");
            Console.WriteLine("Тема: Аналіз open-source проєкту ASP.NET Core (dotnet/aspnetcore)\n");

            Console.WriteLine("1. Проаналізовані класи:");
            Console.WriteLine("   - HeaderDictionary (використання індексаторів та захист IsReadOnly)");
            Console.WriteLine("   - CookieOptions (валідація у set-аксесорі для MaxAge)");
            Console.WriteLine("   - PathString (незмінність / immutability, read-only властивості)\n");

            Console.WriteLine("2. Знайдені практики валідації:");
            Console.WriteLine("   - Перевірка діапазонів із викиданням ArgumentOutOfRangeException");
            Console.WriteLine("   - Захист стану об'єкта за допомогою виклику ThrowIfReadOnly()");
            Console.WriteLine("   - Принцип Fail-Fast для збереження цілісності даних\n");

            Console.WriteLine("Повний звіт із посиланнями на джерела розміщено у файлі REPORT.md.");
        }
    }
}