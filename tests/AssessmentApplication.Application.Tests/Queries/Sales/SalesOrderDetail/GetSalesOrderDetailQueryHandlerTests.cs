using System;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Sales.SalesOrderDetail;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AssessmentApplication.Application.Tests.Queries.Sales.SalesOrderDetail
{
    public class GetSalesOrderDetailQueryHandlerTests
    {
        private readonly GetSalesOrderDetailQueryHandler _handler;
        private readonly Mock<ILogger<GetSalesOrderDetailQueryHandler>> _logger;
        private readonly Mock<ISalesRepository> _salesRepository;
        private readonly IValidator<GetSalesOrderDetailQuery> _validator;

        public GetSalesOrderDetailQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetSalesOrderDetailQueryHandler>>();
            _salesRepository = new Mock<ISalesRepository>();
            _validator = new GetSalesOrderDetailQueryValidator();
            _handler = new GetSalesOrderDetailQueryHandler(_logger.Object, _salesRepository.Object, _validator);
        }

        [Fact]
        public async Task Request_With_Invalid_Id_Should_Return_Invalid_Result()
        {
            // Arrange
            int id = 0;
            string errors = "'Sales Order Detail Id' must be greater than '0'.";
            string expectedMessage = $"GetSalesOrderDetailQuery with SalesOrderDetailId: {id} produced errors on validation {errors}";
            GetSalesOrderDetailQuery query = new GetSalesOrderDetailQuery();
            CancellationToken cancellationToken = new CancellationToken();

            // Act
            QueryResult<SalesOrderDetailEntity> actual = await _handler.Handle(query, cancellationToken);

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                actual
                    .QueryResultType
                    .Should()
                    .Be(QueryResultType.Invalid);

                _logger.Invocations.Count
                    .Should()
                    .Be(1);

                _logger.Invocations[0].Arguments[0]
                    .Should()
                    .Be(LogLevel.Error);

                _logger
                    .Verify(x => x.Log(LogLevel.Error,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((x, t) => string.Equals(x.ToString(), expectedMessage)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                        Times.Once());
            }
        }

        [Fact]
        public async Task Should_Call_GetSalesOrderDetailAsync_In_Repository()
        {
            // Arrange
            int id = 1;
            CancellationToken cancellationToken = new CancellationToken();
            GetSalesOrderDetailQuery query = new GetSalesOrderDetailQuery
            {
                SalesOrderDetailId = id
            };
            _salesRepository
                .Setup(x => x.GetSalesOrderDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SalesOrderDetailEntity());

            // Act
            QueryResult<SalesOrderDetailEntity> actual = await _handler.Handle(query, cancellationToken);

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                actual
                    .QueryResultType
                    .Should()
                    .Be(QueryResultType.Success);

                _salesRepository
                    .Verify(x => x.GetSalesOrderDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task Should_Return_NotFound_If_No_Entity_Exists()
        {
            // Arrange
            int id = 1;
            string expectedMessage = $"GetSalesOrderDetailQuery with SalesOrderDetailId: {id} was not found.";
            CancellationToken cancellationToken = new CancellationToken();
            GetSalesOrderDetailQuery query = new GetSalesOrderDetailQuery
            {
                SalesOrderDetailId = id
            };

            _salesRepository
                .Setup(x => x.GetSalesOrderDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SalesOrderDetailEntity)null);

            // Act
            QueryResult<SalesOrderDetailEntity> actual = await _handler.Handle(query, cancellationToken);

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                actual
                    .QueryResultType
                    .Should()
                    .Be(QueryResultType.NotFound);

                _logger.Invocations.Count
                    .Should()
                    .Be(1);

                _logger.Invocations[0].Arguments[0]
                    .Should()
                    .Be(LogLevel.Error);

                _logger
                    .Verify(x => x.Log(LogLevel.Error,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((x, t) => string.Equals(x.ToString(), expectedMessage)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                        Times.Once());
            }
        }
    }
}
