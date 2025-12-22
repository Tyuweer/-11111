namespace AIS6
{
    /// <summary>
    /// Абстрактный декоратор для сотрудника.
    /// Базовый класс для реализации паттерна "Декоратор".
    /// </summary>
    public abstract class EmployeeDecorator : Employee
    {
        /// <summary>
        /// Ссылка на декорируемого сотрудника
        /// </summary>
        protected Employee _employee;

        /// <summary>
        /// Конструктор декоратора
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        protected EmployeeDecorator(Employee employee)
            : base(employee.Name, employee.BaseSalary, employee.BankService)
        {
            _employee = employee;
        }

        /// <summary>
        /// Получить информацию о сотруднике
        /// </summary>
        /// <returns>Информация о сотруднике</returns>
        public override string GetInfo()
        {
            return _employee.GetInfo();
        }

        /// <summary>
        /// Рассчитать зарплату сотрудника
        /// </summary>
        /// <returns>Зарплата сотрудника</returns>
        public override double CalculateSalary()
        {
            return _employee.CalculateSalary();
        }
    }
}