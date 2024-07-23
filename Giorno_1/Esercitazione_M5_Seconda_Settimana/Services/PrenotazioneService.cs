using Esercitazione_M5_Seconda_Settimana.Models;
using Microsoft.Data.SqlClient;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public class PrenotazioneService : IPrenotazioneService
    {
        private readonly string connectionstring;
        private const string CREA_PRENOTAZIONE = "INSERT INTO Prenotazioni (DataPrenotazione, NumProgressivo, Anno, SoggiornoDal, SoggiornoAl, Caparra, Tariffa, PensioneCompleta, IdCliente, IdCamera) OUTPUT INSERTED.IdPrenotazione VALUES (@DataPrenotazione, @NumProgressivo, @Anno, @SoggiornoDal, @SoggiornoAl, @Caparra, @Tariffa, @PensioneCompleta, @IdCliente, @IdCamera)";
        private const string CREA_PRENOTAZIONE_SERVIZIO = "INSERT INTO PrenotazioniServizi (IdPrenotazione, IdServizio, Data, Quantita, Prezzo) OUTPUT Inserted.IdPS VALUES (@IdPrenotazione, @IdServizio, @Data, @Quantita, @Prezzo)";
        public PrenotazioneService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("AuthDb")!;
        }


        public Prenotazione CreaPrenotazione(Prenotazione prenotazione)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(CREA_PRENOTAZIONE, conn);
                cmd.Parameters.AddWithValue("DataPrenotazione", prenotazione.DataPrenotazione);
                cmd.Parameters.AddWithValue("NumProgressivo", prenotazione.NumProgressivo);
                cmd.Parameters.AddWithValue("Anno", prenotazione.Anno);
                cmd.Parameters.AddWithValue("SoggiornoDal", prenotazione.SoggiornoDal);
                cmd.Parameters.AddWithValue("SoggiornoAl", prenotazione.SoggiornoAl);
                cmd.Parameters.AddWithValue("Caparra", prenotazione.Caparra);
                cmd.Parameters.AddWithValue("Tariffa", prenotazione.Tariffa);
                cmd.Parameters.AddWithValue("PensioneCompleta", prenotazione.PensioneCompleta);
                cmd.Parameters.AddWithValue("IdCliente", prenotazione.IdCliente);
                cmd.Parameters.AddWithValue("IdCamera", prenotazione.IdCamera);
                prenotazione.IdPrenotazione = (int)cmd.ExecuteScalar();
                return prenotazione;

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public PrenotazioniServizio CreaPrenotazioneServizio(PrenotazioniServizio p)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(CREA_PRENOTAZIONE_SERVIZIO, conn);
                cmd.Parameters.AddWithValue("IdPrenotazione", p.IdPrenotazione);
                cmd.Parameters.AddWithValue("IdServizio", p.IdServizio);
                cmd.Parameters.AddWithValue("Data", p.Data);
                cmd.Parameters.AddWithValue("Quantita", p.Quantita);
                cmd.Parameters.AddWithValue("Prezzo", p.Prezzo);
                p.IdPS = (int)cmd.ExecuteScalar();
                return p;

            }
            catch (Exception ex) { }
            return null;
        }
    }
}