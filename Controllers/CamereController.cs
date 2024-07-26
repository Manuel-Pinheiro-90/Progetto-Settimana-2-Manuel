using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class CamereController : Controller
    {
        private readonly ICameraDAO _cameraDAO;

        public CamereController(ICameraDAO cameraDAO)
        {
            _cameraDAO = cameraDAO;
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Camere
        public IActionResult Index()
        {
            var camere = _cameraDAO.GetAll();
            return View(camere);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Camere Dettagli
        public IActionResult Details(int id)
        {
            var camera = _cameraDAO.GetById(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Camere Create
        public IActionResult Create()
        {
            return View();
        }

        //  POST: Camere/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Camera camera)
        {
            if (ModelState.IsValid)
            {
                _cameraDAO.Add(camera);
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Camere Edit
        public IActionResult Edit(int id)
        {
            var camera = _cameraDAO.GetById(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        // POST: Camere/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Camera camera)
        {
            if (id != camera.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cameraDAO.Update(camera);
                }
                catch
                {
                    if (_cameraDAO.GetById(camera.ID) == null)
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
            return View(camera);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET: Camere Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _cameraDAO.Delete(id);
            return Json(new { success = true });
        }

    }




}

