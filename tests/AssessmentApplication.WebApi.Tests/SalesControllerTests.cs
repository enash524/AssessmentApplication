using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Sales.SalesOrderDetail;
using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AssessmentApplication.WebApi.Controllers;
using AssessmentApplication.WebApi.Models;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
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
        private readonly Mock<ILogger<BaseController>> _logger = new Mock<ILogger<BaseController>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private readonly SalesController _salesController;

        public SalesControllerTests()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCollection services = new ServiceCollection();

            services.AddScoped(x => _logger.Object);
            services.AddScoped(x => _mapper.Object);
            services.AddScoped(x => _mediator.Object);

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
        public async Task GetSalesOrderDetail_ShouldReturnOkResult()
        {
            // Arrange
            _mapper
                .Setup(x => x.Map<SalesOrderDetailVm>(It.IsAny<SalesOrderDetailEntity>()))
                .Returns(new SalesOrderDetailVm());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<SalesOrderDetailEntity>
                {
                    Result = new SalesOrderDetailEntity()
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
            }

            _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetSalesOrderDetail_ShouldReturnBadRequest_WhenInvalidInput()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<SalesOrderDetailEntity>
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
            }

            _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetSalesOrderDetail_ShouldReturnNotFoundRequest_WhenQueryResultNotFound()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<SalesOrderDetailEntity>
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
                    .BeOfType<NotFoundResult>();
            }

            _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetSalesOrderHeader_ShouldReturnOkResult()
        {
            // Arrange
            _mapper
                .Setup(x => x.Map<PagedResponse<List<SalesOrderHeaderVm>>>(It.IsAny<PagedResponse<List<SalesOrderHeaderEntity>>>()))
                .Returns(new PagedResponse<List<SalesOrderHeaderVm>>());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PagedResponse<List<SalesOrderHeaderEntity>>
                {
                    Data = new List<SalesOrderHeaderEntity>
                    {
                        new SalesOrderHeaderEntity()
                    }
                });

            // Act
            ActionResult<PagedResponse<List<SalesOrderHeaderVm>>> response = await _salesController.GetSalesOrderHeader();

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
        public async Task GetSalesOrderHeader_ShouldReturnNotFoundResult_WhenQueryResultNotFound()
        {
            // Arrange
            _mapper
                .Setup(x => x.Map<PagedResponse<List<SalesOrderHeaderVm>>>(It.IsAny<PagedResponse<List<SalesOrderHeaderEntity>>>()))
                .Returns(new PagedResponse<List<SalesOrderHeaderVm>>());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PagedResponse<List<SalesOrderHeaderEntity>>());

            // Act
            ActionResult<PagedResponse<List<SalesOrderHeaderVm>>> response = await _salesController.GetSalesOrderHeader();

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
                    .BeOfType<NotFoundResult>();
            }

            _mediator.Verify(x => x.Send(It.IsAny<GetSalesOrderHeaderQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
