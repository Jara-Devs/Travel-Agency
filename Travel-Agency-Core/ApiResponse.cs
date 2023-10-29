using System.Net;

namespace Travel_Agency_Core;

public class ApiResponse<T>
{
    public HttpStatusCode Status { get; private set; }

    public string Message { get; private set; }

    public T? Value { get; set; }

    public bool Ok => HttpStatusCode.Accepted == Status;

    public ApiResponse(HttpStatusCode status, string message = "")
    {
        this.Message = message;
        this.Status = status;
    }

    public ApiResponse(T value)
    {
        this.Value = value;
        this.Message = "Ok";
        this.Status = HttpStatusCode.Accepted;
    }

    public ApiResponse<T1> ConvertApiResponse<T1>() => new(this.Status, this.Message);
    public ApiResponse ConvertApiResponse() => new(this.Status, this.Message);
}

public class ApiResponse
{
    public HttpStatusCode Status { get; private set; }

    public string Message { get; private set; }

    public bool Ok => HttpStatusCode.Accepted == Status;

    public ApiResponse(HttpStatusCode status, string message = "")
    {
        this.Message = message;
        this.Status = status;
    }

    public ApiResponse()
    {
        this.Status = HttpStatusCode.Accepted;
        this.Message = "Ok";
    }

    public ApiResponse<T> ConvertApiResponse<T>() => new(this.Status, this.Message);
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