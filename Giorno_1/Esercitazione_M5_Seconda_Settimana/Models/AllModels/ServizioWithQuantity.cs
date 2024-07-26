namespace Esercitazione_M5_Seconda_Settimana.Models.AllModels
{
    public class ServizioWithQuantity
    {
        public int IdServizio { get; set; }
        public string Descrizione { get; set; }

        public int Quantita { get; set; }

        public decimal Prezzo { get; set; }
        public decimal PrezzoTot { get; set; }
    }
}
