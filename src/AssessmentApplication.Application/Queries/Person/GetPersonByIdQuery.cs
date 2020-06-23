using AssessmentApplication.Application.Models;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Queries.Person
{
    /// <summary>
    /// GetPersonByIdQuery input parameters
    /// </summary>
    public class GetPersonByIdQuery : IRequest<QueryResult<PersonEntity>>
    {
        /// <summary>
        /// The business entity Id
        /// </summary>
        public int BusinessEntityId { get; set; }
    }
}