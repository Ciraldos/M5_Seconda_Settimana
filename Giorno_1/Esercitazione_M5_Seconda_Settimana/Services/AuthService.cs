using Esercitazione_M5_Seconda_Settimana.Models;
using Microsoft.Data.SqlClient;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public class AuthService : IAuthService
    {
        private string connectionstring;
        private const string LOGIN_COMMAND = "SELECT IdUser FROM Users Where Username = @Username AND Password = @Password";
        private const string REGISTER_COMMAND = "INSTERT INTO Users (Username, Password) OUTPUT INSERTED.IdUser Values (@username, @password)";
        public AuthService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("AuthDb")!;
        }

        public Users Login(string Username, string Password)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("Username", Username);
                cmd.Parameters.AddWithValue("Password", Password);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    var u = new Users { IdUser = r.GetInt32(0), Username = Username, Password = Password };
                    r.Close();
                    return u;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public Users Register(string Username, string Password)
        {
            try
            {
                using var conn = new SqlConnection(connectionstring);
                conn.Open();
                var cmd = new SqlCommand(REGISTER_COMMAND, conn);
                cmd.Parameters.AddWithValue("username", Username);
                cmd.Parameters.AddWithValue("password", Password);
                var IdUser = (int)cmd.ExecuteScalar();

                return new Users { IdUser = IdUser, Username = Username, Password = Password };
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
