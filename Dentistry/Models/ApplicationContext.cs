using Dentistry.Models;
using Microsoft.EntityFrameworkCore;

namespace Dentistry.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Reception> Receptions => Set<Reception>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Настройка логирования.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        /// <summary>
        /// Инициализация БД начальными данными.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Doctor doctor1 = new()
            {
                Id = 1,
                Surname = "Иванов",
                Name = "Иван",
                MiddleName = "Иванович",
                Age = 45,
                Gender = "М",
                Phone = "79814679",
                Speciality = "Стоматолог"
            };
            Doctor doctor2 = new()
            {
                Id = 2,
                Surname = "Петрова",
                Name = "Мария",
                MiddleName = "Геннадьевна",
                Age = 32,
                Gender = "Ж",
                Phone = "79814680",
                Speciality = "Стоматолог-хирург"
            };

            Patient patient1 = new()
            {
                Id = 1,
                Surname = "Сидоров",
                Name = "Андрей",
                MiddleName = "Петрович",
                Gender = "М",
                AgeCategory = "Ребёнок",
                Phone = "665865",
                City = "Городовище"
            };

            Patient patient2 = new()
            {
                Id = 2,
                Surname = "Волков",
                Name = "Алексей",
                MiddleName = "Дмитриевич",
                Gender = "М",
                AgeCategory = "Пенсионер",
                Phone = "665870",
                City = "Томск"
            };

            Reception reception1 = new()
            {
                Id = 1,
                DoctorId = doctor1.Id,
                PatientId = patient1.Id,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.ToLocalTime(),
                Cabinet = "123",
                Status = "Ждём",
            };

            Reception reception2 = new()
            {
                Id = 2,
                DoctorId = doctor2.Id,
                PatientId = patient2.Id,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.ToLocalTime(),
                Cabinet = "124",
                Status = "На приёме",
            };

            modelBuilder.Entity<Doctor>().HasData(doctor1, doctor2);
            modelBuilder.Entity<Patient>().HasData(patient1, patient2);
            modelBuilder.Entity<Reception>().HasData(reception1, reception2);
        }

    }
}