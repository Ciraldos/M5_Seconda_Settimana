using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esercitazione_M5_Seconda_Settimana.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {
        private IClientiService _clientiservice;

        public ClientiController(IClientiService clientiService)
        {
            _clientiservice = clientiService;
        }
        public IActionResult CreaCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreaCliente(Cliente cliente)
        {
            var c = _clientiservice.CreaCliente(cliente);
            return RedirectToAction("Index", "Home");
        }
    }
}
