using Dentistry.Data;
using Dentistry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Dentistry.Controllers
{
	public class ReceptionsController : Controller
	{
		private readonly ApplicationContext _context;

		public ReceptionsController(ApplicationContext context)
		{
			_context = context;
		}

		[Authorize(Roles = "Администратор,Admin")]
		public async Task<IActionResult> Index(SortReception sr = SortReception.DateAsc)
		{

			IQueryable<Reception>? receptions = _context.Receptions.Include(r => r.Doctor).Include(r => r.Patient);

			ViewData["DoctorSort"] = sr == SortReception.DoctorAsc ? SortReception.DoctorDesc : SortReception.DoctorAsc;
			ViewData["PatientSort"] = sr == SortReception.PatientAsc ? SortReception.PatientDesc : SortReception.PatientAsc;
			ViewData["CabinetSort"] = sr == SortReception.CabinetAsc ? SortReception.CabinetDesc : SortReception.CabinetAsc;
			ViewData["DateSort"] = sr == SortReception.DateAsc ? SortReception.DateDesc : SortReception.DateAsc;
			ViewData["TimeSort"] = sr == SortReception.TimeAsc ? SortReception.TimeDesc : SortReception.TimeAsc;
			ViewData["StatusSort"] = sr == SortReception.StatusAsc ? SortReception.StatusDesc : SortReception.StatusAsc;

			receptions = sr switch
			{
				SortReception.DateDesc => receptions.OrderByDescending(r => r.Date),
				SortReception.PatientAsc => receptions.OrderBy(r => r.Patient),
				SortReception.PatientDesc => receptions.OrderByDescending(r => r.Patient),
				SortReception.DoctorAsc => receptions.OrderBy(r => r.Doctor),
				SortReception.DoctorDesc => receptions.OrderByDescending(r => r.Doctor),
				SortReception.CabinetAsc => receptions.OrderBy(r => r.Cabinet),
				SortReception.CabinetDesc => receptions.OrderByDescending(r => r.Cabinet),
				SortReception.TimeAsc => receptions.OrderBy(r => r.Time),
				SortReception.TimeDesc => receptions.OrderByDescending(r => r.Time),
				SortReception.StatusAsc => receptions.OrderBy(r => r.Status),
				SortReception.StatusDesc => receptions.OrderByDescending(r => r.Status),
				_ => receptions.OrderBy(s => s.Date)
			};

			return receptions != null ?
			View(await receptions.AsNoTracking().ToListAsync()) :
			Problem("Нет информации о приёмах.");
		}

		[Authorize(Roles = "Администратор,Admin")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Receptions == null)
			{
				return NotFound();
			}

			var reception = await _context.Receptions
				.Include(r => r.Doctor)
				.Include(r => r.Patient)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (reception == null)
			{
				return NotFound();
			}

			return View(reception);
		}

		[Authorize(Roles = "Администратор,Admin")]
		public IActionResult Create()
		{
			ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Surname");
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Surname");
			return View();
		}

		[Authorize(Roles = "Администратор,Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,DoctorId,PatientId,Cabinet,Date,Time,Status")] Reception reception)
		{
			if (ModelState.IsValid)
			{
				_context.Add(reception);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Surname", reception.DoctorId);
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Surname", reception.PatientId);
			return View(reception);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Receptions == null)
			{
				return NotFound();
			}

			var reception = await _context.Receptions.FindAsync(id);
			if (reception == null)
			{
				return NotFound();
			}
			ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Surname", reception.DoctorId);
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Surname", reception.PatientId);
			return View(reception);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,DoctorId,PatientId,Cabinet,Date,Time,Status")] Reception reception)
		{
			if (id != reception.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(reception);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ReceptionExists(reception.Id))
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
			ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Surname", reception.DoctorId);
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Surname", reception.PatientId);
			return View(reception);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Receptions == null)
			{
				return NotFound();
			}

			var reception = await _context.Receptions
				.Include(r => r.Doctor)
				.Include(r => r.Patient)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (reception == null)
			{
				return NotFound();
			}

			return View(reception);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Receptions == null)
			{
				return Problem("Сущность ПРИЁМ не существует.");
			}
			var reception = await _context.Receptions.FindAsync(id);
			if (reception != null)
			{
				_context.Receptions.Remove(reception);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ReceptionExists(int id)
		{
			return (_context.Receptions?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}