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
        public IActionResult GetAllPrenotazioni()
        {
            var p = _prenotazioneService.GetAll();
            return View(p);
        }

        public IActionResult Checkout(int id)
        {
            ViewBag.Id = id;
            var p = _prenotazioneService.Checkout(id);
            return View(p);
        }

        [HttpPost]
        public IActionResult CreazionePrenotazione(Prenotazione p)
        {
            var prenotazione = _prenotazioneService.CreaPrenotazione(p);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreaPrenotazioneServizio()
        {
            ViewBag.servizi = _prenotazioneService.GetServizi();
            return View();
        }

        [HttpPost]
        public IActionResult CreaPrenotazioneServizio(PrenotazioniServizio p, int id)
        {
            var prenotazione = _prenotazioneService.CreaPrenotazioneServizio(p, id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CercaByCf()
        {
            return View();
        }

        public IActionResult CercaByPensione()
        {
            return View();
        }



    }
}
