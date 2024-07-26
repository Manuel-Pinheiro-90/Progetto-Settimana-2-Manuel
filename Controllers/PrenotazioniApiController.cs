using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Models;
using System.Linq;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrenotazioniApiController : ControllerBase
    {
        private readonly IPrenotazioneDAO _prenotazioneDAO;
        private readonly IClienteDAO _clienteDAO;

        public PrenotazioniApiController(IPrenotazioneDAO prenotazioneDAO, IClienteDAO clienteDAO)
        {
            _prenotazioneDAO = prenotazioneDAO;
            _clienteDAO = clienteDAO;
        }

        [HttpGet("SearchByCodiceFiscale")]
        public IActionResult SearchByCodiceFiscale(string codiceFiscale)
        {
            var cliente = _clienteDAO.GetAll().FirstOrDefault(c => c.CodiceFiscale == codiceFiscale);
            if (cliente == null)
            {
                return NotFound(new { Message = "Cliente non trovato" });
            }

            var prenotazioni = _prenotazioneDAO.GetAll().Where(p => p.CodiceFiscaleCliente == cliente.ID).ToList();
            return Ok(prenotazioni);
        }


        [HttpGet("CountPensioneCompleta")]
        public IActionResult CountPensioneCompleta()
        {
            var prenotazioni = _prenotazioneDAO.GetAll().Where(p => p.TipoSoggiorno == "pensione completa").ToList();
            var count = prenotazioni.Count;
            var clienti = prenotazioni.Select(p => _clienteDAO.GetById(p.CodiceFiscaleCliente))
                                       .Distinct()
                                       .Select(c => new { c.Nome, c.Cognome, c.CodiceFiscale })
                                       .ToList();

            return Ok(new { Count = count, Clienti = clienti });
        }
    }
}
