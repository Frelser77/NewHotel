using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewHotel.Data;
using NewHotel.Models;

namespace NewHotel.Controllers
{
    [Authorize]
    public class ServizioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServizioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servizio
        /*  public async Task<IActionResult> Index()
          {
              var applicationDbContext = _context.Servizi.Include(s => s.PrenotazioneServizi);
              return View(await applicationDbContext.ToListAsync());
          }
        */

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Servizi; // Rimuovi .Include(s => s.PrenotazioneServizi) temporaneamente
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Servizio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi
                .Include(s => s.PrenotazioneServizi)
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // GET: Servizio/Create
        public IActionResult Create(int? id)
        {
            return View();
        }

        // POST: Servizio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("TipoServizio,Quantita,PrezzoTot,DataAggiunta,IdPrenotazione")] Servizio servizio)
        {
            // Rimuovi le ModelState che non sono necessarie o che potrebbero causare problemi di validazione.
            ModelState.Remove("Prenotazioni");
            ModelState.Remove("PrenotazioneServizi");

            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    // Assegna l'ID della prenotazione al servizio se l'ID è fornito
                    servizio.IdPrenotazione = id.Value;
                }

                // Aggiunge il servizio al contesto e salva le modifiche
                _context.Add(servizio);
                await _context.SaveChangesAsync();

                // Reindirizza all'indice o alla vista che preferisci dopo il salvataggio
                return RedirectToAction(nameof(Index));
            }

            // Se il modello non è valido, ritorna alla vista corrente con i dati esistenti
            ViewData["Id"] = id;
            return View(servizio);
        }

        // GET: Servizio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ModelState.Remove("Prenotazioni");
            ModelState.Remove("PrenotazioneServizi");

            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            ViewData["IdPrenotazione"] = new SelectList(_context.Prenotazioni, "IdPrenotazione", "IdPrenotazione", servizio.IdPrenotazione);
            return View(servizio);
        }

        // POST: Servizio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServizio,TipoServizio,Quantita,PrezzoTot,DataAggiunta,IdPrenotazione")] Servizio servizio)
        {
            ModelState.Remove("Prenotazioni");
            ModelState.Remove("PrenotazioneServizi");
            ModelState.Remove("Quantitá");
            ModelState.Remove("DataAggiunta");

            if (id != servizio.IdServizio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servizio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServizioExists(servizio.IdServizio))
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
            ViewData["PrenotazioneServizi"] = new SelectList(_context.Prenotazioni, "TipoServizio", "IdPrenotazione", servizio.IdPrenotazione);
            return View(servizio);
        }

        // GET: Servizio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi
                .Include(s => s.PrenotazioneServizi)
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // POST: Servizio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio != null)
            {
                _context.Servizi.Remove(servizio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*
        public async Task<IActionResult> SelezionaServizio(int idPrenotazione)
        {
            List<Servizio> servizi = await _context.Servizi.ToListAsync();
            ViewData["IdPrenotazione"] = idPrenotazione;
            return View(servizi);
        }
        */
        public async Task<IActionResult> AggiungiServizioAllaPrenotazione(int idServizioSelezionato, int idPrenotazione)
        {
            // Verifica l'esistenza del servizio e della prenotazione
            var servizioEsistente = await _context.Servizi.FindAsync(idServizioSelezionato);
            var prenotazioneEsistente = await _context.Prenotazioni.FindAsync(idPrenotazione);

            if (servizioEsistente == null || prenotazioneEsistente == null)
            {
                return NotFound();
            }

            // Crea una nuova istanza di PrenotazioneServizio per collegare il servizio alla prenotazione
            var nuovoPrenotazioneServizio = new PrenotazioneServizio
            {
                IdServizio = idServizioSelezionato,
                IdPrenotazione = idPrenotazione
            };

            // Verifica se l'associazione esiste già per evitare duplicati
            var associazioneEsistente = await _context.PrenotazioniServizi
                .AnyAsync(ps => ps.IdServizio == idServizioSelezionato && ps.IdPrenotazione == idPrenotazione);

            if (!associazioneEsistente)
            {
                // Aggiunge la nuova associazione al contesto e salva le modifiche
                _context.PrenotazioniServizi.Add(nuovoPrenotazioneServizio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", "Prenotazione", new { id = idPrenotazione });
        }

        private bool ServizioExists(int id)
        {
            return _context.Servizi.Any(e => e.IdServizio == id);
        }
    }
}
