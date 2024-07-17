using Terse.Models;

namespace Terse;

public static class Results
{
    public static IResult WrongType() =>
        Microsoft.AspNetCore.Http.Results.NotFound(new ErrorResponse
        {
            Code = 404,
            Message = "The tool can not be output in the specified type."
        });

    public static IResult NotFound(string? message) =>
        Microsoft.AspNetCore.Http.Results.NotFound(new ErrorResponse
        {
            Code = 404,
            Message = message
        });

    public static IResult NotFound(KeyNotFoundException e) => NotFound(e.Message);
}