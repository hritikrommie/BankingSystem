namespace BankingSystem.Core.Dtos;

public class TokenDtos
{
    public string AccessToken { get; set; } = null!;
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiresAt { get; set; }
}
