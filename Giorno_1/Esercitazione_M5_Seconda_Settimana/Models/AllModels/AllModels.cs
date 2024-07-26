namespace Esercitazione_M5_Seconda_Settimana.Models.AllModels
{
    public class AllModels
    {
        public PrenotazioniCamerePeriodoTariffa Prenotazione { get; set; }
        public List<ServizioWithQuantity> Servizio { get; set; } = new List<ServizioWithQuantity>();
        public decimal Importo { get; set; }
    }
}
