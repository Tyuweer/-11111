using System;
using System.Collections.Generic;

namespace AIS6
{
    /// <summary>
    /// Главный класс программы
    /// </summary>
    class Program
    {
        static List<Employee> employees = new List<Employee>();
        static List<IBankService> banks = new List<IBankService>();

        /// <summary>
        /// Точка входа в программу
        /// </summary>
        static void Main()
        {
            banks.Add(new Sberbank());
            banks.Add(new Gazprombank());

            employees.Add(new Engineer("Иван Петров", 80000, banks[0]));
            employees.Add(new Manager("Мария Сидорова", 95000, banks[1]));
            employees.Add(new Scientist("Алексей Козлов", 120000, banks[0]));

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ СОТРУДНИКАМИ ===\n");
                Console.WriteLine("1. Показать всех сотрудников");
                Console.WriteLine("2. Добавить нового сотрудника");
                Console.WriteLine("3. Добавить ученую степень");
                Console.WriteLine("4. Добавить сертификат английского");
                Console.WriteLine("5. Сменить банковский сервис");
                Console.WriteLine("6. Рассчитать зарплаты");
                Console.WriteLine("0. Выйти");
                Console.Write("\nВыберите действие: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowAllEmployees();
                        break;
                    case "2":
                        AddNewEmployee();
                        break;
                    case "3":
                        AddAcademicDegree();
                        break;
                    case "4":
                        AddEnglishCertificate();
                        break;
                    case "5":
                        ChangeBankService();
                        break;
                    case "6":
                        CalculateSalaries();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nВыход из программы...");
                        break;
                    default:
                        Console.WriteLine("\nНеверный выбор!");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Отобразить список всех сотрудников
        /// </summary>
        static void ShowAllEmployees()
        {
            Console.WriteLine("\n=== СПИСОК СОТРУДНИКОВ ===");
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет");
            }
            else
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {employees[i].GetInfo()}");
                }
            }
        }

        /// <summary>
        /// Добавить нового сотрудника в систему
        /// </summary>
        static void AddNewEmployee()
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОГО СОТРУДНИКА ===");

            Console.Write("Введите имя сотрудника: ");
            string name = Console.ReadLine();

            Console.Write("Введите базовую зарплату: ");
            if (!double.TryParse(Console.ReadLine(), out double salary))
            {
                Console.WriteLine("Ошибка: введите корректное число!");
                return;
            }

            Console.WriteLine("\nВыберите должность:");
            Console.WriteLine("1. Инженер");
            Console.WriteLine("2. Менеджер");
            Console.WriteLine("3. Ученый");
            Console.Write("Ваш выбор: ");
            string positionChoice = Console.ReadLine();

            Console.WriteLine("\nВыберите банковский сервис:");
            Console.WriteLine("1. Сбербанк (комиссия 1%)");
            Console.WriteLine("2. Газпромбанк (комиссия 1.5%)");
            Console.Write("Ваш выбор: ");

            if (!int.TryParse(Console.ReadLine(), out int bankChoice) || bankChoice < 1 || bankChoice > 2)
            {
                Console.WriteLine("Ошибка: неверный выбор банка!");
                return;
            }

            Employee newEmployee = null;
            IBankService selectedBank = banks[bankChoice - 1];

            switch (positionChoice)
            {
                case "1":
                    newEmployee = new Engineer(name, salary, selectedBank);
                    break;
                case "2":
                    newEmployee = new Manager(name, salary, selectedBank);
                    break;
                case "3":
                    newEmployee = new Scientist(name, salary, selectedBank);
                    break;
                default:
                    Console.WriteLine("Ошибка: неверный выбор должности!");
                    return;
            }

            employees.Add(newEmployee);
            Console.WriteLine($"\nСотрудник {name} успешно добавлен!");
        }

        /// <summary>
        /// Добавить ученую степень выбранному сотруднику (Декоратор)
        /// </summary>
        static void AddAcademicDegree()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("\nНет сотрудников для добавления ученой степени!");
                return;
            }

            ShowAllEmployees();
            Console.Write("\nВыберите номер сотрудника: ");

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > employees.Count)
            {
                Console.WriteLine("Ошибка: неверный номер!");
                return;
            }

            Console.Write("Введите область наук: ");
            string field = Console.ReadLine();

            Console.Write("Введите тему диссертации: ");
            string topic = Console.ReadLine();

            Console.Write("Введите год защиты: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Ошибка: введите корректный год!");
                return;
            }

            employees[index - 1] = new AcademicDegree(employees[index - 1], topic, year, field);
            Console.WriteLine("\nУченая степень успешно добавлена!");
        }

        /// <summary>
        /// Добавить сертификат английского языка выбранному сотруднику (Декоратор)
        /// </summary>
        static void AddEnglishCertificate()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("\nНет сотрудников для добавления сертификата!");
                return;
            }

            ShowAllEmployees();
            Console.Write("\nВыберите номер сотрудника: ");

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > employees.Count)
            {
                Console.WriteLine("Ошибка: неверный номер!");
                return;
            }

            Console.Write("Введите название экзамена или сертификата: ");
            string exam = Console.ReadLine();

            Console.Write("Введите год получения сертификата: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Ошибка: введите корректный год!");
                return;
            }

            employees[index - 1] = new IntermediateEnglishCertificate(employees[index - 1], exam, year);
            Console.WriteLine("\nСертификат английского успешно добавлен!");
        }

        /// <summary>
        /// Сменить банковский сервис для сотрудника (Стратегия)
        /// </summary>
        static void ChangeBankService()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("\nНет сотрудников для изменения банковского сервиса!");
                return;
            }

            ShowAllEmployees();
            Console.Write("\nВыберите номер сотрудника: ");

            if (!int.TryParse(Console.ReadLine(), out int empIndex) || empIndex < 1 || empIndex > employees.Count)
            {
                Console.WriteLine("Ошибка: неверный номер!");
                return;
            }

            Console.WriteLine("\nВыберите новый банковский сервис:");
            Console.WriteLine("1. Сбербанк (комиссия 1%)");
            Console.WriteLine("2. Газпромбанк (комиссия 1.5%)");
            Console.Write("Ваш выбор: ");

            if (!int.TryParse(Console.ReadLine(), out int bankChoice) || bankChoice < 1 || bankChoice > 2)
            {
                Console.WriteLine("Ошибка: неверный выбор банка!");
                return;
            }

            employees[empIndex - 1].BankService = banks[bankChoice - 1];
            Console.WriteLine("\nБанковский сервис успешно изменен!");
        }

        /// <summary>
        /// Рассчитать и отобразить зарплаты всех сотрудников
        /// </summary>
        static void CalculateSalaries()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("\nНет сотрудников для расчета зарплаты!");
                return;
            }

            Console.WriteLine("\n=== РАСЧЕТ ЗАРПЛАТ ===");
            Console.WriteLine("Имя сотрудника          | Базовая зарплата | К выплате     | Банк");
            Console.WriteLine(new string('-', 70));

            foreach (var emp in employees)
            {
                double netSalary = emp.CalculateSalary();
                string bankName = emp.BankService?.Name ?? "Не указан";

                Console.WriteLine($"{emp.Name,-22} | {emp.BaseSalary,15:C} | {netSalary,12:C} | {bankName}");
            }
        }
    }
}