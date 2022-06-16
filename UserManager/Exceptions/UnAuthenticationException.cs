using System.Net;

namespace UserManagement.UserManager.Exceptions;

public class UnAuthenticationException : Exception
{
    public HttpStatusCode Status { get; private set; } = HttpStatusCode.Unauthorized;
    public UnAuthenticationException(string message) : base(message)
    {

    }
}