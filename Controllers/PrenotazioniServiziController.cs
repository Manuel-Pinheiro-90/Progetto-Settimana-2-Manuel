using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class PrenotazioniServiziController : Controller
    {
        private readonly IPrenotazioneServizioDAO _prenotazioneServizioDAO;

        public PrenotazioniServiziController(IPrenotazioneServizioDAO prenotazioneServizioDAO)
        {
            _prenotazioneServizioDAO = prenotazioneServizioDAO;
        }

        // //////////////////////////////////////////////////////////////////////////////////// GET: PrenotazioniServizi
        public IActionResult Index()
        {
            var prenotazioniServizi = _prenotazioneServizioDAO.GetAll();
            return View(prenotazioniServizi);
        }

        /////////////////////////////////////////////////////////////////////////GET: PrenotazioniServizi/Details/5
        public IActionResult Details(int id)
        {
            var prenotazioneServizio = _prenotazioneServizioDAO.GetById(id);
            if (prenotazioneServizio == null)
            {
                return NotFound();
            }
            return View(prenotazioneServizio);
        }

        // //////////////////////////////////////////////////////////////////////////////////// GET: PrenotazioniServizi/Create
        public IActionResult Create()
        {
            return View();
        }

       ///////////////////////////POST: PrenotazioniServizi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrenotazioneServizio prenotazioneServizio)
        {
            if (ModelState.IsValid)
            {
                _prenotazioneServizioDAO.Add(prenotazioneServizio);
                return RedirectToAction(nameof(Index));
            }
            return View(prenotazioneServizio);
        }

        /////////////////////////////////////////////////////////GET: PrenotazioniServizi/Edit/5
        public IActionResult Edit(int id)
        {
            var prenotazioneServizio = _prenotazioneServizioDAO.GetById(id);
            if (prenotazioneServizio == null)
            {
                return NotFound();
            }
            return View(prenotazioneServizio);
        }

        //POST: PrenotazioniServizi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PrenotazioneServizio prenotazioneServizio)
        {
            if (id != prenotazioneServizio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _prenotazioneServizioDAO.Update(prenotazioneServizio);
                }
                catch
                {
                    if (_prenotazioneServizioDAO.GetById(prenotazioneServizio.ID) == null)
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
            return View(prenotazioneServizio);
        }

        //////////////////////////////////////////////////////////////////GET: PrenotazioniServizi/Delete/5
        public IActionResult Delete(int id)
        {
            var prenotazioneServizio = _prenotazioneServizioDAO.GetById(id);
            if (prenotazioneServizio == null)
            {
                return NotFound();
            }
            return View(prenotazioneServizio);
        }

        // POST: PrenotazioniServizi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _prenotazioneServizioDAO.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
