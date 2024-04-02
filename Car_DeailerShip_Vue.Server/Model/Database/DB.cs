using Microsoft.Data.SqlClient;

namespace Car_DeailerShip_Vue.Server.Model.Database
{
    public class DB
    {
        SqlConnection conn = new SqlConnection("Server=DESKTOP-ODE6OML\\SQLEXPRESS;Database=Автодиллер;Trusted_Connection=True;TrustServerCertificate=Yes;pooling=false;");

        public void openconn()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

        }
        public void closedconn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        public SqlConnection getconn()
        {
            return conn;
        }
    }
}
