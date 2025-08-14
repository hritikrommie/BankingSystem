using BankingSystem.Core.Dtos;
using BankingSystem.Core.Entity;
using BankingSystem.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingSystem.Core.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<TokenDtos> GetToken(UserDtos user)
    {
        var secret = _configuration["Jwt:Key"];
        var audience = _configuration["Jwt:Audience"];
        var issuer = _configuration["Jwt:Issuer"];
        var expiredAt = _configuration["Jwt:ExpiresInMinutes"];

        var encodedSecret = Encoding.ASCII.GetBytes(secret);
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()!),
            new Claim(ClaimTypes.Role, user.Role!)
        });

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claims,
            Audience = audience,
            Issuer = issuer,
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddMinutes(Convert.ToInt32(expiredAt)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedSecret), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenData =  tokenHandler.WriteToken(token);
        var response = new TokenDtos()
        {
            AccessToken = tokenData,
            IssuedAt = DateTime.Now,
            ExpiredAt = DateTime.Now.AddMinutes(Convert.ToInt32(expiredAt)),
        };
        return Task.FromResult(response);
    }
}
