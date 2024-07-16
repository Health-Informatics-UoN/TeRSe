namespace Terse.Models;

public class ErrorResponse
{
    public required int Code { get; set; }

    public required string Message { get; set; }
}