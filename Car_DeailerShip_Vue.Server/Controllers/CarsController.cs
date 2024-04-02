using Car_DeailerShip_Vue.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Car_DeailerShip_Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase

    {
        IConfiguration configuration;
        public CarsController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        // GET: api/<CarsController>
        [HttpPost]
        public async Task<ActionResult<string>> PostXML()
        {
            GetDataXML getDataXML = new GetDataXML();
            await getDataXML.GetAuto();
            return "OK";
        }
        [Route("GetCar")]
        [HttpGet]
        public JsonResult GetCar()
        {
            string query = "SELECT CAST(Машины.Марка AS nvarchar) + ' ' + CAST(Машины.Модель AS NVARCHAR) + ',' + CAST(Машины.Год AS nvarchar) as Заголовок, Описание, Машины.Цена,Машины.Номер_машины FROM Публикация\r\nJOIN Машины\r\nON Публикация.Номер_машины = Машины.Номер_машины";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using(SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(dt);
        }

        [Route("GetCarImages")]
        [HttpGet]
        public JsonResult GetCarImages(int car)
        {
            string query = "SELECT Список_изображений FROM Публикация WHERE Номер_машины = @count";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@count", car);
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }

            int idImg = 0;
            foreach(DataRow row in dt.Rows)
            {
                idImg = Convert.ToInt32(row["Список_изображений"].ToString());
            }
            query = "SELECT Изображение FROM Изображения WHERE Номер_списка=@num";
            dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@num", idImg);
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(dt);
        }


        //// GET api/<CarsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CarsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CarsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CarsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [Route("CarFind")]
        [HttpGet]
        public JsonResult CarFind(int id)
        {
            string query = "SELECT CAST(Машины.Марка AS nvarchar) + ' ' + CAST(Машины.Модель AS NVARCHAR) + ',' + CAST(Машины.Год AS nvarchar) as Заголовок, Описание, Машины.Цена,Машины.Номер_машины,Дополнительные_услуги,Машины.Объем_двигателя,Машины.Мощность,Машины.Коробка,Машины.Привод,Машины.Тип_руля,Машины.VIN,Машины.Тип_автомобиля FROM Публикация\r\nJOIN Машины\r\nON Публикация.Номер_машины = Машины.Номер_машины \r\nWHERE Машины.Номер_машины=@num";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@num",id);
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(dt);
        }
    }
}
