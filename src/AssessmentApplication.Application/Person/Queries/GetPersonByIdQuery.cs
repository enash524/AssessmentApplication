using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Person.Queries
{
    public class GetPersonByIdQuery : IRequest<PersonEntity>
    {
        public int BusinessEntityId { get; set; }
    }
}