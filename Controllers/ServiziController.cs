using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class ServiziController : Controller
    {
        private readonly IServizioDAO _servizioDAO;

        public ServiziController(IServizioDAO servizioDAO)
        {
            _servizioDAO = servizioDAO;
        }

        //  // ///////////////////////////////////////////////////////////////////////////////////////////  GET: Servizi
        public IActionResult Index()
        {
            var servizi = _servizioDAO.GetAll();
            return View(servizi);
        }

        // // ///////////////////////////////////////////////////////////////////////////////////////////  GET: Servizi/Details/5
        public IActionResult Details(int id)
        {
            var servizio = _servizioDAO.GetById(id);
            if (servizio == null)
            {
                return NotFound();
            }
            return View(servizio);
        }

        // // ///////////////////////////////////////////////////////////////////////////////////////////  GET: Servizi/Create
        public IActionResult Create()
        {
            return View();
        }

        // // ///////////////////////////////////////////////////////////////////////////////////////////  POST: Servizi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Servizio servizio)
        {
            if (ModelState.IsValid)
            {
                _servizioDAO.Add(servizio);
                return RedirectToAction(nameof(Index));
            }
            return View(servizio);
        }

        // // ///////////////////////////////////////////////////////////////////////////////////////////  GET: Servizi/Edit/5
        public IActionResult Edit(int id)
        {
            var servizio = _servizioDAO.GetById(id);
            if (servizio == null)
            {
                return NotFound();
            }
            return View(servizio);
        }

        // // ///////////////////////////////////////////////////////////////////////////////////////////  POST: Servizi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Servizio servizio)
        {
            if (id != servizio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _servizioDAO.Update(servizio);
                }
                catch
                {
                    if (_servizioDAO.GetById(servizio.ID) == null)
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
            return View(servizio);
        }

        // // ///////////////////////////////////////////////////////////////////////////////////////////  GET: Servizi/Delete/5
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _servizioDAO.Delete(id);
            return Json(new { success = true });
        }
    }
}
