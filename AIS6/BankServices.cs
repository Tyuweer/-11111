namespace AIS6
{
    /// <summary>
    /// Расчет зарплаты через Сбербанк
    /// </summary>
    public class Sberbank : IBankService
    {
        /// <summary>
        /// Название банковского сервиса
        /// </summary>
        public string Name => "Сбербанк";

        /// <summary>
        /// Рассчитать зарплату с комиссией 1%
        /// </summary>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <returns>Зарплата за вычетом 1% комиссии</returns>
        public double CalculateSalary(double baseSalary)
        {
            double commission = baseSalary * 0.01; // 1%
            return baseSalary - commission;
        }
    }

    /// <summary>
    /// Расчет зарплаты через Газпромбанк
    /// </summary>
    public class Gazprombank : IBankService
    {
        /// <summary>
        /// Название банковского сервиса
        /// </summary>
        public string Name => "Газпромбанк";

        /// <summary>
        /// Рассчитать зарплату с комиссией 1.5%
        /// </summary>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <returns>Зарплата за вычетом 1.5% комиссии</returns>
        public double CalculateSalary(double baseSalary)
        {
            double commission = baseSalary * 0.015; // 1.5%
            return baseSalary - commission;
        }
    }
}