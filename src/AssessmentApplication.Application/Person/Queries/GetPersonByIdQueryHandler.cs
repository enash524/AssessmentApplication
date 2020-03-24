using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Person.Queries
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonEntity>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonByIdQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<PersonEntity> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return _personRepository.GetPersonByIdAsync(request.BusinessEntityId, cancellationToken);
        }
    }
}