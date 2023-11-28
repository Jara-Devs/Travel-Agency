using System.Net;

namespace Travel_Agency_Core;

public class ApiResponse<T>
{
    public ApiResponse(HttpStatusCode status, string message = "")
    {
        Message = message;
        Status = status;
    }

    public ApiResponse(T value)
    {
        Value = value;
        Message = "Ok";
        Status = HttpStatusCode.Accepted;
    }

    public HttpStatusCode Status { get; }

    public string Message { get; }

    public T? Value { get; set; }

    public bool Ok => HttpStatusCode.Accepted == Status;

    public ApiResponse<T1> ConvertApiResponse<T1>()
    {
        return new ApiResponse<T1>(Status, Message);
    }

    public ApiResponse ConvertApiResponse()
    {
        return new ApiResponse(Status, Message);
    }
}

public class ApiResponse
{
    public ApiResponse(HttpStatusCode status, string message = "")
    {
        Message = message;
        Status = status;
    }

    public ApiResponse()
    {
        Status = HttpStatusCode.Accepted;
        Message = "Ok";
    }

    public HttpStatusCode Status { get; }

    public string Message { get; }

    public bool Ok => HttpStatusCode.Accepted == Status;

    public ApiResponse<T> ConvertApiResponse<T>()
    {
        return new ApiResponse<T>(Status, Message);
    }
}

public class NotFound : ApiResponse
{
    public NotFound(string message = "") : base(HttpStatusCode.NotFound, message)
    {
    }
}

public class BadRequest : ApiResponse
{
    public BadRequest(string message = "") : base(HttpStatusCode.BadRequest, message)
    {
    }
}

public class NotFound<T> : ApiResponse<T>
{
    public NotFound(string message = "") : base(HttpStatusCode.NotFound, message)
    {
    }
}

public class BadRequest<T> : ApiResponse<T>
{
    public BadRequest(string message = "") : base(HttpStatusCode.BadRequest, message)
    {
    }
}

public class Unauthorized<T> : ApiResponse<T>
{
    public Unauthorized(string message = "") : base(HttpStatusCode.Unauthorized, message)
    {
    }
}

public class Unauthorized : ApiResponse
{
    public Unauthorized(string message = "") : base(HttpStatusCode.Unauthorized, message)
    {
    }
}