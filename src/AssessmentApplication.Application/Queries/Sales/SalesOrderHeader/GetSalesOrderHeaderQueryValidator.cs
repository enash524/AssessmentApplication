using FluentValidation;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderHeader
{
    /// <summary>
    /// Validates the input parameters for the GetSalesOrderHeaderQuery
    /// </summary>
    public class GetSalesOrderHeaderQueryValidator : AbstractValidator<GetSalesOrderHeaderQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderHeaderQueryValidator`1"/> class.
        /// </summary>
        public GetSalesOrderHeaderQueryValidator()
        {
        }
    }
}
