using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;


namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class PrenotazioniController : Controller
    {
        private readonly IPrenotazioneDAO _prenotazioneDAO;
        private readonly IClienteDAO _clienteDAO;
        private readonly ICameraDAO _cameraDAO;
        private readonly IServizioDAO _servizioDAO;
        private readonly IPrenotazioneServizioDAO _prenotazioneServizioDAO;

        public PrenotazioniController(IPrenotazioneDAO prenotazioneDAO, IClienteDAO clienteDAO, ICameraDAO cameraDAO, IServizioDAO servizioDAO, IPrenotazioneServizioDAO prenotazioneServizioDAO)
        {
            _prenotazioneDAO = prenotazioneDAO;
            _clienteDAO = clienteDAO;
            _cameraDAO = cameraDAO;
            _servizioDAO = servizioDAO;
            _prenotazioneServizioDAO = prenotazioneServizioDAO;
        }

        // GET: Prenotazioni/Index
        public IActionResult Index()
        {
            var prenotazioni = _prenotazioneDAO.GetAll();
            var riepilogoList = prenotazioni.Select(p => {
                var cliente = _clienteDAO.GetById(p.CodiceFiscaleCliente);
                var camera = _cameraDAO.GetById(p.NumeroCamera);
                var serviziAggiuntivi = _prenotazioneServizioDAO.GetAll()
                    .Where(ps => ps.NumeroPrenotazione == p.ID)
                    .Select(ps => new ServizioRiepilogoViewModel
                    {
                        DescrizioneServizio = _servizioDAO.GetById(ps.ServizioID).Descrizione,
                        Quantita = ps.Quantita,
                        Prezzo = _servizioDAO.GetById(ps.ServizioID).Prezzo
                    }).ToList();

                return new PrenotazioneCompletaViewModel
                {
                    PrenotazioneId = p.ID,
                    Cliente = cliente,
                    Camera = camera,
                    DataPrenotazione = p.DataPrenotazione,
                    PeriodoDal = p.PeriodoDal,
                    PeriodoAl = p.PeriodoAl,
                    CaparraConfirmatoria = p.CaparraConfirmatoria ?? 0,
                    TariffaApplicata = p.TariffaApplicata,
                    TipoSoggiorno = p.TipoSoggiorno,
                    ServiziAggiuntivi = serviziAggiuntivi
                };
            }).ToList();

            return View(riepilogoList);
        }

        // GET: Prenotazioni/Create
        public IActionResult Create()
        {
            var viewModel = new PrenotazioneViewModel
            {
                Clienti = _clienteDAO.GetAll(),
                Camere = _cameraDAO.GetAll(),
                Servizi = _servizioDAO.GetAll()
            };
            return View(viewModel);
        }

        // POST: Prenotazioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrenotazioneViewModel viewModel)
        {
          
            
                var prenotazione = new Prenotazione
                {
                    CodiceFiscaleCliente = viewModel.ClienteId,
                    NumeroCamera = viewModel.CameraId,
                    DataPrenotazione = viewModel.DataPrenotazione,
                    Anno = viewModel.DataPrenotazione.Year,
                    PeriodoDal = viewModel.PeriodoDal,
                    PeriodoAl = viewModel.PeriodoAl,
                    CaparraConfirmatoria = viewModel.CaparraConfirmatoria,
                    TariffaApplicata = viewModel.TariffaApplicata,
                    TipoSoggiorno = viewModel.TipoSoggiorno
                };

                _prenotazioneDAO.Add(prenotazione);

                foreach (var servizioId in viewModel.ServiziSelezionati)
                {
                    var prenotazioneServizio = new PrenotazioneServizio
                    {
                        NumeroPrenotazione = prenotazione.ID,
                        ServizioID = servizioId,
                        Data = viewModel.DataPrenotazione,
                        Quantita = viewModel.QuantitaServizi.ContainsKey(servizioId) ? viewModel.QuantitaServizi[servizioId] : 1
                    };
                    _prenotazioneServizioDAO.Add(prenotazioneServizio);
                }

                return RedirectToAction(nameof(Index));
            

            viewModel.Clienti = _clienteDAO.GetAll();
            viewModel.Camere = _cameraDAO.GetAll();
            viewModel.Servizi = _servizioDAO.GetAll();
            return View(viewModel);
        }

        // GET: Prenotazioni/Checkout/5
        public IActionResult Checkout(int id)
        {
            var prenotazione = _prenotazioneDAO.GetById(id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            var cliente = _clienteDAO.GetById(prenotazione.CodiceFiscaleCliente);
            var camera = _cameraDAO.GetById(prenotazione.NumeroCamera);
            var serviziAggiuntivi = _prenotazioneServizioDAO.GetAll()
                .Where(ps => ps.NumeroPrenotazione == prenotazione.ID)
                .Select(ps => new ServizioRiepilogoViewModel
                {
                    DescrizioneServizio = _servizioDAO.GetById(ps.ServizioID).Descrizione,
                    Quantita = ps.Quantita,
                    Prezzo = _servizioDAO.GetById(ps.ServizioID).Prezzo
                }).ToList();

            var totaleServiziAggiuntivi = serviziAggiuntivi.Sum(s => s.Quantita * s.Prezzo);
            var importoDaSaldare = (prenotazione.TariffaApplicata + totaleServiziAggiuntivi) - prenotazione.CaparraConfirmatoria ?? 0;

            var checkoutViewModel = new PrenotazioneCompletaViewModel
            {
                PrenotazioneId = prenotazione.ID,
                Cliente = cliente,
                Camera = camera,
                DataPrenotazione = prenotazione.DataPrenotazione,
                PeriodoDal = prenotazione.PeriodoDal,
                PeriodoAl = prenotazione.PeriodoAl,
                CaparraConfirmatoria = prenotazione.CaparraConfirmatoria ?? 0,
                TariffaApplicata = prenotazione.TariffaApplicata,
                TipoSoggiorno = prenotazione.TipoSoggiorno,
                ServiziAggiuntivi = serviziAggiuntivi
            };

            ViewBag.ImportoDaSaldare = importoDaSaldare;

            return View(checkoutViewModel);
        }




        // /////////////////////////////////////////////////////////////////////////////////////////// POST: Clienti/Delete/5



        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _prenotazioneDAO.Delete(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }









    }
}
