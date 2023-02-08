using System.ComponentModel.DataAnnotations;

namespace Dentistry.Models
{
    /// <summary>
    /// Класс для хранения информации о сущности ПАЦИЕНТ.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Фамилия пациента.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Surname { get; set; }
        /// <summary>
        /// Имя пациента.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        /// <summary>
        /// Отчество пациента.
        /// </summary>
        public string? MiddleName { get; set; }
        /// <summary>
        /// Пол пациента.
        /// </summary>
        [Required]
        [MaxLength(1)]
        public string? Gender { get; set; }
        /// <summary>
        /// Категория возраста.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? AgeCategory { get; set; }
        /// <summary>
        /// Номер телефона.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string? Phone { get; set; }
        /// <summary>
        /// Город.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string? City { get; set; }

        /// <summary>
        /// Список ПРИЁМОВ.
        /// </summary>
        public List<Reception> Receptions = new();
    }
}
