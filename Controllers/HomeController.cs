using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;
using System.Diagnostics;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPrenotazioneDAO _prenotazioneDAO;
        private readonly IClienteDAO _clienteDAO;
        public HomeController(ILogger<HomeController> logger, IPrenotazioneDAO prenotazioneDAO, IClienteDAO clienteDAO)
        {
            _logger = logger;
             _prenotazioneDAO = prenotazioneDAO;
            _clienteDAO = clienteDAO;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchByCodiceFiscale(string codiceFiscale)
        {
            var cliente = _clienteDAO.GetAll().FirstOrDefault(c => c.CodiceFiscale == codiceFiscale);
            if (cliente == null)
            {
                ViewBag.ErrorMessage = "Cliente non trovato";
                return View("Index");
            }

            var prenotazioni = _prenotazioneDAO.GetAll().Where(p => p.CodiceFiscaleCliente == cliente.ID).ToList();
            ViewBag.Prenotazioni = prenotazioni;
            return View("Index");
        }

        [HttpGet]
        public IActionResult CountPensioneCompleta()
        {
            var count = _prenotazioneDAO.GetAll().Count(p => p.TipoSoggiorno == "pensione completa");
            ViewBag.CountPensioneCompleta = count;
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
