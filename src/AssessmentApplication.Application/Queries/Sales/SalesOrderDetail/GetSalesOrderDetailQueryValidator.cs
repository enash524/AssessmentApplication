using FluentValidation;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderDetail
{
    /// <summary>
    /// Validates the input parameters for the GetSalesOrderDetail query
    /// </summary>
    public class GetSalesOrderDetailQueryValidator : AbstractValidator<GetSalesOrderDetailQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderDetailQueryValidator`1"/> class.
        /// </summary>
        public GetSalesOrderDetailQueryValidator()
        {
            RuleFor(x => x.SalesOrderDetailId).NotNull();
            RuleFor(x => x.SalesOrderDetailId).GreaterThan(0);
        }
    }
}
