using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;
using System.Diagnostics;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPrenotazioneDAO _prenotazioneDAO;
        private readonly IClienteDAO _clienteDAO;
        private readonly ICameraDAO _cameraDAO;
        private readonly IServizioDAO _servizioDAO;

        public HomeController(ILogger<HomeController> logger, IPrenotazioneDAO prenotazioneDAO, IClienteDAO clienteDAO, ICameraDAO cameraDAO, IServizioDAO servizioDAO)
        {
            _logger = logger;
             _prenotazioneDAO = prenotazioneDAO;
            _clienteDAO = clienteDAO;
            _cameraDAO = cameraDAO;
            _servizioDAO = servizioDAO;


        }
       
        public IActionResult Index()
        {
            ViewBag.TotalPrenotazioni = _prenotazioneDAO.GetAll().Count();
            ViewBag.TotalClienti = _clienteDAO.GetAll().Count();
            ViewBag.TotalCamere = _cameraDAO.GetAll().Count();
            ViewBag.TotalServizi = _servizioDAO.GetAll().Count();

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
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
