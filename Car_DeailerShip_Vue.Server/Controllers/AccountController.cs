using Car_DeailerShip_Vue.Server.Model;
using Car_DeailerShip_Vue.Server.Model.Encryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Car_DeailerShip_Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IConfiguration configuration;
        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: api/<AccountController>
        [HttpPost]
        [Route("token")]

        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);
        }
        private ClaimsIdentity GetIdentity(string login, string password)
        {
            string query = "SELECT Роль,Логин FROM Пользователи WHERE CONVERT(VARCHAR, Логин) = @login AND CONVERT(VARCHAR, Пароль) = @password";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@login", EncryptionData.Encrypt(login));
                    command.Parameters.AddWithValue("@password", EncryptionData.Encrypt(password));
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt.Load(reader);

                    connection.Close();
                }
                string logins = "";
                string roles = "";
                foreach (DataRow rowView in dt.Rows)
                {
                    logins = rowView["Логин"].ToString();
                    roles = rowView["Роль"].ToString();
                }
                if (logins != "")
                {
                    var claims = new List<Claim>
                    {
                        new Claim("username", logins),
                        new Claim("role", roles)
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
                return null;

            }

        }

        // GET api/<AccountController>/5
        [Route("Register")]
        [HttpPost]
        public string Register(string surname, string name, string middlename, string login, string password, string role)
        {
            string query = "INSERT INTO Пользователи VALUES(@id,@surname,@name,@middlename,@login,@password,@role)";
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            var hash = DateTime.Now.ToString().GetHashCode().ToString("x");
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var keySize = 2048;
                    var rsaCryptoServiceProvider = new RSACryptoServiceProvider(keySize);


                    command.Parameters.AddWithValue("@id", hash.ToString());
                    command.Parameters.AddWithValue("@login", EncryptionData.Encrypt(login));

                    command.Parameters.AddWithValue("@password", EncryptionData.Encrypt(password));
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@middlename", middlename);
                    command.Parameters.AddWithValue("@role", role);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return hash.ToString();
        }
        [HttpGet]
        [Route("GetUserId")]
        public string getUserId(string Login)
        {
            string query = "SELECT Идентификатор FROM Пользователи WHERE CONVERT(VARCHAR, Логин) = @login";
            DataTable dt = new DataTable();
            string _sqlDataSource = configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@login", Login);

                    connection.Open();
                    reader = command.ExecuteReader();
                    dt.Load(reader);

                    connection.Close();
                }
            }
            string UserId = "";

            foreach (DataRow rowView in dt.Rows)
            {
                UserId = rowView["Идентификатор"].ToString();

            }
            return UserId;
        }
    }
}


        // POST api/<AccountController>
        

        // PUT api/<AccountController>/5
       

        // DELETE api/<AccountController>/5
       
