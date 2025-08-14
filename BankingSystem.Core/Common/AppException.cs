namespace BankingSystem.Core.Common;

public class AppException : ApplicationException
{
    public int? StatusCode;
    public AppException(string message, int code = 500) : base(message)
    {
        StatusCode = code;
    }
}
