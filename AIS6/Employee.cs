namespace AIS6
{
    /// <summary>
    /// Абстрактный класс сотрудника.
    /// Содержит основную информацию.
    /// </summary>
    public abstract class Employee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Базовая зарплата сотрудника
        /// </summary>
        public double BaseSalary { get; set; }

        /// <summary>
        /// Банковский сервис для расчета зарплаты
        /// </summary>
        public IBankService BankService { get; set; }

        /// <summary>
        /// Конструктор базового класса сотрудника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <param name="bankService">Банковский сервис для расчета</param>
        protected Employee(string name, double baseSalary, IBankService bankService)
        {
            Name = name;
            BaseSalary = baseSalary;
            BankService = bankService;
        }

        /// <summary>
        /// Получить информацию о сотруднике
        /// </summary>
        /// <returns>Строка с информацией о сотруднике</returns>
        public virtual string GetInfo()
        {
            return $"{Name} | Базовая зарплата: {BaseSalary:C}";
        }

        /// <summary>
        /// Рассчитать итоговую зарплату с использованием текущего банковского сервиса
        /// </summary>
        /// <returns>Зарплата к выплате</returns>
        public virtual double CalculateSalary()
        {
            return BankService?.CalculateSalary(BaseSalary) ?? BaseSalary;
        }
    }
}