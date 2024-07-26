using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esercitazione_M5_Seconda_Settimana.Controllers
{
    [Authorize]
    public class CamereController : Controller
    {
        private ICamereService _camereService;

        public CamereController(ICamereService camereService)
        {
            _camereService = camereService;
        }
        public IActionResult CreaCamera()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreaCamera(Camera camera)
        {
            var c = _camereService.CreaCamera(camera);
            return RedirectToAction("Index", "Home");
        }
    }
}
