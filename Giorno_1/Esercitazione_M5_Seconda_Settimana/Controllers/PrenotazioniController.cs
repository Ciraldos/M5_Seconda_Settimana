using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esercitazione_M5_Seconda_Settimana.Controllers
{
    [Authorize]
    public class PrenotazioniController : Controller
    {
        private IPrenotazioneService _prenotazioneService;
        public PrenotazioniController(IPrenotazioneService prenotazioneService)
        {
            _prenotazioneService = prenotazioneService;
        }
        public IActionResult CreazionePrenotazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreazionePrenotazione(Prenotazione p)
        {
            var prenotazione = _prenotazioneService.CreaPrenotazione(p);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreaPrenotazioneServizio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaPrenotazioneServizio(PrenotazioniServizio p)
        {
            var prenotazione = _prenotazioneService.CreaPrenotazioneServizio(p);
            return RedirectToAction("Index", "Home");
        }

    }
}
