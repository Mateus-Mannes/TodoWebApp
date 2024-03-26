using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.Domain;
using TodoApp.Options;
using TodoApp.ViewModels;

namespace TodoApp.Services;

public class TokenService
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public TokenViewModel GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new List<Claim>() { 
                new Claim(ClaimTypes.Name, user.Slug),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, "user"),
            }),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.UtcNow.AddHours(2),

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenViewModel(tokenHandler.WriteToken(token), tokenDescriptor.Expires);
    }
}
