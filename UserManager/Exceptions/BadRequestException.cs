using System.Net;

namespace UserManagement.UserManager.Exceptions;

public class BadRequestException : Exception
{
    public HttpStatusCode Status { get; private set; } = HttpStatusCode.BadRequest;

    public BadRequestException(string msg) : base(msg)
    {

    }
}
