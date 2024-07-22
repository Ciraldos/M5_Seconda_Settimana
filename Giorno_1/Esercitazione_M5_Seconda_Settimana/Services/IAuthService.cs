using Esercitazione_M5_Seconda_Settimana.Models;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public interface IAuthService
    {
        public Users Login(string Username, string Password);
        public Users Register(string Username, string Password);

    }
}
