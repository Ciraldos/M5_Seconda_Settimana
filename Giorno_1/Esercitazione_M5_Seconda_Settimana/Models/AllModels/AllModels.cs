namespace Esercitazione_M5_Seconda_Settimana.Models.AllModels
{
    public class AllModels
    {
        public PrenotazioniCamerePeriodoTariffa Prenotazione { get; set; }
        public List<Servizio> Servizio { get; set; } = new List<Servizio>();
        public decimal Importo { get; set; }
    }
}
