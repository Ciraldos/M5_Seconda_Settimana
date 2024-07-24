using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Esercitazione_M5_Seconda_Settimana.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrenotazioniControllerAPI : ControllerBase
    {
        private IPrenotazioneService _prenotazioneService;
        public PrenotazioniControllerAPI(IPrenotazioneService prenotazioneService)
        {
            _prenotazioneService = prenotazioneService;
        }
        [HttpGet("ByCf")]
        public ActionResult<IEnumerable<Prenotazione>> GetAll(string cf)
        {
            if (string.IsNullOrWhiteSpace(cf))
            {
                return BadRequest("Il codice fiscale è richiesto.");
            }
            var prenotazioni = _prenotazioneService.PrenotazioneByCf(cf);
            if (prenotazioni == null || !prenotazioni.Any())
            {
                return NotFound("Nessuna prenotazione trovata per il codice fiscale fornito.");
            }
            return Ok(prenotazioni);
        }

        [HttpGet("ByPensione")]
        public ActionResult<IEnumerable<Prenotazione>> GetAllByPrenotazione(string p)
        {
            if (string.IsNullOrWhiteSpace(p))
            {
                return BadRequest("Il tipo di pensione è richiesta");
            }
            var prenotazioni = _prenotazioneService.PrenotazioneByPensione(p);
            if (prenotazioni == null || !prenotazioni.Any())
            {
                return NotFound("Nessuna prenotazione trovata per il tipo di pensione fornito fornito.");
            }
            return Ok(prenotazioni);
        }
    }
}
