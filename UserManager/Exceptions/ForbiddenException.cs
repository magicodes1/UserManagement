using System.Net;

namespace UserManagement.UserManager.Exceptions;
public class ForbiddenException : Exception
{
    public HttpStatusCode Status { get; private set; } = HttpStatusCode.Forbidden;

    public ForbiddenException(string message) : base(message)
    {

    }
}