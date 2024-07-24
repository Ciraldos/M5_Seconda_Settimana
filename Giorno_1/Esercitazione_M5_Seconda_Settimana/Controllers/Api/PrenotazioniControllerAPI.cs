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
        [HttpGet]
        public ActionResult<IEnumerable<Prenotazione>> GetAll(string cf)
        {
            var prenotazioni = _prenotazioneService.PrenotazioneByCf(cf);
            return Ok(prenotazioni);
        }
    }
}
