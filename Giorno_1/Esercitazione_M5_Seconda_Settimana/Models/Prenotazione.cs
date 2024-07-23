namespace Esercitazione_M5_Seconda_Settimana.Models
{
    public class Prenotazione
    {
        public int IdPrenotazione { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumProgressivo { get; set; }
        public int Anno { get; set; }
        public DateTime SoggiornoDal { get; set; }
        public DateTime SoggiornoAl { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set; }
        public string PensioneCompleta { get; set; }
        public int IdCliente { get; set; }
        public int IdCamera { get; set; }
    }
}
