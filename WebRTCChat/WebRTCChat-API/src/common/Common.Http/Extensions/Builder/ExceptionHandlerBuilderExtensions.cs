using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Http;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.AspNetCore.Builder;
#pragma warning restore IDE0130 // Namespace does not match folder structure

    public static class ExceptionHandlerBuilderExtensions
    {

        public static void UseApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var (code, error) = GetError(contextFeature.Error);

                        context.Response.StatusCode = code;
                        context.Response.ContentType = "application/json";

                        //Error log by Micrososft

                        var content = JsonSerializer.Serialize(error, new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                        });
                        await context.Response.WriteAsync(content);

                    }
                });
            });
        }

        private static (int code, ErrorResponse error) GetError(Exception exception)
        {
            var response = new ErrorResponse
            {
                Error = new ErrorDetail()
            };

            int code;
            if (exception is ValidationException ve)
            {
                code = (int)HttpStatusCode.BadRequest;
                response.Error.Code = "BadArgument";
                response.Error.Message = "One or more validation errors occurred.";
                response.Error.Details = new List<ErrorDetail>
                {
                    new ErrorDetail()
                    {
                        Target = ve.Parmaeter,
                        Message = ve.Message,
                    }
                };

            }
            else if (exception is EntityNotFoundException ne)
            {
                code = (int)HttpStatusCode.NotFound;

                response.Error.Code = "NotFound";
                response.Error.Message = "Entity not found";
                response.Error.Details = new ErrorDetail[]
                {
                    new ErrorDetail()
                    {
                        Code = "NotFound",
                        Target = ne.EntityType.Name,
                        Message = ne.Message,
                    }
                };
            }
            else if (exception is EntityStateConflictException ce)
            {
                code = (int)HttpStatusCode.Conflict;

                response.Error.Code = "Conflict";
                response.Error.Message = "Entity is in conflict state due to arguements";
                response.Error.Details = new ErrorDetail[]
                {
                    new ErrorDetail()
                    {
                        Code = "Conflict",
                        Target = ce.EntityType.Name,
                        Message = ce.Message,
                    }
                };
            }
            else if (exception is NotAllowedOperationException nae)
            {
                code = (int)HttpStatusCode.Forbidden;

                response.Error.Code = "AccessDenied";
                response.Error.Message = "Not allowed to execute operation due to insufficient access";
                response.Error.Details = new ErrorDetail[]
                {
                    new ErrorDetail()
                    {
                        Code = "AccessDenied",
                        Message = nae.Message,
                    }
                };
            }
            else
            {
                code = (int)HttpStatusCode.InternalServerError;
                response.Error.Code = "InternalError";
                response.Error.Message = exception.Message;
            }



            return (code, response);
        }
    }
