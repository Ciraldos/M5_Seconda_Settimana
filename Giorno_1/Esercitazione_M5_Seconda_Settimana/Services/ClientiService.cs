using Esercitazione_M5_Seconda_Settimana.Models;
using Microsoft.Data.SqlClient;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public class ClientiService : IClientiService
    {
        private string connectionstring;
        private const string COMMAND_CREATE_CLIENTI = "INSERT INTO Clienti (CF, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) OUTPUT INSERTED.IdCliente Values (@CF, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";



        public ClientiService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("AuthDb")!;
        }

        public Cliente CreaCliente(Cliente cliente)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(COMMAND_CREATE_CLIENTI, conn);
                cmd.Parameters.AddWithValue("@CF", cliente.CF);
                cmd.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Citta", cliente.Citta);
                cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);
                cliente.IdCliente = (int)cmd.ExecuteScalar();
                return cliente;
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
