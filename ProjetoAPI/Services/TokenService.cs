using Microsoft.IdentityModel.Tokens;
using NHibernate;
<<<<<<< HEAD
=======
using NHibernate.Linq;
>>>>>>> b27b432 (Adicionando API)
using ProjetoAPI.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ISessionFactory sessionFactory;

        public TokenService(IConfiguration configuration, ISessionFactory sessionFactory)
        {
            _configuration = configuration;
            this.sessionFactory = sessionFactory;
        }

<<<<<<< HEAD
        public string GenerateJwtToken(string email)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = session.Query<DonoPosto>().FirstOrDefault(d => d.Email == email);
=======
        public async Task<string> GenerateJwtToken(string email)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.Query<DonoPosto>().FirstOrDefaultAsync(d => d.Email == email);
>>>>>>> b27b432 (Adicionando API)
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, donoPosto.DonoPostoId.ToString()),
            new Claim(ClaimTypes.Name, donoPosto.Nome)
        };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
