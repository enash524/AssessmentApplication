using System;
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
        public async Task Request_With_Due_Date_End_After_Due_Date_Start_Should_Return_Invalid_Result()
        {
            // Arrange
            string expectedMessageEnd = "Due Date End Must Occur On or After Due Date Start";
            string expectedMessageStart = "Due Date Start Must Occur On or Before Due Date End";
            string expectedMessage = $"{expectedMessageEnd}{Environment.NewLine}{expectedMessageStart}";
            GetSalesOrderHeaderQuery query = new GetSalesOrderHeaderQuery
            {
                DueDateEnd = DateTime.Now,
                DueDateStart = DateTime.Now.AddDays(1)
            };
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
        public async Task Request_With_Order_Date_End_After_Order_Date_Start_Should_Return_Invalid_Result()
        {
            // Arrange
            string expectedMessageEnd = "Order Date End Must Occur On or After Order Date Start";
            string expectedMessageStart = "Order Date Start Must Occur On or Before Order Date End";
            string expectedMessage = $"{expectedMessageEnd}{Environment.NewLine}{expectedMessageStart}";
            GetSalesOrderHeaderQuery query = new GetSalesOrderHeaderQuery
            {
                OrderDateEnd = DateTime.Now,
                OrderDateStart = DateTime.Now.AddDays(1)
            };
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
        public async Task Request_With_Ship_Date_End_After_Ship_Date_Start_Should_Return_Invalid_Result()
        {
            // Arrange
            string expectedMessageEnd = "Ship Date End Must Occur On or After Ship Date Start";
            string expectedMessageStart = "Ship Date Start Must Occur On or Before Ship Date End";
            string expectedMessage = $"{expectedMessageEnd}{Environment.NewLine}{expectedMessageStart}";
            GetSalesOrderHeaderQuery query = new GetSalesOrderHeaderQuery
            {
                ShipDateEnd = DateTime.Now,
                ShipDateStart = DateTime.Now.AddDays(1)
            };
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
