using Esercitazione_M5_Seconda_Settimana.Models;
using Microsoft.Data.SqlClient;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public class CamereService : ICamereService
    {
        private string connectionstring;
        private const string COMMAND_CREATE_CAMERE = "INSERT INTO Camere (NumCamera, Descrizione, Tipologia) OUTPUT INSERTED.IdCamera Values (@NumCamera, @Descrizione, @Tipologia)";

        public CamereService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("AuthDb")!;
        }

        public Camera CreaCamera(Camera camera)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                using var cmd = new SqlCommand(COMMAND_CREATE_CAMERE, conn);
                cmd.Parameters.AddWithValue("@NumCamera", camera.NumCamera);
                cmd.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                cmd.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                camera.IdCamera = (int)cmd.ExecuteScalar();
                return camera;
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
