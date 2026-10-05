using System;
using System.Text;

namespace Lab5Variant8
{
    public class Polynomial
    {
        // Приватне поле для зберігання коефіцієнтів (індекс = степінь)
        private double[] _coefficients;

        // Конструктори
        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null || coefficients.Length == 0)
            {
                _coefficients = new double[] { 0 };
            }
            else
            {
                _coefficients = (double[])coefficients.Clone();
                TrimLeadingZeros();
            }
        }

        // Властивість для отримання степеня полінома
        public int Degree => _coefficients.Length - 1;

        // Індексатор: доступ до коефіцієнта за степенем (this[degree])
        public double this[int degree]
        {
            get
            {
                if (degree < 0)
                    throw new ArgumentOutOfRangeException(nameof(degree), "Степінь не може бути від'ємною.");

                if (degree >= _coefficients.Length)
                    return 0.0; // Повертаємо 0 для степенів, вищих за поточний

                return _coefficients[degree];
            }
            set
            {
                if (degree < 0)
                    throw new ArgumentOutOfRangeException(nameof(degree), "Степінь не може бути від'ємною.");

                if (degree >= _coefficients.Length)
                {
                    // Розширюємо масив, якщо записуємо значення в новий степінь
                    Array.Resize(ref _coefficients, degree + 1);
                }

                _coefficients[degree] = value;
                TrimLeadingZeros();
            }
        }

        // Обчислення значення полінома P(x) (Схема Горнера)
        public double Evaluate(double x)
        {
            double result = 0;
            for (int i = _coefficients.Length - 1; i >= 0; i--)
            {
                result = result * x + _coefficients[i];
            }
            return result;
        }

        // Обрізання зайвих нульових коефіцієнтів при старших степенях
        private void TrimLeadingZeros()
        {
            int lastNonZero = _coefficients.Length - 1;
            while (lastNonZero > 0 && Math.Abs(_coefficients[lastNonZero]) < 1e-9)
            {
                lastNonZero--;
            }

            if (lastNonZero < _coefficients.Length - 1)
            {
                Array.Resize(ref _coefficients, lastNonZero + 1);
            }
        }

        // Оператор додавання двох поліномів (+)
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            if (p1 is null) return p2;
            if (p2 is null) return p1;

            int maxDegree = Math.Max(p1.Degree, p2.Degree);
            double[] resultCoeffs = new double[maxDegree + 1];

            for (int i = 0; i <= maxDegree; i++)
            {
                resultCoeffs[i] = p1[i] + p2[i];
            }

            return new Polynomial(resultCoeffs);
        }

        // Оператор множення полінома на скаляр (*)
        public static Polynomial operator *(Polynomial p, double scalar)
        {
            if (p is null) return null;

            double[] resultCoeffs = new double[p._coefficients.Length];
            for (int i = 0; i < p._coefficients.Length; i++)
            {
                resultCoeffs[i] = p._coefficients[i] * scalar;
            }

            return new Polynomial(resultCoeffs);
        }

        public static Polynomial operator *(double scalar, Polynomial p) => p * scalar;

        // Оператори порівняння (== та !=)
        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 is null || p2 is null) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Polynomial p1, Polynomial p2) => !(p1 == p2);

        // Перевизначення методів класу Object
        public override bool Equals(object obj)
        {
            if (obj is Polynomial other)
            {
                if (Degree != other.Degree) return false;

                for (int i = 0; i <= Degree; i++)
                {
                    if (Math.Abs(this[i] - other[i]) > 1e-9)
                        return false;
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            foreach (var coeff in _coefficients)
            {
                hash.Add(coeff);
            }
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            if (_coefficients.Length == 0 || (_coefficients.Length == 1 && Math.Abs(_coefficients[0]) < 1e-9))
                return "0";

            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            for (int i = Degree; i >= 0; i--)
            {
                double coeff = _coefficients[i];
                if (Math.Abs(coeff) < 1e-9) continue;

                if (!isFirst && coeff > 0)
                    sb.Append(" + ");
                else if (coeff < 0)
                    sb.Append(isFirst ? "-" : " - ");

                double absCoeff = Math.Abs(coeff);

                if (i == 0 || Math.Abs(absCoeff - 1.0) > 1e-9)
                {
                    sb.Append(absCoeff);
                }

                if (i > 0)
                {
                    sb.Append("x");
                    if (i > 1) sb.Append($"^{i}");
                }

                isFirst = false;
            }

            return sb.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Демонстрація роботи класу Polynomial (Варіант 8) ===\n");

            // 1. Створення об'єктів (коефіцієнти від 0-го степеня до старшого)
            // p1 = 2 + 3x + x^2
            Polynomial p1 = new Polynomial(2, 3, 1);
            // p2 = -1 + 4x^2 - 2x^3
            Polynomial p2 = new Polynomial(-1, 0, 4, -2);

            Console.WriteLine($"Поліном P1(x): {p1}");
            Console.WriteLine($"Поліном P2(x): {p2}");

            // 2. Демонстрація використання індексатора (читання та запис)
            Console.WriteLine("\n--- Робота з індексатором (this[int degree]) ---");
            Console.WriteLine($"Коефіцієнт при x^1 у P1: {p1[1]}");
            Console.WriteLine($"Коефіцієнт при x^5 у P1 (неіснуючий степінь): {p1[5]}");

            Console.WriteLine("\nЗмінюємо коефіцієнт при x^3 у P1 на 5 via індексатор...");
            p1[3] = 5; // Автоматичне розширення масиву
            Console.WriteLine($"Новий P1(x): {p1}");

            // 3. Додатковий метод Evaluate(x)
            Console.WriteLine("\n--- Обчислення значення полінома Evaluate(x) ---");
            double x = 2.0;
            Console.WriteLine($"P1({x}) = {p1.Evaluate(x)}");

            // 4. Перевантажені оператори (+ та *)
            Console.WriteLine("\n--- Перевантажені оператори (+, *) ---");
            Polynomial p3 = new Polynomial(1, 2, 3); // 1 + 2x + 3x^2
            Polynomial p4 = new Polynomial(3, -1);   // 3 - x

            Console.WriteLine($"P3(x): {p3}");
            Console.WriteLine($"P4(x): {p4}");

            Polynomial sum = p3 + p4;
            Console.WriteLine($"Додавання (P3 + P4): {sum}");

            Polynomial scaled = p3 * 3.5;
            Console.WriteLine($"Множення на скаляр (P3 * 3.5): {scaled}");

            // 5. Порівняння об'єктів (==, !=, Equals)
            Console.WriteLine("\n--- Порівняння поліномів (==, !=, Equals) ---");
            Polynomial p5 = new Polynomial(1, 2, 3);
            Console.WriteLine($"P3 == P5 (однакові коефіцієнти): {p3 == p5}");
            Console.WriteLine($"P3 == P4: {p3 == p4}");
            Console.WriteLine($"P3.Equals(P5): {p3.Equals(p5)}");
        }
    }
}