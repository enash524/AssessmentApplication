using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Sales.SalesOrderDetail;
using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AssessmentApplication.Models.SalesOrder;
using AssessmentApplication.WebApi.Controllers;
using AssessmentApplication.WebApi.Models;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AssessmentApplication.WebApi.Tests
{
    public class SalesControllerTests
    {
        private readonly Mock<ILogger<BaseController>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IMediator> _mediator;
        private readonly SalesController _salesController;
        private readonly IValidator<GetSalesOrderHeaderQuery> _validator;

        public SalesControllerTests()
        {
            _logger = new Mock<ILogger<BaseController>>();
            _mapper = new Mock<IMapper>();
            _mediator = new Mock<IMediator>();
            _validator = new GetSalesOrderHeaderQueryValidator();

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCollection services = new ServiceCollection();

            services.AddScoped(x => _logger.Object);
            services.AddScoped(x => _mapper.Object);
            services.AddScoped(x => _mediator.Object);
            services.AddScoped(x => _validator);

            httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext
                {
                    RequestServices = services.BuildServiceProvider()
                });

            _salesController = new SalesController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessorMock.Object.HttpContext
                }
            };
        }

        [Fact]
        public async Task GetSalesOrderDetail_ShouldReturnBadRequest_WhenInvalidInput()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<List<SalesOrderDetailEntity>>
                {
                    QueryResultType = QueryResultType.Invalid
                });

            // Act
            ActionResult<SalesOrderDetailVm> response = await _salesController.GetSalesOrderDetail(-1);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<BadRequestResult>();

                _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetSalesOrderDetail_ShouldReturnOkRequest_WhenQueryResultNotFound()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<List<SalesOrderDetailEntity>>
                {
                    QueryResultType = QueryResultType.NotFound
                });

            // Act
            ActionResult<SalesOrderDetailVm> response = await _salesController.GetSalesOrderDetail(1);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<OkObjectResult>();

                _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetSalesOrderDetail_ShouldReturnOkResult()
        {
            // Arrange
            _mapper
                .Setup(x => x.Map<SalesOrderDetailVm>(It.IsAny<SalesOrderDetailEntity>()))
                .Returns(new SalesOrderDetailVm());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<List<SalesOrderDetailEntity>>
                {
                    Result = new List<SalesOrderDetailEntity>()
                });

            // Act
            ActionResult<SalesOrderDetailVm> response = await _salesController.GetSalesOrderDetail(1);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<OkObjectResult>();

                _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetSalesOrderHeader_Should_Return_BadRequestResult_When_Query_Is_Invalid()
        {
            // Arrange
            SalesOrderSearchModel model = new SalesOrderSearchModel();

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>
                {
                    QueryResultType = QueryResultType.Invalid,
                    Result = new PagedResponse<List<SalesOrderHeaderEntity>>
                    {
                        Data = null
                    }
                });

            // Act
            ActionResult<PagedResponse<List<SalesOrderHeaderVm>>> response = await _salesController.GetSalesOrderHeader(model);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<BadRequestResult>();

                _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetSalesOrderHeader_ShouldReturnOkResult()
        {
            // Arrange
            SalesOrderSearchModel model = new SalesOrderSearchModel();

            _mapper
                .Setup(x => x.Map<PagedResponse<List<SalesOrderHeaderVm>>>(It.IsAny<PagedResponse<List<SalesOrderHeaderEntity>>>()))
                .Returns(new PagedResponse<List<SalesOrderHeaderVm>>());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>
                {
                    QueryResultType = QueryResultType.Success,
                    Result = new PagedResponse<List<SalesOrderHeaderEntity>>
                    {
                        Data = new List<SalesOrderHeaderEntity>
                        {
                            new SalesOrderHeaderEntity()
                        }
                    }
                });

            // Act
            ActionResult<PagedResponse<List<SalesOrderHeaderVm>>> response = await _salesController.GetSalesOrderHeader(model);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<OkObjectResult>();
            }
        }

        [Fact]
        public async Task GetSalesOrderHeader_ShouldReturnOkResult_WhenQueryResultNotFound()
        {
            // Arrange
            SalesOrderSearchModel model = new SalesOrderSearchModel();

            _mapper
                .Setup(x => x.Map<PagedResponse<List<SalesOrderHeaderVm>>>(It.IsAny<PagedResponse<List<SalesOrderHeaderEntity>>>()))
                .Returns(new PagedResponse<List<SalesOrderHeaderVm>>());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>());

            // Act
            ActionResult<PagedResponse<List<SalesOrderHeaderVm>>> response = await _salesController.GetSalesOrderHeader(model);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<OkObjectResult>();

                _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }
    }
}
