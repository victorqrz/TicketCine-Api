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

var app = builder.Build();

// Configura o middleware de autenticação e autorização
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Adiciona o middleware de autenticação
app.UseAuthentication(); // Certifique-se de que o middleware de autenticação vem antes do de autorização
app.UseAuthorization();

app.MapControllers();

app.Run();
