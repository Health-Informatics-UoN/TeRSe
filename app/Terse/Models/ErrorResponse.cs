namespace Terse.Models;

public class ErrorResponse
{
    public required int Code { get; set; }

    public string? Message { get; set; }
}