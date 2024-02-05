namespace Common.Http
{
    public class ErrorResponse
    {
        public required ErrorDetail Error { get; set; }
    }

    public class ErrorDetail
    {
        public string? Code { get; set; }
        public string? Target { get; set; }
        public string? Message { get; set; }
        public ICollection<ErrorDetail>? Details { get; set; }
        public InnerError? InnerError { get; set; }
    }

    public class InnerError
    {
        public string? Code { get; set; }
        public InnerError? Error { get; set; }
    }
}
