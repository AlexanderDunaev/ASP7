using System.ComponentModel.DataAnnotations;

namespace Dentistry.Models
{
    /// <summary>
    /// Класс для хранения информации о сущности ДОКТОР.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Фамилия доктора.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Surname { get; set; } 
        /// <summary>
        /// Имя доктора.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; } 
		/// <summary>
		/// Отчество доктора.
		/// </summary>
		public string? MiddleName { get; set; }
		/// <summary>
		/// Возраст доктора.
		/// </summary>
		[Required]
        public int Age { get; set; }
        /// <summary>
        /// Пол.
        /// </summary>
        [Required]
        [MaxLength(1)]
        public string? Gender { get; set; }
        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Специальность.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string? Speciality { get; set; }
        /// <summary>
        /// Список ПРИЁМОВ.
        /// </summary>
        public List<Reception> Receptions = new();
    }
}
