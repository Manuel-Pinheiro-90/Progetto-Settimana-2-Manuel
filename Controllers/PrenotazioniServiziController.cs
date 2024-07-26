using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    [Authorize]
    public class PrenotazioniServiziController : Controller
    {
        private readonly IPrenotazioneServizioDAO _prenotazioneServizioDAO;
        private readonly IServizioDAO _servizioDAO;
        public PrenotazioniServiziController(IPrenotazioneServizioDAO prenotazioneServizioDAO, IServizioDAO servizioDAO)
        {
            _prenotazioneServizioDAO = prenotazioneServizioDAO;
            _servizioDAO = servizioDAO;
        }

        // //////////////////////////////////////////////////////////////////////////////////// GET PrenotazioniServizi
        public IActionResult Index()
        {
            var prenotazioniServizi = _prenotazioneServizioDAO.GetAll();
            return View(prenotazioniServizi);
        }

        /////////////////////////////////////////////////////////////////////////GET PrenotazioniServizi Details
        public IActionResult Details(int id)
        {
            var prenotazioneServizio = _prenotazioneServizioDAO.GetById(id);
            if (prenotazioneServizio == null)
            {
                return NotFound();
            }
            return View(prenotazioneServizio);
        }

        // //////////////////////////////////////////////////////////////////////////////////// GET PrenotazioniServizi Create
        public IActionResult Create()
        {
            var servizi = _servizioDAO.GetAll();
            ViewBag.Servizi = new SelectList(servizi, "ID", "Descrizione");
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

        /////////////////////////////////////////////////////////GET PrenotazioniServizi Edit
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

        //////////////////////////////////////////////////////////////////GET PrenotazioniServizi Delete
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
