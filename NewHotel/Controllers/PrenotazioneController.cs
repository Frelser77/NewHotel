using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewHotel.Data;
using NewHotel.Models;

namespace NewHotel.Controllers
{
    [Authorize]
    public class PrenotazioneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrenotazioneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prenotazione
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Prenotazione> prenotazioniQuery = _context.Prenotazioni
                                                                 .Include(p => p.Camera)
                                                                 .Include(p => p.Cliente);

            if (!string.IsNullOrEmpty(searchString))
            {
                prenotazioniQuery = prenotazioniQuery.Where(p => p.Cliente.Nome.Contains(searchString)
                                                                || p.Cliente.Cognome.Contains(searchString));
            }

            var applicationDbContext = await prenotazioniQuery.ToListAsync();
            return View(applicationDbContext);
        }

        // GET: Prenotazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Servizi)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // GET: Prenotazione/Create
        public IActionResult Create()
        {
            var prenotazione = new Prenotazione
            {
                DataPrenotazione = DateTime.Today,
                DataCheckIn = DateTime.Today,
                DataCheckOut = DateTime.Today.AddDays(1)
            };

            ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "Descrizione");
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "Nome");
            ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione");
            return View(prenotazione);
        }

        // POST: Prenotazione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,IdCamera,IdPensione,DataPrenotazione,DataCheckIn,DataCheckOut,NumeroOspiti,Acconto,PrezzoTotale,Note")] Prenotazione prenotazione)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");
            ModelState.Remove("PrenotazioneServizi");

            int durataSoggiorno = (prenotazione.DataCheckOut - prenotazione.DataCheckIn).Days;
            Pensione? pensione = await _context.Pensioni.FindAsync(prenotazione.IdPensione);
            prenotazione.PrezzoTotale = durataSoggiorno * pensione.CostoGiornaliero;

            if (ModelState.IsValid)
            {
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "Descrizione", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CF", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);

            return View(prenotazione);
        }

        // GET: Prenotazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Carica prenotazione e includi i servizi correlati
            var prenotazione = await _context.Prenotazioni
             .Include(p => p.PrenotazioneServizi)
             .ThenInclude(ps => ps.Servizio)
             .Include(p => p.Camera)
             .Include(p => p.Cliente)
             .Include(p => p.Pensione)
             .FirstOrDefaultAsync(m => m.IdPrenotazione == id);

            if (prenotazione == null)
            {
                return NotFound();
            }

            ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "Descrizione", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CF", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);

            return View(prenotazione);
        }

        // POST: Prenotazione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrenotazione,IdCliente,IdCamera,IdPensione,DataPrenotazione,DataCheckIn,DataCheckOut,Acconto,NumeroOspiti,PrezzoTotale,Note")] Prenotazione prenotazioneFormData)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");


            if (id != prenotazioneFormData.IdPrenotazione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Trova la prenotazione esistente
                    var prenotazioneToUpdate = await _context.Prenotazioni
                        .Include(p => p.Servizi)
                        .FirstOrDefaultAsync(p => p.IdPrenotazione == id);

                    if (prenotazioneToUpdate == null)
                    {
                        return NotFound();
                    }

                    // Aggiorna la prenotazione con i nuovi dati del form
                    _context.Entry(prenotazioneToUpdate).CurrentValues.SetValues(prenotazioneFormData);

                    // Aggiungi qui la logica per aggiornare i servizi se necessario

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazioneFormData.IdPrenotazione))
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

            // Se siamo arrivati qui, qualcosa è fallito, quindi ricarica la vista
            ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "Descrizione", prenotazioneFormData.IdCamera);
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CF", prenotazioneFormData.IdCliente);
            ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione", prenotazioneFormData.IdPensione);
            return View(prenotazioneFormData);
        }

        // GET: Prenotazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Camera)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // POST: Prenotazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione != null)
            {
                _context.Prenotazioni.Remove(prenotazione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Prenotazione/RemoveServizio/5
        public async Task<IActionResult> RemoveServizio(int idPrenotazione, int idServizio)
        {
            var prenotazioneServizioToRemove = await _context.PrenotazioniServizi
                .FirstOrDefaultAsync(ps => ps.IdServizio == idServizio && ps.IdPrenotazione == idPrenotazione);

            if (prenotazioneServizioToRemove != null)
            {
                _context.PrenotazioniServizi.Remove(prenotazioneServizioToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Edit), new { id = idPrenotazione });
        }
        /*

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int idPrenotazione)
        {
            // Trova la prenotazione basata sull'ID fornito
            var prenotazione = await _context.Prenotazioni
                .Include(p => p.PrenotazioneServizi)
                    .ThenInclude(ps => ps.Servizio)
                .Include(p => p.Pensione)
                .FirstOrDefaultAsync(p => p.IdPrenotazione == idPrenotazione);

            if (prenotazione == null)
            {
                return NotFound();
            }

            // Calcola il totale
            await CalcolaTotale(prenotazione);

            // Salva eventuali modifiche
            await _context.SaveChangesAsync();

            // Reindirizza a una vista/route appropriata dopo il checkout
            // Potresti voler mostrare un riepilogo della prenotazione o reindirizzare l'utente a una pagina di conferma
            return RedirectToAction(nameof(Details), new { id = idPrenotazione });
        }
                private async Task CalcolaTotale(Prenotazione prenotazione)
                {
                    var durataSoggiorno = (prenotazione.DataCheckOut - prenotazione.DataCheckIn).Days;

                    // Ottieni il costo giornaliero basato sul tipo di camera
                    decimal costoCamera = await OttieniCostoCamera(prenotazione.Camera.TipoCamera);

                    // Ottieni il costo giornaliero basato sul tipo di pensione
                    decimal costoPensione = await OttieniCostoPensione(prenotazione.Pensione.TipoPensione);

                    // Calcola il costo base per la durata del soggiorno e il numero di ospiti
                    var costoBase = durataSoggiorno * costoCamera * prenotazione.NumeroOspiti;

                    // Calcola il costo della pensione
                    var costoTotalePensione = durataSoggiorno * costoPensione * prenotazione.NumeroOspiti;

                    // Calcola il costo dei servizi aggiuntivi
                    // Assumiamo che tu abbia un modo per calcolare questo basato sui servizi selezionati per la prenotazione
                    var costoServizi = CalcolaCostoServizi(prenotazione.PrenotazioneServizi);

                    // Calcola il totale
                    var totale = costoBase + costoTotalePensione + costoServizi;

                    // Aggiorna il prezzo totale della prenotazione
                    prenotazione.PrezzoTotale = totale;
                }
        */
        private Task<decimal> OttieniCostoCamera(TipoCamera tipoCamera)
        {
            decimal costo = tipoCamera switch
            {
                TipoCamera.Standard => 35m,       // Costo per camera Standard
                TipoCamera.Deluxe => 60m,         // Costo per camera Deluxe
                TipoCamera.EroiGalattici => 100m, // Costo per camera Eroi Galattici
                _ => throw new ArgumentOutOfRangeException(nameof(tipoCamera), $"Costo non definito per il tipo di camera {tipoCamera}.")
            };

            return Task.FromResult(costo);
        }

        private Task<decimal> OttieniCostoPensione(TipiPensione tipiPensione)
        {
            decimal costo = tipiPensione switch
            {
                TipiPensione.PensioneCompleta => 40m,               // Costo per pensione completa
                TipiPensione.MezzaPensione => 25m,                  // Costo per mezza pensione
                TipiPensione.PernottamentoEColazione => 15m,        // Costo per pernottamento e colazione
                _ => throw new ArgumentOutOfRangeException(nameof(tipiPensione), $"Costo non definito per il tipo di pensione {tipiPensione}.")
            };

            return Task.FromResult(costo);
        }

        private decimal CalcolaCostoServizi(ICollection<PrenotazioneServizio> prenotazioneServizi)
        {
            // Calcola il costo totale dei servizi aggiuntivi
            // Assumi che ogni servizio abbia un campo 'Costo' o una logica simile per determinare il costo
            return prenotazioneServizi.Sum(ps => ps.Servizio.PrezzoTot);
        }


        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.IdPrenotazione == id);
        }
    }
}
