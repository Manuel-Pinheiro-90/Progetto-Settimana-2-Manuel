using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class PrenotazioniController : Controller
    {
        private readonly IPrenotazioneDAO _prenotazioneDAO;

        public PrenotazioniController(IPrenotazioneDAO prenotazioneDAO)
        {
            _prenotazioneDAO = prenotazioneDAO;
        }

        // GET: Prenotazioni
        public IActionResult Index()
        {
            var prenotazioni = _prenotazioneDAO.GetAll();
            return View(prenotazioni);
        }

        // GET: Prenotazioni/Details/5
        public IActionResult Details(int id)
        {
            var prenotazione = _prenotazioneDAO.GetById(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            return View(prenotazione);
        }

        // GET: Prenotazioni/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prenotazioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _prenotazioneDAO.Add(prenotazione);
                return RedirectToAction(nameof(Index));
            }
            return View(prenotazione);
        }

        // GET: Prenotazioni/Edit/5
        public IActionResult Edit(int id)
        {
            var prenotazione = _prenotazioneDAO.GetById(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            return View(prenotazione);
        }

        // POST: Prenotazioni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Prenotazione prenotazione)
        {
            if (id != prenotazione.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _prenotazioneDAO.Update(prenotazione);
                }
                catch
                {
                    if (_prenotazioneDAO.GetById(prenotazione.ID) == null)
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
            return View(prenotazione);
        }

        // GET: Prenotazioni/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _prenotazioneDAO.Delete(id);
            return Json(new { success = true });
        }

    }
}
