using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(string secretKey, string issuer, string audience)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }

        public string Builder(string email, string role)
        {
            // Define a chave secreta para assinatura do token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Definir as claims (informações contidas no token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Autenticação Sistema"),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role), // Altere "entidade" para "role" (ClaimTypes.Role)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Identificador único do token
            };

            // Gerar o token com validade de 24 horas
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24), // Expiração em 24 horas
                signingCredentials: credentials
            );

            // Retornar o token como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
