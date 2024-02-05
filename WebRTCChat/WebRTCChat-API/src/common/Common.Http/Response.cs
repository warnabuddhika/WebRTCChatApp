namespace Common.Http
{
    public class Response<TResult>
    {
        public TResult Data { get; init; }

        public Response(TResult data)
        {
            Data = data;
        }
    }

    public class SuccessResponse : Response<SuccessResult>
    {
        public SuccessResponse(bool success) : base(new SuccessResult(success)) { }
    }

    public class SuccessResult
    {
        public bool Success { get; init; }

        public SuccessResult(bool success)
        {
            Success = success;
        }
    }
}
