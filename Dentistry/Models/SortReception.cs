namespace Dentistry.Models
{
    /// <summary>
    /// Перечисление параметров сортировки модели ПРИЁМ.
    /// </summary>
    public enum SortReception
    {
        /// <summary>
        /// Параметр сортировки доктора по возрастанию.
        /// </summary>
        DoctorAsc,
        /// <summary>
        /// Параметр сортировки доктора по убыванию.
        /// </summary>
        DoctorDesc,
        /// <summary>
        /// Параметр сортировки пациента по возрастанию.
        /// </summary>
        PatientAsc,
        /// <summary>
        /// Параметр сортировки пациента по убыванию.
        /// </summary>
        PatientDesc,
        /// <summary>
        /// Параметр сортировки кабинета по возрастанию.
        /// </summary>
        CabinetAsc,
        /// <summary>
        /// Параметр сортировки кабинета по убыванию.
        /// </summary>
        CabinetDesc,
        /// <summary>
        /// Параметр сортировки даты приёма по возрастанию.
        /// </summary>
        DateAsc,
        /// <summary>
        /// Параметр сортировки даты приёма по убыванию.
        /// </summary>
        DateDesc,
        /// <summary>
        /// Параметр сортировки времени приёма по возрастанию.
        /// </summary>
        TimeAsc,
        /// <summary>
        /// Параметр сортировки времени приёма по убыванию.
        /// </summary>
        TimeDesc,
        /// <summary>
        /// Параметр сортировки статуса по возрастанию.
        /// </summary>
        StatusAsc,
        /// <summary>
        /// Параметр сортировки статуса по убыванию.
        /// </summary>
        StatusDesc
    }
}