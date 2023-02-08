using Dentistry.Data;
using Dentistry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Dentistry.Controllers
{

    public class DoctorsController : Controller
    {
        private readonly ApplicationContext _context;

        public DoctorsController(ApplicationContext context)
        {
            _context = context;
        }

		[Authorize(Roles = "Главврач,Admin")]
		public async Task<IActionResult> Index(SortDoctor sd = SortDoctor.SurnameAsc)
        {
            IQueryable<Doctor>? doctors = _context.Doctors;

            ViewData["SurnameSort"] = sd == SortDoctor.SurnameAsc ? SortDoctor.SurnameDesc : SortDoctor.SurnameAsc;
            ViewData["NameSort"] = sd == SortDoctor.NameAsc ? SortDoctor.NameDesc : SortDoctor.NameAsc;
            ViewData["MiddleNameSort"] = sd == SortDoctor.MiddleNameAsc ? SortDoctor.MiddleNameDesc : SortDoctor.MiddleNameAsc;
            ViewData["AgeSort"] = sd == SortDoctor.AgeAsc ? SortDoctor.AgeDesc : SortDoctor.AgeAsc;
            ViewData["GenderSort"] = sd == SortDoctor.GenderAsc ? SortDoctor.GenderDesc : SortDoctor.GenderAsc;
            ViewData["PhoneSort"] = sd == SortDoctor.PhoneAsc ? SortDoctor.PhoneDesc : SortDoctor.PhoneAsc;
            ViewData["SpecialitySort"] = sd == SortDoctor.SpecialityAsc ? SortDoctor.SpecialityDesc : SortDoctor.SpecialityAsc;

            doctors = sd switch
            {
                SortDoctor.SurnameDesc => doctors.OrderByDescending(d => d.Surname),
                SortDoctor.NameAsc => doctors.OrderBy(d => d.Name),
                SortDoctor.NameDesc => doctors.OrderByDescending(d => d.Name),
                SortDoctor.MiddleNameAsc => doctors.OrderBy(d => d.MiddleName),
                SortDoctor.MiddleNameDesc => doctors.OrderByDescending(d => d.MiddleName),
                SortDoctor.AgeAsc => doctors.OrderBy(d => d.Age),
                SortDoctor.AgeDesc => doctors.OrderByDescending(d => d.Age),
                SortDoctor.GenderAsc => doctors.OrderBy(d => d.Gender),
                SortDoctor.GenderDesc => doctors.OrderByDescending(d => d.Gender),
                SortDoctor.PhoneAsc => doctors.OrderBy(d => d.Phone),
                SortDoctor.PhoneDesc => doctors.OrderByDescending(d => d.Phone),
                SortDoctor.SpecialityAsc => doctors.OrderBy(d => d.Speciality),
                SortDoctor.SpecialityDesc => doctors.OrderByDescending(d => d.Speciality),
                _ => doctors.OrderBy(s => s.Surname)
            };

            return doctors != null ?
                        View(await doctors.AsNoTracking().ToListAsync()) :
                        Problem("Нет информации о врачах.");
        }

		[Authorize(Roles = "Главврач,Admin")]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

		[Authorize(Roles = "Главврач,Admin")]
		public IActionResult Create()
        {
            return View();
        }

		[Authorize(Roles = "Главврач, Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,MiddleName,Age,Gender,Phone,Speciality")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

		[Authorize(Roles = "Главврач,Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

		[Authorize(Roles = "Главврач,Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,MiddleName,Age,Gender,Phone,Speciality")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

		[Authorize(Roles = "Главврач,Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

		[Authorize(Roles = "Главврач,Admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctors == null)
            {
                return Problem("Сущность ДОКТОР не существует.");
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}