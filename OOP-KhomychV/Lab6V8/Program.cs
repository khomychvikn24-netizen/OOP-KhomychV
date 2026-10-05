using System;
using System.Text;

namespace Lab6Variant8
{
    // ==========================================
    // 1. БАЗОВИЙ КЛАС: Device
    // ==========================================
    public class Device
    {
        // Приватні поля
        private string _brand;
        private string _model;

        // Властивості
        public string Brand
        {
            get => _brand;
            set => _brand = string.IsNullOrWhiteSpace(value) ? "Unknown Brand" : value;
        }

        public string Model
        {
            get => _model;
            set => _model = string.IsNullOrWhiteSpace(value) ? "Unknown Model" : value;
        }

        // Конструктор
        public Device(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        // Віртуальний метод для перевизначення (override) у похідних классах
        public virtual void PowerOn()
        {
            Console.WriteLine($"[Device] Пристрій {Brand} {Model} вмикається...");
        }

        // Звичайний (невіртуальний) метод для демонстрації приховування (new)
        public string GetDeviceType()
        {
            return "Загальний електронний пристрій (Device)";
        }
    }

    // ==========================================
    // 2. ПОХІДНИЙ КЛАС: Smartphone
    // ==========================================
    public class Smartphone : Device
    {
        public double ScreenSize { get; set; }

        // Конструктор з використанням base(...)
        public Smartphone(string brand, string model, double screenSize)
            : base(brand, model)
        {
            ScreenSize = screenSize;
        }

        // Перевизначення (override) віртуального методу
        public override void PowerOn()
        {
            Console.WriteLine($"[Smartphone] Смартфон {Brand} {Model} ({ScreenSize}\") анімує екран привітання та вмикається.");
        }

        // Власний унікальний метод
        public void MakeCall(string number)
        {
            Console.WriteLine($"[Smartphone] {Brand} {Model} здійснює виклики на номер: {number}...");
        }

        // Приховування (new) невіртуального методу базового класу
        public new string GetDeviceType()
        {
            return "Розумний телефон (Smartphone)";
        }
    }

    // ==========================================
    // 3. ПОХІДНИЙ КЛАС: Laptop
    // ==========================================
    public class Laptop : Device
    {
        public string Processor { get; set; }

        // Конструктор з використанням base(...)
        public Laptop(string brand, string model, string processor)
            : base(brand, model)
        {
            Processor = processor;
        }

        // Перевизначення (override) віртуального методу
        public override void PowerOn()
        {
            Console.WriteLine($"[Laptop] Ноутбук {Brand} {Model} з процесором {Processor} завантажує операційну систему.");
        }

        // Власний унікальний метод
        public void RunProgram(string programName)
        {
            Console.WriteLine($"[Laptop] {Brand} {Model} запускає програму: {programName}.");
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

            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №6 (Варіант 8) ===\n");

            // 1. Створення об'єктів
            Device genericDevice = new Device("Generic", "X1");
            Smartphone phone = new Smartphone("Apple", "iPhone 15 Pro", 6.1);
            Laptop laptop = new Laptop("ASUS", "ROG Zephyrus", "Intel Core i9");

            // 2. Викликаємо власні методи похідних класів
            Console.WriteLine("--- Виклики унікальних методів ---");
            phone.MakeCall("+380971234567");
            laptop.RunProgram("Visual Studio 2022");
            Console.WriteLine();

            // 3. Демонстрація поліморфізму (virtual / override)
            Console.WriteLine("--- Демонстрація поліморфізму (virtual / override) ---");
            Console.WriteLine("Створюємо масив типу Device[] із різними об'єктами:");
            
            Device[] devices = new Device[] { genericDevice, phone, laptop };

            foreach (Device dev in devices)
            {
                // Для кожного об'єкта викликається ЙОГО власна реалізація PowerOn(),
                // незалежно від того, що посилання має тип базового класу Device
                dev.PowerOn();
            }
            Console.WriteLine();

            // 4. Демонстрація різниці між override та new
            Console.WriteLine("--- Демонстрація різниці між override та new ---");

            // А) Через посилання типу Smartphone (пряме посилання на похідний клас)
            Smartphone directPhoneRef = phone;
            Console.WriteLine($"Виклик через Smartphone reference: {directPhoneRef.GetDeviceType()}");

            // Б) Через посилання типу Device (посилання базового класу на той самий об'єкт Smartphone)
            Device basePhoneRef = phone;
            Console.WriteLine($"Виклик через Device reference:     {basePhoneRef.GetDeviceType()}");

            Console.WriteLine("\nПояснення:");
            Console.WriteLine("- 'override' гарантує виклик реалізації ФАКТИЧНОГО об'єкта (динамічна зв'язаність).");
            Console.WriteLine("- 'new' здійснює приховування: метод обирається за ТИПОМ ПОСИЛАННЯ (статична зв'язаність).");
        }
    }
}