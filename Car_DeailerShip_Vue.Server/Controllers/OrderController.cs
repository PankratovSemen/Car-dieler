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
            string query = "SELECT Машины.Марка + ' ' + Машины.Модель as Название_машины, Машины.Цена,Машины.VIN, Номер_заказа,Статус FROM Заказы \r\nJOIN Машины ON Заказы.Номер_машины = Машины.Номер_машины\r\nWHERE CONVERT(VARCHAR, Идентификатор_пользователя)=@id";
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
        [HttpPost]
        [Route("SetStatus")]
        public IActionResult SetStatus(string OrderId, string Status)
        {
            string query = "UPDATE Заказы SET Статус=@status WHERE Номер_заказа=@order";
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;

            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {



                    command.Parameters.AddWithValue("@status", Status);
                    command.Parameters.AddWithValue("@order", OrderId);

                   
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return new JsonResult("OK");
                }
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetOrderAll")]
        public IActionResult GetOrderAll()
        {
            string query = "SELECT Машины.Марка + ' ' + Машины.Модель as Название_машины, Машины.Цена, Номер_заказа,Статус FROM Заказы \r\nJOIN Машины ON Заказы.Номер_машины = Машины.Номер_машины\r\n";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                   
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

        [HttpGet]
        [Route("GetOrderId")]
        public IActionResult GetOrderId(string idorder)
        {
            string query = "SELECT Машины.Марка + ' ' + Машины.Модель as Название_машины, Машины.Цена,Машины.VIN, Номер_заказа,Статус,Идентификатор_пользователя FROM Заказы \r\nJOIN Машины ON Заказы.Номер_машины = Машины.Номер_машины\r\nWHERE CONVERT(VARCHAR, Номер_заказа)=@id";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@id", idorder);
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
    }
}


