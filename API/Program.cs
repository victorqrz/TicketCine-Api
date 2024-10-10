using Microsoft.EntityFrameworkCore;
using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims; // Adicionado para reconhecer ClaimTypes

var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço de autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ticket-cine", // Issuer que você usou no token
            ValidAudience = "cinema",     // Audience que você usou no token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-keysecret-key")), // Chave secreta usada para assinar o token
            RoleClaimType = ClaimTypes.Role // Adicione esta linha
        };
    });

// Adiciona os serviços do banco de dados
builder.Services.AddDbContext<APIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TicketCineDB")));

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona a configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Permitir requisições apenas do Angular no localhost:4200
               .AllowAnyMethod()                     // Permitir todos os métodos (GET, POST, PUT, DELETE, etc.)
               .AllowAnyHeader()                     // Permitir todos os headers
               .AllowCredentials();                  // Permitir o uso de credenciais (cookies, tokens)
    });
});

var app = builder.Build();

// Configura o middleware de autenticação e autorização
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Ativa a política de CORS configurada anteriormente
app.UseCors("AllowAngular"); // Ativa o CORS para permitir requisições do Angular

// Adiciona o middleware de autenticação
app.UseAuthentication(); // Certifique-se de que o middleware de autenticação vem antes do de autorização
app.UseAuthorization();

app.MapControllers();

app.Run();
