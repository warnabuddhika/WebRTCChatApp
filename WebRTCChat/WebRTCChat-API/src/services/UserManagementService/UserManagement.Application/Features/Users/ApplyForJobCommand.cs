using MediatR;
using System.Text.Json.Serialization;

namespace Boomerang.Employees.Application.Features.Employees
{
    public class ApplyForJobCommand : IRequest<bool>
    {
        [JsonIgnore]
        public Guid ApplicantId { get; set; }
        public Guid JobId { get; set; }
    }
}
