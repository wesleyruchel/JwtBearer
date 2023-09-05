using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtBearer.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtBearer.Services;

public class AuthService
{   
    private readonly Configuration _configuration;

    public AuthService(Configuration configuration)
    {
        _configuration = configuration;
    }

    public string GenereteToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetPrivateKey());
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddMinutes(30),
            Subject = GenereteClaims(user),
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenereteClaims(User user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));

        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role));

        return ci;
    }
}