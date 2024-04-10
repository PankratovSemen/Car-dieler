using Car_DeailerShip_Vue.Server.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           // укзывает, будет ли валидироваться издатель при валидации токена
                           ValidateIssuer = true,
                           // строка, представляющая издателя
                           ValidIssuer = AuthOptions.ISSUER,

                           // будет ли валидироваться потребитель токена
                           ValidateAudience = true,
                           // установка потребителя токена
                           ValidAudience = AuthOptions.AUDIENCE,
                           // будет ли валидироваться время существования
                           ValidateLifetime = true,

                           // установка ключа безопасности
                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                           // валидация ключа безопасности
                           ValidateIssuerSigningKey = true,
                       };
                   });

//Json Serialize
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opttions =>
opttions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());

var app = builder.Build();
app.UseCors(builder =>
{
    builder
        .WithOrigins("https://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader();

});
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
