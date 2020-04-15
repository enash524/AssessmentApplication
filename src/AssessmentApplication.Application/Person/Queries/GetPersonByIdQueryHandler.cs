using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Application.Person.Queries
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonEntity>
    {
        private readonly ILogger<GetPersonByIdQueryHandler> _logger;
        private readonly IPersonRepository _personRepository;

        public GetPersonByIdQueryHandler(ILogger<GetPersonByIdQueryHandler> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public Task<PersonEntity> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return _personRepository.GetPersonByIdAsync(request.BusinessEntityId, cancellationToken);
        }
    }
}