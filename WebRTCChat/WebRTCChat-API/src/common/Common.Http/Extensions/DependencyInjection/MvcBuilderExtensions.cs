using Common.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder ConfigureApi(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var details = new List<ErrorDetail>();

                    foreach (var kvp in context.ModelState)
                    {
                        if (kvp.Value.Errors.Count > 0)
                        {
                            foreach (var error in kvp.Value.Errors)
                            {
                                var subError = new ErrorDetail
                                {
                                    Target = kvp.Key,
                                    Message = error.ErrorMessage
                                };

                                details.Add(subError);
                            }

                        }
                    }

                    return new BadRequestObjectResult(new ErrorResponse
                    {
                        Error = new ErrorDetail
                        {
                            Code = "BadArgument",
                            Message = "One or more validation errors occurred.",
                            Details = details.ToArray()
                        }
                    });
                };
            });

            return builder;
        }
    }
}
