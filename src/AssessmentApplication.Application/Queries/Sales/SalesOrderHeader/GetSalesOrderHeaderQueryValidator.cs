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
            When(x => x.DueDateEnd != null && x.DueDateStart != null, () =>
            {
                RuleFor(x => x.DueDateEnd).NotEmpty();
                RuleFor(x => x.DueDateStart).NotEmpty();
                RuleFor(x => x.DueDateEnd)
                    .GreaterThanOrEqualTo(x => x.DueDateStart)
                    .WithMessage("Due Date End Must Occur On or After Due Date Start");
                RuleFor(x => x.DueDateStart)
                    .LessThanOrEqualTo(x => x.DueDateEnd)
                    .WithMessage("Due Date Start Must Occur On or Before Due Date End");
            });

            When(x => x.OrderDateEnd != null && x.OrderDateStart != null, () =>
            {
                RuleFor(x => x.OrderDateEnd).NotEmpty();
                RuleFor(x => x.OrderDateStart).NotEmpty();
                RuleFor(x => x.OrderDateEnd)
                    .GreaterThanOrEqualTo(x => x.OrderDateStart)
                    .WithMessage("Order Date End Must Occur On or After Order Date Start");
                RuleFor(x => x.OrderDateStart)
                    .LessThanOrEqualTo(x => x.OrderDateEnd)
                    .WithMessage("Order Date Start Must Occur On or Before Order Date End");
            });

            When(x => x.ShipDateEnd != null && x.ShipDateStart != null, () =>
            {
                RuleFor(x => x.ShipDateEnd).NotEmpty();
                RuleFor(x => x.ShipDateStart).NotEmpty();
                RuleFor(x => x.ShipDateEnd)
                    .GreaterThanOrEqualTo(x => x.ShipDateStart)
                    .WithMessage("Ship Date End Must Occur On or After Ship Date Start");
                RuleFor(x => x.ShipDateStart)
                    .LessThanOrEqualTo(x => x.ShipDateEnd)
                    .WithMessage("Ship Date Start Must Occur On or Before Ship Date End");
            });
        }
    }
}
