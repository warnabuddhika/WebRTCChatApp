using Ascentic.Bp.EventBus.Shared;
using Ascentic.Bp.Ddd.Domain.Exceptions;
using MediatR;
using Boomerang.Employees.Application.Features.Applicants.Events.Integration;
using System.Diagnostics.CodeAnalysis;
using Boomerang.Employee.Domain.AggregrateRoots.Applicants;

namespace Boomerang.Employees.Application.Features.Applicants
{
    public class ApplyForJobCommandHandler : IRequestHandler<ApplyForJobCommand,bool>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ISharedEventBus _sharedBus;

        public ApplyForJobCommandHandler(IApplicantRepository applicantRepository, ISharedEventBus sharedEventBus)
        {
            _applicantRepository = applicantRepository;
            _sharedBus = sharedEventBus;
        }

        public async Task<bool> Handle([NotNull]ApplyForJobCommand request, CancellationToken cancellationToken)
        {
            var applicant = await _applicantRepository.GetAsync(a => a.Id == request.ApplicantId, cancellationToken);

            if (applicant == null)
            {
               throw new EntityNotFoundException(typeof(Boomerang.Employee.Domain.AggregrateRoots.Employees.Employee), request.ApplicantId);
            }

            applicant.ApplyForJob(request.JobId);            

            await _applicantRepository.UpdateAsync(applicant, cancellationToken);

            await _applicantRepository.UnitOfWork.CommitChangesAsync(cancellationToken);

            await _sharedBus.Publish(new ApplicantCreatedIntegrationEvent
            {
                ApplicantId = applicant.Id,
                ApplicantName = applicant.Name,
                AppliedDate = DateTime.UtcNow,
                JobId = request.JobId,
            }, cancellationToken);

            return true;
        }
    }
}
