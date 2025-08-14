namespace BankingSystem.Api.Models.Response;

public class UserLoginResponse
{
    /// <summary>
    /// User id
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Name of the user
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Access Token
    /// </summary>
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// Token issue time
    /// </summary>
    public DateTime TokenIssuedAt { get; set; }

    /// <summary>
    /// Token Expiry time
    /// </summary>
    public DateTime TokenExpiresAt { get; set; }

    /// <summary>
    /// Refresh token 
    /// </summary>
    public string RefreshToken { get; set; } = null!;

    /// <summary>
    /// Refresh token expiry time
    /// </summary>
    public DateTime RefreshTokenExpiresAt { get; set; }
}
