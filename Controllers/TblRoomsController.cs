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
    public class TblRoomsController : Controller
    {
        private readonly MtechGastroLiverContext _context;

        public TblRoomsController(MtechGastroLiverContext context)
        {
            _context = context;
        }

        // GET: TblRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblRooms.ToListAsync());
        }

        // GET: TblRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoom = await _context.TblRooms
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (tblRoom == null)
            {
                return NotFound();
            }

            return View(tblRoom);
        }

        // GET: TblRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomNo")] TblRoom tblRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblRoom);
        }

        // GET: TblRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoom = await _context.TblRooms.FindAsync(id);
            if (tblRoom == null)
            {
                return NotFound();
            }
            return View(tblRoom);
        }

        // POST: TblRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomNo")] TblRoom tblRoom)
        {
            if (id != tblRoom.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRoomExists(tblRoom.RoomId))
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
            return View(tblRoom);
        }

        // GET: TblRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoom = await _context.TblRooms
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (tblRoom == null)
            {
                return NotFound();
            }

            return View(tblRoom);
        }

        // POST: TblRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblRoom = await _context.TblRooms.FindAsync(id);
            if (tblRoom != null)
            {
                _context.TblRooms.Remove(tblRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblRoomExists(int id)
        {
            return _context.TblRooms.Any(e => e.RoomId == id);
        }
    }
}
