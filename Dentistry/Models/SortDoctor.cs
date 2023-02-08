namespace Dentistry.Models
{
    /// <summary>
    /// Перечисление параметров сортировки модели ДОКТОР.
    /// </summary>
    public enum SortDoctor
    {
        /// <summary>
        /// Параметр сортировки фамилии по возрастанию.
        /// </summary>
        SurnameAsc,
        /// <summary>
        /// Параметр сортировки фамилии по убыванию.
        /// </summary>
        SurnameDesc,
        /// <summary>
        /// Параметр сортировки имени по возрастанию.
        /// </summary>
        NameAsc,
        /// <summary>
        /// Параметр сортировки имени по убыванию.
        /// </summary>
        NameDesc,
        /// <summary>
        /// Параметр сортировки отчества по возрастанию.
        /// </summary>
        MiddleNameAsc,
        /// <summary>
        /// Параметр сортировки отчества по убыванию.
        /// </summary>
        MiddleNameDesc,
        /// <summary>
        /// Параметр сортировки возраста по возрастанию.
        /// </summary>
        AgeAsc,
        /// <summary>
        /// Параметр сортировки возраста по убыванию.
        /// </summary>
        AgeDesc,
        /// <summary>
        /// Параметр сортировки пола.
        /// </summary>
        GenderAsc,
        /// <summary>
        /// Параметр сортировки пола.
        /// </summary>
        GenderDesc,
        /// <summary>
        /// Параметр сортировки номера телефона по возрастанию.
        /// </summary>
        PhoneAsc,
        /// <summary>
        /// Параметр сортировки номера телефона по убыванию.
        /// </summary>
        PhoneDesc,
        /// <summary>
        /// Параметр сортировки специальности по возрастанию.
        /// </summary>
        SpecialityAsc,
        /// <summary>
        /// Параметр сортировки специальности по убыванию.
        /// </summary>
        SpecialityDesc
    }
}