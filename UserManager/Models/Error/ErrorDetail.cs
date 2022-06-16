using System.Text.Json;

namespace UserManagement.UserManager.Models.Error;

public class ErrorDetail
{
    public int StatusCode { get;private set; }
    public string Message { get;private set; } = string.Empty;

    public DateTime Time { get;private set; }

    public ErrorDetail(Exception ex, int statusCode, DateTime time)
    {
        StatusCode = statusCode;
        Message = ex.Message;
        Time = time;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}