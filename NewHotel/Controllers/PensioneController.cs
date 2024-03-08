using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewHotel.Data;
using NewHotel.Models;

namespace NewHotel.Controllers
{
    [Authorize]
    public class PensioneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PensioneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pensione
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pensioni.ToListAsync());
        }

        // GET: Pensione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensione = await _context.Pensioni
                .FirstOrDefaultAsync(m => m.IdPensione == id);
            if (pensione == null)
            {
                return NotFound();
            }

            return View(pensione);
        }

        // GET: Pensione/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pensione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPensione,CostoGiornaliero")] Pensione pensione)
        {
            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                _context.Add(pensione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pensione);
        }

        // GET: Pensione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensione = await _context.Pensioni.FindAsync(id);
            if (pensione == null)
            {
                return NotFound();
            }
            return View(pensione);
        }

        // POST: Pensione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPensione,TipoPensione,CostoGiornaliero")] Pensione pensione)
        {
            ModelState.Remove("Prenotazioni");

            if (id != pensione.IdPensione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pensione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PensioneExists(pensione.IdPensione))
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
            return View(pensione);
        }

        // GET: Pensione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensione = await _context.Pensioni
                .FirstOrDefaultAsync(m => m.IdPensione == id);
            if (pensione == null)
            {
                return NotFound();
            }

            return View(pensione);
        }

        // POST: Pensione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pensione = await _context.Pensioni.FindAsync(id);
            if (pensione != null)
            {
                _context.Pensioni.Remove(pensione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PensioneExists(int id)
        {
            return _context.Pensioni.Any(e => e.IdPensione == id);
        }
    }
}
