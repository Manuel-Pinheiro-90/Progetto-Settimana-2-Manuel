using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class ClientiController : Controller
    {
        private readonly IClienteDAO _clienteDAO;

        public ClientiController(IClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }

        /// GET: Clienti
        public IActionResult Index()
        {
            var clienti = _clienteDAO.GetAll();
            return View(clienti);
        }

        // GET: Clienti/Details/5
        public IActionResult Details(int id)
        {
            var cliente = _clienteDAO.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // GET: Clienti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clienti/Create
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

        // GET: Clienti/Edit/5
        public IActionResult Edit(int id)
        {
            var cliente = _clienteDAO.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clienti/Edit/5
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



        // POST: Clienti/Delete/5
        


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _clienteDAO.Delete(id);
            return Json(new { success = true });
        }










    }
}
