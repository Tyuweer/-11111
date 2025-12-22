namespace AIS6
{
    /// <summary>
    /// Конкретный класс: сотрудник-инженер
    /// </summary>
    public class Engineer : Employee
    {
        /// <summary>
        /// Конструктор инженера
        /// </summary>
        /// <param name="name">Имя инженера</param>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <param name="bankService">Банковский сервис</param>
        public Engineer(string name, double baseSalary, IBankService bankService)
            : base(name, baseSalary, bankService) { }

        /// <summary>
        /// Получить информацию об инженере
        /// </summary>
        /// <returns>Строка с информацией об инженере</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Должность: Инженер | Банк: {BankService?.Name}";
        }
    }

    /// <summary>
    /// Конкретный класс: сотрудник-менеджер
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Конструктор менеджера
        /// </summary>
        /// <param name="name">Имя менеджера</param>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <param name="bankService">Банковский сервис</param>
        public Manager(string name, double baseSalary, IBankService bankService)
            : base(name, baseSalary, bankService) { }

        /// <summary>
        /// Получить информацию о менеджере
        /// </summary>
        /// <returns>Строка с информацией о менеджере</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Должность: Менеджер | Банк: {BankService?.Name}";
        }
    }

    /// <summary>
    /// Конкретный класс: сотрудник-ученый
    /// </summary>
    public class Scientist : Employee
    {
        /// <summary>
        /// Конструктор ученого
        /// </summary>
        /// <param name="name">Имя ученого</param>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <param name="bankService">Банковский сервис</param>
        public Scientist(string name, double baseSalary, IBankService bankService)
            : base(name, baseSalary, bankService) { }

        /// <summary>
        /// Получить информацию об ученом
        /// </summary>
        /// <returns>Строка с информацией об ученом</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Должность: Ученый | Банк: {BankService?.Name}";
        }
    }
}