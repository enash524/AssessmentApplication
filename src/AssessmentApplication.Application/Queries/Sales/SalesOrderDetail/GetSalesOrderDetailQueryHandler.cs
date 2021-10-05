using System.Collections.Generic;
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
    public class GetSalesOrderDetailQueryHandler : IRequestHandler<GetSalesOrderDetailQuery, QueryResult<List<SalesOrderDetailEntity>>>
    {
        private readonly ILogger<GetSalesOrderDetailQueryHandler> _logger;
        private readonly ISalesRepository _salesOrderDetailRepository;
        private readonly IValidator<GetSalesOrderDetailQuery> _validator;

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
        public async Task<QueryResult<List<SalesOrderDetailEntity>>> Handle(GetSalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                _logger.LogError(
                    "GetSalesOrderDetailQuery with SalesOrderDetailId: {SalesOrderDetailId} produced errors on validation {errors}",
                    request.SalesOrderDetailId,
                    validationResult.ToString());

                return new QueryResult<List<SalesOrderDetailEntity>>
                {
                    QueryResultType = QueryResultType.Invalid,
                    Result = null
                };
            }

            List<SalesOrderDetailEntity> entity = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(request.SalesOrderDetailId, cancellationToken);

            if (entity == null)
            {
                _logger.LogError("GetSalesOrderDetailQuery with SalesOrderDetailId: {SalesOrderDetailId} was not found.", request.SalesOrderDetailId);

                return new QueryResult<List<SalesOrderDetailEntity>>
                {
                    QueryResultType = QueryResultType.NotFound,
                    Result = null
                };
            }

            return new QueryResult<List<SalesOrderDetailEntity>>
            {
                Result = entity,
                QueryResultType = QueryResultType.Success
            };
        }
    }
}
