using Car_DeailerShip_Vue.Server.Model.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Cryptography;

namespace Car_DeailerShip_Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IConfiguration configuration;
        public OrderController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("GetOrder")]
        public IActionResult GetOrder(string userId)
        {
            string query = "SELECT Машины.Марка + ' ' + Машины.Модель as Название_машины, Машины.Цена, Номер_заказа,Статус FROM Заказы \r\nJOIN Машины ON Заказы.Номер_машины = Машины.Номер_машины\r\nWHERE CONVERT(VARCHAR, Идентификатор_пользователя)=@id";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@id", userId);
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt.Load(reader);

                    connection.Close();
                    if (dt.Rows.Count > 0)
                    {
                        return new JsonResult(dt);
                    }
                }
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("SetOrder")]
        public IActionResult SetOrder(string userId, int carId, string Status)
        {
            string query = "INSERT INTO Заказы(Идентификатор_пользователя,Номер_машины,Статус) VALUES(@id,@carid,@status)";
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;

            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {



                    command.Parameters.AddWithValue("@id", userId);
                    command.Parameters.AddWithValue("@carid", carId);

                    command.Parameters.AddWithValue("@status", Status);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return new JsonResult("OK");
                }
            }
            return BadRequest();
        }
    }
}


