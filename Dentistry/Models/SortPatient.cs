namespace Dentistry.Models
{
    /// <summary>
    /// Перечисление параметров сортировки модели ПАЦИЕНТ.
    /// </summary>
    public enum SortPatient
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
        /// Параметр сортировки пола.
        /// </summary>
        GenderAsc,
        /// <summary>
        /// Параметр сортировки пола.
        /// </summary>
        GenderDesc,
        /// <summary>
        /// Параметр сортировки категории возраста по возрастанию.
        /// </summary>
        AgeCategoryAsc,
        /// <summary>
        /// Параметр сортировки категории возраста по убыванию.
        /// </summary>
        AgeCategoryDesc,
        /// <summary>
        /// Параметр сортировки номера телефона по возрастанию.
        /// </summary>
        PhoneAsc,
        /// <summary>
        /// Параметр сортировки номера телефона по убыванию.
        /// </summary>
        PhoneDesc,
        /// <summary>
        /// Параметр сортировки города по возрастанию.
        /// </summary>
        CityAsc,
        /// <summary>
        /// Параметр сортировки города по убыванию.
        /// </summary>
        CityDesc
    }
}