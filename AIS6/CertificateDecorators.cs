namespace AIS6
{
    /// <summary>
    /// Добавляет информацию об ученой степени к сотруднику
    /// </summary>
    public class AcademicDegree : EmployeeDecorator
    {
        /// <summary>
        /// Название диссертации
        /// </summary>
        public string DissertationTitle { get; set; }

        /// <summary>
        /// Год защиты диссертации
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Область наук
        /// </summary>
        public string ScienceArea { get; set; }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="dissertationTitle">Название диссертации</param>
        /// <param name="year">Год защиты</param>
        /// <param name="scienceArea">Область наук</param>
        public AcademicDegree(Employee employee, string dissertationTitle, int year, string scienceArea)
            : base(employee)
        {
            DissertationTitle = dissertationTitle;
            Year = year;
            ScienceArea = scienceArea;
        }

        /// <summary>
        /// Получить информацию о сотруднике с ученой степенью
        /// </summary>
        /// <returns>Расширенная информация о сотруднике</returns>
        public override string GetInfo()
        {
            return $"{_employee.GetInfo()} | Уч. степень: {ScienceArea} ('{DissertationTitle}', {Year}г.)";
        }
    }

    /// <summary>
    /// Добавляет информацию о сертификате английского языка к сотруднику
    /// </summary>
    public class IntermediateEnglishCertificate : EmployeeDecorator
    {
        /// <summary>
        /// Название экзамена/сертификата
        /// </summary>
        public string ExaminationTitle { get; set; }

        /// <summary>
        /// Год получения сертификата
        /// </summary>
        public int YearOfCertificate { get; set; }

        /// <summary>
        /// Конструктор декоратора сертификата английского
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="examinationTitle">Название экзамена</param>
        /// <param name="yearOfCertificate">Год получения</param>
        public IntermediateEnglishCertificate(Employee employee, string examinationTitle, int yearOfCertificate)
            : base(employee)
        {
            ExaminationTitle = examinationTitle;
            YearOfCertificate = yearOfCertificate;
        }

        /// <summary>
        /// Получить информацию о сотруднике с сертификатом английского
        /// </summary>
        /// <returns>Расширенная информация о сотруднике</returns>
        public override string GetInfo()
        {
            return $"{_employee.GetInfo()} | Англ. Intermediate: {ExaminationTitle} ({YearOfCertificate}г.)";
        }
    }
}