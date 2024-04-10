﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Car_DeailerShip_Vue.Server.Model
{
    public class AuthOptions
    {
        public const string ISSUER = "CarsDelier"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "carsdelier_gjeooqseeffff2242422eddd2e222";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
