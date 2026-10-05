using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8Variant8
{
    // ==========================================
    // 1. БАЗОВИЙ КЛАС: Transaction
    // ==========================================
    public class Transaction
    {
        public decimal Amount { get; set; }

        public Transaction(decimal amount)
        {
            Amount = amount;
        }

        // Віртуальний метод для поліморфного виконання транзакцій
        public virtual void Execute()
        {
            Console.WriteLine($"[Transaction] Виконується базова транзакція на суму: {Amount:C2}");
        }
    }

    // ==========================================
    // 2. ПОХІДНИЙ КЛАС: Deposit (Поповнення)
    // ==========================================
    public class Deposit : Transaction
    {
        public string DestinationAccount { get; set; }

        public Deposit(decimal amount, string destinationAccount)
            : base(amount)
        {
            DestinationAccount = destinationAccount;
        }

        public override void Execute()
        {
            Console.WriteLine($"[Deposit] Поповнення рахунку {DestinationAccount} на суму {Amount:C2} успішно проведено.");
        }
    }

    // ==========================================
    // 3. ПОХІДНИЙ КЛАС: Withdrawal (Зняття)
    // ==========================================
    public class Withdrawal : Transaction
    {
        public string SourceAccount { get; set; }

        public Withdrawal(decimal amount, string sourceAccount)
            : base(amount)
        {
            SourceAccount = sourceAccount;
        }

        public override void Execute()
        {
            Console.WriteLine($"[Withdrawal] Зняття готівки з рахунку {SourceAccount} на суму {Amount:C2} виконано.");
        }
    }

    // ==========================================
    // 4. ПОХІДНИЙ КЛАС: Transfer (Переказ)
    // ==========================================
    public class Transfer : Transaction
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }

        public Transfer(decimal amount, string fromAccount, string toAccount)
            : base(amount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
        }

        public override void Execute()
        {
            Console.WriteLine($"[Transfer] Переказ {Amount:C2} з рахунку {FromAccount} на рахунок {ToAccount} завершено.");
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

            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №8 (Варіант 8) ===");
            Console.WriteLine("Тема: Поліморфізм: динамічне зв'язування та перевизначення методів\n");

            // 1. Створення колекції об'єктів базового типу List<Transaction>
            List<Transaction> transactions = new List<Transaction>
            {
                new Deposit(1500.50m, "UA12345678901"),
                new Withdrawal(300.00m, "UA12345678901"),
                new Transfer(2500.00m, "UA12345678901", "UA98765432109"),
                new Deposit(4000.00m, "UA98765432109"),
                new Withdrawal(1200.75m, "UA98765432109")
            };

            // 2. Демонстрація поліморфізму (обробка списку в циклі)
            Console.WriteLine("--- ВИКОНАННЯ ТРАНЗАКЦІЙ (Поліморфні виклики) ---");
            decimal totalAmount = 0m;

            foreach (Transaction tx in transactions)
            {
                // Поліморфний виклик: у runtime обирається версія Execute() відповідного похідного класу
                tx.Execute();

                // Агрегація: накопичення загальної суми транзакцій
                totalAmount += tx.Amount;
            }

            // 3. Виведення результатів агрегації
            Console.WriteLine("\n==================================================");
            Console.WriteLine("PIDСУМКОВІ РЕЗУЛЬТАТИ АГРЕГАЦІЇ:");
            Console.WriteLine("==================================================");
            Console.WriteLine($"Загальна кількість транзакцій: {transactions.Count}");
            Console.WriteLine($"Загальний обсяг транзакцій (Total Amount): {totalAmount:C2}");
            Console.WriteLine("==================================================");
        }
    }
}