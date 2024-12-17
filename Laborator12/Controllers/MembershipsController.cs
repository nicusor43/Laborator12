using Laborator12.Data;
using Laborator12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator12.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            var memberships = _context.Memberships.Include(m => m.Gym);
            return View(await memberships.ToListAsync());
        }

        // GET: Memberships/Create
        public IActionResult Create()
        {
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Nume");
            return View();
        }

        // POST: Memberships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titlu,Valoare,GymId")] Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Nume", membership.GymId);
            return View(membership);
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Nume", membership.GymId);
            return View(membership);
        }

        // POST: Memberships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titlu,Valoare,DataEmitere,GymId")] Membership membership)
        {
            if (id != membership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.Id))
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
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Nume", membership.GymId);
            return View(membership);
        }

        // GET: Memberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Memberships
                .Include(m => m.Gym)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);
            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Abonamentul a fost sters cu succes!";
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(int id)
        {
            return _context.Memberships.Any(e => e.Id == id);
        }
    }
}