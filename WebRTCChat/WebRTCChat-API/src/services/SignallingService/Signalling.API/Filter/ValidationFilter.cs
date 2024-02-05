using FluentValidation;

namespace Users.API.Filter
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            if (context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) is not T obj)
            {
                return Results.BadRequest();
            }

            var validationResult = await _validator.ValidateAsync(obj);

            return !validationResult.IsValid ? Results.BadRequest(string.Join("/n", validationResult.Errors)) : await next(context);
        }
    }
}
