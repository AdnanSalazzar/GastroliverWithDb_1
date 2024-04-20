using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GastroliverWithDb.Models;

namespace GastroliverWithDb.Controllers
{
    public class TblPatientsController : Controller
    {
        private readonly MtechGastroLiverContext _context;

        public TblPatientsController(MtechGastroLiverContext context)
        {
            _context = context;
        }

        // GET: TblPatients
        public async Task<IActionResult> Index()
        {
            var mtechGastroLiverContext = _context.TblPatients.Include(t => t.Room);
            return View(await mtechGastroLiverContext.ToListAsync());
        }

        // GET: TblPatients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPatient = await _context.TblPatients
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (tblPatient == null)
            {
                return NotFound();
            }

            return View(tblPatient);
        }

        // GET: TblPatients/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.TblRooms, "RoomId", "RoomId");
            return View();
        }

        // POST: TblPatients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,Name,PhoneNo,Email,Nid,Gender,Age,RoomId")] TblPatient tblPatient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPatient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.TblRooms, "RoomId", "RoomId", tblPatient.RoomId);
            return View(tblPatient);
        }

        // GET: TblPatients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPatient = await _context.TblPatients.FindAsync(id);
            if (tblPatient == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.TblRooms, "RoomId", "RoomId", tblPatient.RoomId);
            return View(tblPatient);
        }

        // POST: TblPatients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,Name,PhoneNo,Email,Nid,Gender,Age,RoomId")] TblPatient tblPatient)
        {
            if (id != tblPatient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPatient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPatientExists(tblPatient.PatientId))
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
            ViewData["RoomId"] = new SelectList(_context.TblRooms, "RoomId", "RoomId", tblPatient.RoomId);
            return View(tblPatient);
        }

        // GET: TblPatients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPatient = await _context.TblPatients
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (tblPatient == null)
            {
                return NotFound();
            }

            return View(tblPatient);
        }

        // POST: TblPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblPatient = await _context.TblPatients.FindAsync(id);
            if (tblPatient != null)
            {
                _context.TblPatients.Remove(tblPatient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPatientExists(int id)
        {
            return _context.TblPatients.Any(e => e.PatientId == id);
        }
    }
}
