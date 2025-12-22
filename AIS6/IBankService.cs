namespace AIS6
{
    /// <summary>
    /// Интерфейс банковского сервиса для расчета зарплаты
    /// </summary>
    public interface IBankService
    {
        /// <summary>
        /// Название банковского сервиса
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Рассчитать итоговую зарплату с учетом комиссии банка
        /// </summary>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <returns>Зарплата к выплате</returns>
        double CalculateSalary(double baseSalary);
    }
}