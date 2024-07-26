using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Models.AllModels;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public interface IPrenotazioneService
    {
        public Prenotazione CreaPrenotazione(Prenotazione p);
        public PrenotazioniServizio CreaPrenotazioneServizio(PrenotazioniServizio p, int id);
        public IEnumerable<Prenotazione> PrenotazioneByCf(string cf);
        public IEnumerable<Prenotazione> PrenotazioneByPensione(string p);
        public IEnumerable<Prenotazione> GetAll();
        public AllModels Checkout(int id);
        public List<Servizio> GetServizi();



    }
}
