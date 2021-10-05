using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Requests;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderHeader
{
    /// <summary>
    /// Contains methods for handling the GetSalesOrderHeaderQuery
    /// </summary>
    public class GetSalesOrderHeaderQueryHandler : IRequestHandler<GetSalesOrderHeaderQuery, QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>>
    {
        private readonly ILogger<GetSalesOrderHeaderQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesOrderDetailRepository;
        private readonly IValidator<GetSalesOrderHeaderQuery> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderHeaderQuery`1"/> class.
        /// </summary>
        public GetSalesOrderHeaderQueryHandler(
            ILogger<GetSalesOrderHeaderQueryHandler> logger,
            IMapper mapper,
            ISalesRepository salesOrderDetailRepository,
            IValidator<GetSalesOrderHeaderQuery> validator)
        {
            _logger = logger;
            _mapper = mapper;
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the GetSalesOrderHeaderQuery
        /// </summary>
        /// <param name="request">The GetSalesOrderHeaderQuery input parameters</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task representing a list of SalesOrderHeaderEntity wrapped in a PagedResponse</returns>
        public async Task<QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>> Handle(GetSalesOrderHeaderQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                _logger.LogError(validationResult.ToString());

                return new QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>
                {
                    QueryResultType = QueryResultType.Invalid,
                    Result = new PagedResponse<List<SalesOrderHeaderEntity>>
                    {
                        Data = null
                    }
                };
            }

            SalesOrderHeaderRequest dataRequest = _mapper.Map<SalesOrderHeaderRequest>(request);
            PagedResponse<List<SalesOrderHeaderEntity>> entity = await _salesOrderDetailRepository.GetSalesOrderHeaderAsync(dataRequest, cancellationToken);

            return new QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>
            {
                QueryResultType = QueryResultType.Success,
                Result = entity
            };
        }
    }
}
