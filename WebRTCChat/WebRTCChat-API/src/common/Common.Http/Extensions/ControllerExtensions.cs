using Microsoft.AspNetCore.Mvc;

namespace Common.Http.Extensions
{
    public static class ControllerExtensions
    {
        public static OkObjectResult ToOkResponse<TResult>(this TResult result)
        {
           return new OkObjectResult(new Response<TResult>(result));
        }

        public static CreatedResult ToCreatedResponse<TResult>(this TResult result, Uri uri)
        {
            return new CreatedResult(uri, new Response<TResult>(result));
        }

       
    }
}
