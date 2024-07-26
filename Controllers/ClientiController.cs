using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;
using System.Linq.Expressions;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {
        private readonly IClienteDAO _clienteDAO;

        public ClientiController(IClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////// GET Clienti
        public IActionResult Index()
        {
            var clienti = _clienteDAO.GetAll();
            return View(clienti);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Clienti Details
        public IActionResult Details(int id)
        {
            var cliente = _clienteDAO.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Clienti Create
        public IActionResult Create()
        {
            return View();
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// POST Clienti Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteDAO.Add(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// GET Clienti Edit
        public IActionResult Edit(int id)
        {
            var cliente = _clienteDAO.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // /////////////////////////////////////////////////////////////////////////////////////////// POST Clienti Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            if (id != cliente.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clienteDAO.Update(cliente);
                }
                catch
                {
                    if (_clienteDAO.GetById(cliente.ID) == null)
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
            return View(cliente);
        }



        // /////////////////////////////////////////////////////////////////////////////////////////// POST Clienti Delete



        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _clienteDAO.Delete(id);
                return Json(new { success = true });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Si è verificato un errore imprevisto." });
            }
        }









    }
}
