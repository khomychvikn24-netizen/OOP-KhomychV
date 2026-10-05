using System;
using System.Text;

namespace Lab7Variant8
{
    // ==========================================
    // 1. БАЗОВИЙ КЛАС: Account
    // ==========================================
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public Account(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        // Віртуальний метод базового класу
        public virtual void CalculateInterest()
        {
            Console.WriteLine($"[Account] Нарахування відсотків для рахунку {AccountNumber}: базовий відсоток не визначено.");
        }
    }

    // ==========================================
    // 2. ПОХІДНИЙ КЛАС A: SavingsAccount (override)
    // ==========================================
    public class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; } // Наприклад, 0.05 для 5%

        public SavingsAccount(string accountNumber, decimal balance, decimal interestRate)
            : base(accountNumber, balance)
        {
            InterestRate = interestRate;
        }

        // Перевизначення (override) віртуального методу
        public override void CalculateInterest()
        {
            decimal interest = Balance * InterestRate;
            Console.WriteLine($"[SavingsAccount (override)] Ощадний рахунок {AccountNumber}: відсотки за ставкою {InterestRate * 100}% = {interest:C2}.");
        }
    }

    // ==========================================
    // 3. ПОХІДНИЙ КЛАС B: CheckingAccount (new)
    // ==========================================
    public class CheckingAccount : Account
    {
        public decimal ServiceFee { get; set; }

        public CheckingAccount(string accountNumber, decimal balance, decimal serviceFee)
            : base(accountNumber, balance)
        {
            ServiceFee = serviceFee;
        }

        // Приховування (new) методу базового класу
        public new void CalculateInterest()
        {
            Console.WriteLine($"[CheckingAccount (new)] Розрахунковий рахунок {AccountNumber} не передбачає нарахування відсотків. Комісія за обслуговування: {ServiceFee:C2}.");
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

            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №7 (Варіант 8) ===");
            Console.WriteLine("Тема: Приховування методів (new) vs перевизначення (override)\n");

            // 1. Створення екземплярів класів
            Account baseAccount = new Account("ACC-000", 1000m);
            SavingsAccount savingsAcc = new SavingsAccount("SAV-001", 5000m, 0.05m);
            CheckingAccount checkingAcc = new CheckingAccount("CHK-002", 2000m, 15m);

            // 2. Збереження об'єктів у змінних типу базового класу (Upcasting)
            Account account1 = savingsAcc;   // SavingsAccount збережено як Account
            Account account2 = checkingAcc;  // CheckingAccount збережено як Account

            // 3. Виклики через посилання БАЗОВОГО типу (Account)
            Console.WriteLine("==================================================");
            Console.WriteLine("1. ВИКЛИК МЕТОДІВ ЧЕРЕЗ ПОСИЛАННЯ БАЗОВОГО ТИПУ (Account)");
            Console.WriteLine("==================================================");
            
            Console.Write("account1.CalculateInterest() -> ");
            account1.CalculateInterest(); 
            // Результат: Викликається SavingsAccount.CalculateInterest() (динамічне зв'язування завдяки override)

            Console.Write("account2.CalculateInterest() -> ");
            account2.CalculateInterest(); 
            // Результат: Викликається Account.CalculateInterest() (статичне зв'язування через new)

            // 4. Виклики через посилання ПОХІДНИХ типів (з явним приведенням)
            Console.WriteLine("\n==================================================");
            Console.WriteLine("2. ВИКЛИК МЕТОДІВ ЧЕРЕЗ ПОСИЛАННЯ ПОХІДНИХ ТИПІВ");
            Console.WriteLine("==================================================");

            Console.Write("((SavingsAccount)account1).CalculateInterest()  -> ");
            ((SavingsAccount)account1).CalculateInterest();

            Console.Write("((CheckingAccount)account2).CalculateInterest() -> ");
            ((CheckingAccount)account2).CalculateInterest();

            // 5. Пояснення результатів
            Console.WriteLine("\n==================================================");
            Console.WriteLine("ПОЯСНЕННЯ РІЗНИЦІ БЕЗПОСЕРЕДНЬО В КОНСОЛІ:");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Для SavingsAccount (override):");
            Console.WriteLine("   Незалежно від того, викликається метод через посилання типу Account чи SavingsAccount,");
            Console.WriteLine("   ЗАВЖДИ виконується код похідного класу. Це демонструє ПОЛІМОРФІЗМ.");
            Console.WriteLine("\n2. Для CheckingAccount (new):");
            Console.WriteLine("   - При виклику через посилання Account виконується метод БАЗОВОГО класу.");
            Console.WriteLine("   - При виклику через посилання CheckingAccount виконується метод ПОХІДНОГО класу.");
            Console.WriteLine("   Це приховування (new) без поліморфізму — метод обирається за типом посилання!");
        }
    }
}