using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Models.AllModels;
using Microsoft.Data.SqlClient;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public class PrenotazioneService : IPrenotazioneService
    {
        private readonly string connectionstring;
        private const string CREA_PRENOTAZIONE = "INSERT INTO Prenotazioni (DataPrenotazione, NumProgressivo, Anno, SoggiornoDal, SoggiornoAl, Caparra, Tariffa, PensioneCompleta, IdCliente, IdCamera) OUTPUT INSERTED.IdPrenotazione VALUES (@DataPrenotazione, @NumProgressivo, @Anno, @SoggiornoDal, @SoggiornoAl, @Caparra, @Tariffa, @PensioneCompleta, @IdCliente, @IdCamera)";
        private const string CREA_PRENOTAZIONE_SERVIZIO = "INSERT INTO PrenotazioniServizi (IdPrenotazione, IdServizio, Data, Quantita, Prezzo) OUTPUT Inserted.IdPS VALUES (@IdPrenotazione, @IdServizio, @Data, @Quantita, @Prezzo)";
        private const string PRENOTAZIONE_BY_CF = "SELECT P.IdPrenotazione, P.DataPrenotazione, P.NumProgressivo, P.Anno, P.SoggiornoDal, P.SoggiornoAl, P.Caparra, P.Tariffa, P.PensioneCompleta, P.IdCliente, P.IdCamera FROM Prenotazioni as P INNER JOIN Clienti as C ON P.IdCliente = C.IdCliente WHERE C.CF = @CF";
        private const string PRENOTAZIONE_BY_PENSIONE = "SELECT IdPrenotazione,DataPrenotazione,NumProgressivo, Anno, SoggiornoDal,SoggiornoAl, Caparra, Tariffa, PensioneCompleta, IdCliente, IdCamera FROM Prenotazioni WHERE PensioneCompleta = @Pensione";
        private const string GET_PRENOTAZIONI = "SELECT * FROM Prenotazioni";
        private const string GET_STANZA_PERIODO_TARIFFA = "SELECT C.NumCamera, P.SoggiornoDal, P.SoggiornoAl, P.Tariffa FROM Prenotazioni as P INNER JOIN Camere as C ON P.IdCamera = C.IdCamera WHERE P.IdPrenotazione = @Id";
        private const string GET_SERVIZI_BY_PRENOTAZIONE = "SELECT S.*, PS.Quantita, PS.Prezzo, (PS.Quantita * PS.Prezzo) as PrezzoTot FROM Servizi as S INNER JOIN PrenotazioniServizi as PS ON S.IdServizio = PS.IdServizio WHERE PS.IdPrenotazione = @Id";
        private const string GET_IMPORTO = "SELECT (p.Tariffa - p.Caparra + ISNULL(SUM(ps.Quantita * ps.Prezzo), 0)) AS ServizioPrezzo FROM Prenotazioni AS p LEFT JOIN PrenotazioniServizi AS ps ON p.IdPrenotazione = ps.IdPrenotazione WHERE p.IdPrenotazione = @Id GROUP BY p.Tariffa, p.Caparra";
        private const string GET_SERVIZI = "SELECT * FROM Servizi";
        public PrenotazioneService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("AuthDb")!;
        }


        public IEnumerable<Prenotazione> GetAll()
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            using var conn = new SqlConnection(connectionstring);
            conn.Open();
            using var cmd = new SqlCommand(GET_PRENOTAZIONI, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var p = new Prenotazione
                {
                    IdPrenotazione = reader.GetInt32(0),
                    DataPrenotazione = reader.GetDateTime(1),
                    NumProgressivo = reader.GetInt32(2),
                    Anno = reader.GetInt32(3),
                    SoggiornoDal = reader.GetDateTime(4),
                    SoggiornoAl = reader.GetDateTime(5),
                    Caparra = reader.GetDecimal(6),
                    Tariffa = reader.GetDecimal(7),
                    PensioneCompleta = reader.GetString(8),
                    IdCliente = reader.GetInt32(9),
                    IdCamera = reader.GetInt32(10)
                };
                prenotazioni.Add(p);
            }
            return prenotazioni;

        }
        public List<Servizio> GetServizi()
        {
            List<Servizio> servizi = new List<Servizio>();
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(GET_SERVIZI, conn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var s = new Servizio
                    {
                        IdServizio = r.GetInt32(0),
                        Descrizione = r.GetString(1),
                    };
                    servizi.Add(s);
                }
                return servizi;
            }
            catch (Exception ex) { }
            return null;
        }
        public AllModels Checkout(int id)
        {
            AllModels allModels = new AllModels();
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using (var cmd = new SqlCommand(GET_STANZA_PERIODO_TARIFFA, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            allModels.Prenotazione = new PrenotazioniCamerePeriodoTariffa
                            {
                                NumCamera = r.GetInt32(0),
                                SoggiornoDal = r.GetDateTime(1),
                                SoggiornoAl = r.GetDateTime(2),
                                Tariffa = r.GetDecimal(3)
                            };
                        }
                    }
                }
                using (var cmd2 = new SqlCommand(GET_SERVIZI_BY_PRENOTAZIONE, conn))
                {
                    cmd2.Parameters.AddWithValue("@Id", id);
                    using (var r2 = cmd2.ExecuteReader())
                    {
                        while (r2.Read())
                        {
                            var s = new ServizioWithQuantity
                            {
                                IdServizio = r2.GetInt32(0),
                                Descrizione = r2.GetString(1),
                                Quantita = r2.GetInt32(2),
                                Prezzo = r2.GetDecimal(3),
                                PrezzoTot = r2.GetDecimal(4),
                            };
                            allModels.Servizio.Add(s);
                        }
                    }
                }
                using (var cmd3 = new SqlCommand(GET_IMPORTO, conn))
                {
                    cmd3.Parameters.AddWithValue("@Id", id);
                    using (var r3 = cmd3.ExecuteReader())
                    {
                        if (r3.Read())
                        {
                            allModels.Importo = r3.GetDecimal(0);
                        }
                    }
                }

                return allModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return null;
            }
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

        //public PrenotazioniServizio CreaPrenotazioneServizio(PrenotazioniServizio p)
        //{
        //    try
        //    {
        //        using var conn = new SqlConnection(connectionstring);
        //        conn.Open();
        //        using var cmd = new SqlCommand(CREA_PRENOTAZIONE_SERVIZIO, conn);
        //        cmd.Parameters.AddWithValue("IdPrenotazione", p.IdPrenotazione);
        //        cmd.Parameters.AddWithValue("IdServizio", p.IdServizio);
        //        cmd.Parameters.AddWithValue("Data", p.Data);
        //        cmd.Parameters.AddWithValue("Quantita", p.Quantita);
        //        cmd.Parameters.AddWithValue("Prezzo", p.Prezzo);
        //        p.IdPS = (int)cmd.ExecuteScalar();
        //        return p;

        //    }
        //    catch (Exception ex) { }
        //    return null;
        //}

        public PrenotazioniServizio CreaPrenotazioneServizio(PrenotazioniServizio p, int id)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(CREA_PRENOTAZIONE_SERVIZIO, conn);
                cmd.Parameters.AddWithValue("IdPrenotazione", id);
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

        public IEnumerable<Prenotazione> PrenotazioneByCf(string cf)
        {
            var prenotazioni = new List<Prenotazione>();

            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(PRENOTAZIONE_BY_CF, conn);
                cmd.Parameters.AddWithValue("@CF", cf);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var p = new Prenotazione
                    {
                        IdPrenotazione = r.GetInt32(0),
                        DataPrenotazione = r.GetDateTime(1),
                        NumProgressivo = r.GetInt32(2),
                        Anno = r.GetInt32(3),
                        SoggiornoDal = r.GetDateTime(4),
                        SoggiornoAl = r.GetDateTime(5),
                        Caparra = r.GetDecimal(6),
                        Tariffa = r.GetDecimal(7),
                        PensioneCompleta = r.GetString(8),
                        IdCliente = r.GetInt32(9),
                        IdCamera = r.GetInt32(10),
                    };
                    prenotazioni.Add(p);
                }
                return prenotazioni;
            }
            catch { }
            return null;
        }
        public IEnumerable<Prenotazione> PrenotazioneByPensione(string p)
        {
            var prenotazioni = new List<Prenotazione>();

            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(PRENOTAZIONE_BY_PENSIONE, conn);
                cmd.Parameters.AddWithValue("@Pensione", p);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var prenotazione = new Prenotazione
                    {
                        IdPrenotazione = r.GetInt32(0),
                        DataPrenotazione = r.GetDateTime(1),
                        NumProgressivo = r.GetInt32(2),
                        Anno = r.GetInt32(3),
                        SoggiornoDal = r.GetDateTime(4),
                        SoggiornoAl = r.GetDateTime(5),
                        Caparra = r.GetDecimal(6),
                        Tariffa = r.GetDecimal(7),
                        PensioneCompleta = r.GetString(8),
                        IdCliente = r.GetInt32(9),
                        IdCamera = r.GetInt32(10),
                    };
                    prenotazioni.Add(prenotazione);
                }
                return prenotazioni;
            }
            catch { }
            return null;
        }
    }
}