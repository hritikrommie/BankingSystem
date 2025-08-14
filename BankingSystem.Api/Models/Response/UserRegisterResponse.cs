namespace BankingSystem.Api.Models.Response;

public class UserRegisterResponse
{
    /// <summary>
    /// User Id, This will be used for further login and account/transactions work
    /// </summary>
    public int CustomerId { get; set; }
}
