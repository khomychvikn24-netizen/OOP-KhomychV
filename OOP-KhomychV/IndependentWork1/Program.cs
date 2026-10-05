using System;
using System.Text;

namespace IndependentWork1
{
    // ==========================================
    // 1. КЛАС: Course (Навчальний курс)
    // ==========================================
    public class Course
    {
        // Приватні поля
        private string _title;
        private int _totalHours;
        private int _completedHours;

        // Публічні властивості (одна read-only)
        public string Title
        {
            get => _title;
            private set => _title = string.IsNullOrWhiteSpace(value) ? "Без назви" : value;
        }

        public int TotalHours
        {
            get => _totalHours;
            private set => _totalHours = value > 0 ? value : 10;
        }

        // Read-only властивість для обчислення відсотка прогресу
        public double ProgressPercentage => (double)_completedHours / TotalHours * 100;

        // Конструктор
        public Course(string title, int totalHours, int completedHours = 0)
        {
            Title = title;
            TotalHours = totalHours;
            _completedHours = Math.Clamp(completedHours, 0, TotalHours);
        }

        // Метод з обчисленням
        public void CompleteHours(int hours)
        {
            if (hours > 0)
            {
                _completedHours = Math.Min(_completedHours + hours, TotalHours);
                Console.WriteLine($"[Course] Додано {hours} год. Завершено {_completedHours} з {TotalHours} год. Прогрес: {ProgressPercentage:F1}%.");
            }
        }
    }

    // ==========================================
    // 2. КЛАС: BankAccount (Банківський рахунок)
    // ==========================================
    public class BankAccount
    {
        // Приватні поля
        private string _accountNumber;
        private string _ownerName;
        private decimal _balance;

        // Публічні властивості
        public string AccountNumber
        {
            get => _accountNumber;
            private set => _accountNumber = value;
        }

        public string OwnerName
        {
            get => _ownerName;
            set => _ownerName = string.IsNullOrWhiteSpace(value) ? "Невідомо" : value;
        }

        // Read-only властивість для балансу
        public decimal Balance => _balance;

        // Конструктор
        public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            OwnerName = ownerName;
            _balance = Math.Max(0, initialBalance);
        }

        // Метод логіки: поповнення рахунку
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine($"[BankAccount] Помилка: сума поповнення має бути більшою за 0.");
                return false;
            }

            _balance += amount;
            Console.WriteLine($"[BankAccount] Рахунок {_accountNumber} поповнено на {amount:C2}. Поточний баланс: {_balance:C2}.");
            return true;
        }

        // Метод логіки: зняття коштів з перевіркою стану
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > _balance)
            {
                Console.WriteLine($"[BankAccount] Відхилено: недостатньо коштів або некоректна сума ({amount:C2}).");
                return false;
            }

            _balance -= amount;
            Console.WriteLine($"[BankAccount] Знято {amount:C2} з рахунку {_accountNumber}. Залишок: {_balance:C2}.");
            return true;
        }
    }

    // ==========================================
    // 3. КЛАС: SmartLamp (Розумна лампа)
    // ==========================================
    public class SmartLamp
    {
        // Приватні поля
        private string _model;
        private bool _isOn;
        private int _brightness; // 0-100%

        // Публічні властивості
        public string Model
        {
            get => _model;
            private set => _model = value;
        }

        public bool IsOn => _isOn;

        public int Brightness
        {
            get => _brightness;
            set => _brightness = Math.Clamp(value, 0, 100);
        }

        // Конструктор
        public SmartLamp(string model, int initialBrightness = 50)
        {
            _model = model;
            Brightness = initialBrightness;
            _isOn = false;
        }

        // Методи змінення стану
        public void TogglePower()
        {
            _isOn = !_isOn;
            string state = _isOn ? "УВІМКНЕНО" : "ВИМКНЕНО";
            Console.WriteLine($"[SmartLamp] Лампа '{_model}' тепер {state} (Яскравість: {_brightness}%).");
        }

        public void PrintStatus()
        {
            Console.WriteLine($"[SmartLamp] Модель: {_model} | Стан: {(_isOn ? "Увімкнено" : "Вимкнено")} | Яскравість: {_brightness}%");
        }
    }

    // ==========================================
    // ГОЛОВНИЙ КЛАС ДЕМОНСТРАЦІЇ
    // ==========================================
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== САМОСТІЙНА РОБОТА №1 ===");
            Console.WriteLine("Тема: Базовий синтаксис C# і оголошення класів\n");

            // 1. Демонстрація класу Course
            Console.WriteLine("--- 1. Об'єкт класу Course ---");
            Course oopCourse = new Course("Об'єктно-орієнтоване програмування", 120, 30);
            Console.WriteLine($"Курс: '{oopCourse.Title}', Тривалість: {oopCourse.TotalHours} год.");
            oopCourse.CompleteHours(15);
            oopCourse.CompleteHours(25);
            Console.WriteLine();

            // 2. Демонстрація класу BankAccount
            Console.WriteLine("--- 2. Об'єкт класу BankAccount ---");
            BankAccount account = new BankAccount("UA89300001000002600123456789", "Олексій Петров", 1500.00m);
            Console.WriteLine($"Власник: {account.OwnerName}, Початковий баланс: {account.Balance:C2}");
            account.Deposit(500.00m);
            account.Withdraw(300.00m);
            account.Withdraw(2500.00m); // Демонстрація перевірки балансу
            Console.WriteLine();

            // 3. Демонстрація класу SmartLamp
            Console.WriteLine("--- 3. Об'єкт класу SmartLamp ---");
            SmartLamp deskLamp = new SmartLamp("Xiaomi Mi Smart LED", 75);
            deskLamp.PrintStatus();
            deskLamp.TogglePower();
            deskLamp.Brightness = 100;
            deskLamp.PrintStatus();
            deskLamp.TogglePower();
        }
    }
}