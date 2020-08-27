using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Application.Queries.Person
{
    /// <summary>
    /// Contains methods for handling the GetPersonByIdQuery
    /// </summary>
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, QueryResult<PersonEntity>>
    {
        private readonly ILogger<GetPersonByIdQueryHandler> _logger;
        private readonly IPersonRepository _personRepository;
        private readonly IValidator<GetPersonByIdQuery> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonByIdQueryHandler`1"/> class.
        /// </summary>
        public GetPersonByIdQueryHandler(
            ILogger<GetPersonByIdQueryHandler> logger,
            IPersonRepository personRepository,
            IValidator<GetPersonByIdQuery> validator)
        {
            _logger = logger;
            _personRepository = personRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the GetPersonByIdQuery
        /// </summary>
        /// <param name="request">The GetPersonByIdQuery input parameters</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task representing the PersonEntity wrapped in a QueryResult</returns>
        public async Task<QueryResult<PersonEntity>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validation = _validator.Validate(request);

            if (!validation.IsValid)
            {
                _logger.LogError("GetPersonByIdQuery with BusinessEntityId: {businessEntityId} produced errors on validation {errors}", request.BusinessEntityId, validation.ToString());

                return new QueryResult<PersonEntity>
                {
                    Result = null,
                    QueryResultType = QueryResultType.Invalid
                };
            }

            PersonEntity entity = await _personRepository.GetPersonByIdAsync(request.BusinessEntityId, cancellationToken);

            if (entity == null)
            {
                return new QueryResult<PersonEntity>
                {
                    Result = null,
                    QueryResultType = QueryResultType.NotFound
                };
            }

            return new QueryResult<PersonEntity>
            {
                Result = entity,
                QueryResultType = QueryResultType.Success
            };
        }
    }
}
