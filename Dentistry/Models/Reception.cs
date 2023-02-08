using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dentistry.Models
{
    /// <summary>
    /// Класс для хранения информации о сущности ПРИЁМ.
    /// </summary>
    public class Reception
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Внешний ключ к сущности ДОКТОР.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Навигационное свойство к сущности ДОКТОР.
        /// </summary>
        public Doctor? Doctor { get; set; }

        /// <summary>
        /// Внешний ключ к сущности ПАЦИЕНТ.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Навигационное свойство к сущности ПАЦИЕНТ.
        /// </summary>
        public Patient? Patient { get; set; }

        /// <summary>
        /// Информация о кабинете.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Cabinet { get; set; }

        /// <summary>
        /// Дата приёма.
        /// </summary>
        [Required]
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Время приёма.
        /// </summary>
        [Required]
        [BindProperty, DataType(DataType.Time)]
        public DateTime Time { get; set; }

        /// <summary>
        /// Информация о статусе приёма.
        /// </summary>
        [Required]
        public string? Status { get; set; }
    }
}