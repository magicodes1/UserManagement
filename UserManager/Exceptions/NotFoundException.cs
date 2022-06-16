using System.Net;


namespace UserManagement.UserManager.Exceptions;

public class NotFoundException : Exception
{
    public HttpStatusCode Status { get; private set; } = HttpStatusCode.NotFound;

    public NotFoundException(string msg) : base(msg)
    {

    }
}