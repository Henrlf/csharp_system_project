using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Franco.Sentry.Application.Auth.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Franco.Sentry.Application.Auth.Service;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(AuthJwt user)
    {
        var jwtSecret = _configuration["JWT:Secret"];

        if (jwtSecret is null)
        {
            throw new Exception("Não conseguimos pegar o secret do JWT.");
        }

        var claims = new List<Claim>
        {
            new("idUser", user.IdUser.ToString()),
            new("username", user.Name),
        };

        if (user.IsAdmin)
        {
            claims.Add(new Claim("role", "Admin"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(user.Hours),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}