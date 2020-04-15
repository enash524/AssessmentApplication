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

namespace AssessmentApplication.Application.Sales.SalesOrderHeader.Queries
{
    public class GetSalesOrderHeaderQueryHandler : IRequestHandler<GetSalesOrderHeaderQuery, PagedResponse<List<SalesOrderHeaderEntity>>>
    {
        private readonly ILogger<GetSalesOrderHeaderQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesOrderDetailRepository;

        public GetSalesOrderHeaderQueryHandler(ILogger<GetSalesOrderHeaderQueryHandler> logger, IMapper mapper, ISalesRepository salesOrderDetailRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public Task<PagedResponse<List<SalesOrderHeaderEntity>>> Handle(GetSalesOrderHeaderQuery request, CancellationToken cancellationToken)
        {
            SalesOrderHeaderRequest dataRequest = _mapper.Map<SalesOrderHeaderRequest>(request);
            return _salesOrderDetailRepository.GetSalesOrderHeaderAsync(dataRequest, cancellationToken);
        }
    }
}