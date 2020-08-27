using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Requests;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderHeader
{
    /// <summary>
    /// Contains methods for handling the GetSalesOrderHeaderQuery
    /// </summary>
    public class GetSalesOrderHeaderQueryHandler : IRequestHandler<GetSalesOrderHeaderQuery, PagedResponse<List<SalesOrderHeaderEntity>>>
    {
        private readonly ILogger<GetSalesOrderHeaderQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesOrderDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderHeaderQuery`1"/> class.
        /// </summary>
        public GetSalesOrderHeaderQueryHandler(
            ILogger<GetSalesOrderHeaderQueryHandler> logger,
            IMapper mapper, ISalesRepository salesOrderDetailRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        /// <summary>
        /// Handles the GetSalesOrderHeaderQuery
        /// </summary>
        /// <param name="request">The GetSalesOrderHeaderQuery input parameters</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task representing a list of SalesOrderHeaderEntity wrapped in a PagedResponse</returns>
        public Task<PagedResponse<List<SalesOrderHeaderEntity>>> Handle(GetSalesOrderHeaderQuery request, CancellationToken cancellationToken)
        {
            SalesOrderHeaderRequest dataRequest = _mapper.Map<SalesOrderHeaderRequest>(request);
            return _salesOrderDetailRepository.GetSalesOrderHeaderAsync(dataRequest, cancellationToken);
        }
    }
}
