using FluentValidation;

namespace AssessmentApplication.Application.Queries.Person
{
    /// <summary>
    /// Validates the input parameters for the GetPersonByIdQuery
    /// </summary>
    public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonByIdQueryValidator`1"/> class.
        /// </summary>
        public GetPersonByIdQueryValidator()
        {
            RuleFor(x => x.BusinessEntityId).NotNull();
            RuleFor(x => x.BusinessEntityId).GreaterThan(0);
        }
    }
}