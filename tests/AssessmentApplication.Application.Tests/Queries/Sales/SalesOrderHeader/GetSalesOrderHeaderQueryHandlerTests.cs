using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Requests;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AssessmentApplication.Application.Tests.Queries.Sales.SalesOrderHeader
{
    public class GetSalesOrderHeaderQueryHandlerTests
    {
        private readonly Mock<ILogger<GetSalesOrderHeaderQueryHandler>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ISalesRepository> _salesRepository;
        private GetSalesOrderHeaderQueryHandler _handler;
        private IValidator<GetSalesOrderHeaderQuery> _validator;

        public GetSalesOrderHeaderQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetSalesOrderHeaderQueryHandler>>();
            _mapper = new Mock<IMapper>();
            _salesRepository = new Mock<ISalesRepository>();
            _validator = new GetSalesOrderHeaderQueryValidator();
            _handler = new GetSalesOrderHeaderQueryHandler(_logger.Object, _mapper.Object, _salesRepository.Object, _validator);
        }

        [Fact]
        public async Task Should_Call_GetSalesOrderHeaderAsync_In_Repository()
        {
            // Arrange
            GetSalesOrderHeaderQuery query = new GetSalesOrderHeaderQuery();
            CancellationToken cancellationToken = new CancellationToken();

            _salesRepository
                .Setup(x => x.GetSalesOrderHeaderAsync(It.IsAny<SalesOrderHeaderRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PagedResponse<List<SalesOrderHeaderEntity>>());

            // Act
            QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>> actual = await _handler.Handle(query, cancellationToken);

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                _salesRepository
                    .Verify(x => x.GetSalesOrderHeaderAsync(It.IsAny<SalesOrderHeaderRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }
    }
}
