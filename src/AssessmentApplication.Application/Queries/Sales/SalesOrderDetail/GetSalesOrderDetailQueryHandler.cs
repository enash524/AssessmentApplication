using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderDetail
{
    /// <summary>
    /// Contains methods for handling the GetSalesOrderDetail query
    /// </summary>
    public class GetSalesOrderDetailQueryHandler : IRequestHandler<GetSalesOrderDetailQuery, QueryResult<SalesOrderDetailEntity>>
    {
        private ILogger<GetSalesOrderDetailQueryHandler> _logger;
        private ISalesRepository _salesOrderDetailRepository;
        private IValidator<GetSalesOrderDetailQuery> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderDetailQueryHandler`1"/> class.
        /// </summary>
        public GetSalesOrderDetailQueryHandler(
            ILogger<GetSalesOrderDetailQueryHandler> logger,
            ISalesRepository salesOrderDetailRepository,
            IValidator<GetSalesOrderDetailQuery> validator)
        {
            _logger = logger;
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the GetSalesOrderDetail query
        /// </summary>
        /// <param name="request">The GetSalesOrderDetail query input parameters</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task representing the SalesOrderDetailEntity wrapped in a QueryResult</returns>
        public async Task<QueryResult<SalesOrderDetailEntity>> Handle(GetSalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validation = _validator.Validate(request);

            if (!validation.IsValid)
            {
                // TODO - SET PROPER LOGGING MESSAGE!!!
                _logger.LogError("asdf");

                return new QueryResult<SalesOrderDetailEntity>
                {
                    Result = null,
                    QueryResultType = QueryResultType.Invalid
                };
            }

            SalesOrderDetailEntity entity = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(request.SalesOrderDetailId, cancellationToken);

            if (entity == null)
            {
                return new QueryResult<SalesOrderDetailEntity>
                {
                    Result = null,
                    QueryResultType = QueryResultType.NotFound
                };
            }

            return new QueryResult<SalesOrderDetailEntity>
            {
                Result = entity,
                QueryResultType = QueryResultType.Success
            };
        }
    }
}
