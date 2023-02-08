using Dentistry.Data;
using Dentistry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Dentistry.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationContext _context;

		public PatientsController(ApplicationContext context)
        {
            _context = context;
        }

		[Authorize]
		public async Task<IActionResult> Index(SortPatient sp = SortPatient.SurnameAsc)
        {
            IQueryable<Patient>? patients = _context.Patients;

            ViewData["SurnameSort"] = sp == SortPatient.SurnameAsc ? SortPatient.SurnameDesc : SortPatient.SurnameAsc;
            ViewData["NameSort"] = sp == SortPatient.NameAsc ? SortPatient.NameDesc : SortPatient.NameAsc;
            ViewData["MiddleNameSort"] = sp == SortPatient.MiddleNameAsc ? SortPatient.MiddleNameDesc : SortPatient.MiddleNameAsc;
            ViewData["GenderSort"] = sp == SortPatient.GenderAsc ? SortPatient.GenderDesc : SortPatient.GenderAsc;
            ViewData["AgeCategorySort"] = sp == SortPatient.AgeCategoryAsc ? SortPatient.AgeCategoryDesc : SortPatient.AgeCategoryAsc;
            ViewData["PhoneSort"] = sp == SortPatient.PhoneAsc ? SortPatient.PhoneDesc : SortPatient.PhoneAsc;
            ViewData["CitySort"] = sp == SortPatient.CityAsc ? SortPatient.CityDesc : SortPatient.CityAsc;


            patients = sp switch
            {
                SortPatient.SurnameDesc => patients.OrderByDescending(d => d.Surname),
                SortPatient.NameAsc => patients.OrderBy(d => d.Name),
                SortPatient.NameDesc => patients.OrderByDescending(d => d.Name),
                SortPatient.MiddleNameAsc => patients.OrderBy(d => d.MiddleName),
                SortPatient.MiddleNameDesc => patients.OrderByDescending(d => d.MiddleName),
                SortPatient.GenderAsc => patients.OrderBy(d => d.Gender),
                SortPatient.GenderDesc => patients.OrderByDescending(d => d.Gender),
                SortPatient.AgeCategoryAsc => patients.OrderBy(d => d.AgeCategory),
                SortPatient.AgeCategoryDesc => patients.OrderByDescending(d => d.AgeCategory),
                SortPatient.PhoneAsc => patients.OrderBy(d => d.Phone),
                SortPatient.PhoneDesc => patients.OrderByDescending(d => d.Phone),
                SortPatient.CityAsc => patients.OrderBy(d => d.City),
                SortPatient.CityDesc => patients.OrderByDescending(d => d.City),

                _ => patients.OrderBy(d => d.Surname)
            };

            return patients != null ?
                          View(await patients.AsNoTracking().ToListAsync()) :
                          Problem("Нет информации о пациентах.");
        }

		[Authorize]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
		[Authorize(Roles = "Администратор,Главврач,Admin")]
		public IActionResult Create()
        {
            return View();
        }

		[Authorize(Roles = "Администратор,Главврач,Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,MiddleName,Gender,AgeCategory,Phone,City")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

		[Authorize(Roles = "Администратор,Главврач,Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

		[Authorize(Roles = "Администратор,Главврач,Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,MiddleName,Gender,AgeCategory,Phone,City")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

		[Authorize(Roles = "Администратор,Главврач,Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

		[Authorize(Roles = "Администратор,Главврач,Admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Сущность ПАЦИЕНТ не существует.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return (_context.Patients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
