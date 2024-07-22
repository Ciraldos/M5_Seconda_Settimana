namespace Esercitazione_M5_Seconda_Settimana.Models
{
    public class PrenotazioniServizio
    {
        public int IdPS { get; set; }
        public int IdPrenotazione { get; set; }
        public int IdServizio { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }

        public Prenotazione Prenotazione { get; set; }
        public Servizio Servizio { get; set; }
    }
}
