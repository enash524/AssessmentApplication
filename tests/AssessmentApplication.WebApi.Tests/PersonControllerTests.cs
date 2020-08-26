using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Person;
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
    public class PersonControllerTests
    {
        private readonly Mock<ILogger<BaseController>> _logger = new Mock<ILogger<BaseController>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private readonly PersonController _personController;

        public PersonControllerTests()
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

            _personController = new PersonController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessorMock.Object.HttpContext
                }
            };
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnOkResult()
        {
            // Arrange
            _mapper
                .Setup(x => x.Map<PersonVm>(It.IsAny<PersonEntity>()))
                .Returns(new PersonVm());

            _mediator
                .Setup(x => x.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<PersonEntity>
                {
                    Result = new PersonEntity()
                });

            // Act
            ActionResult<PersonVm> response = await _personController.GetPersonById(1);

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

            _mediator.Verify(x => x.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnBadRequestResult_WhenInvalidInput()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<PersonEntity>
                {
                    QueryResultType = QueryResultType.Invalid
                });

            // Act
            ActionResult<PersonVm> response = await _personController.GetPersonById(-1);

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

            _mediator.Verify(x => x.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnNotFoundResult_WhenQueryResultNotFound()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<PersonEntity>
                {
                    QueryResultType = QueryResultType.NotFound
                });

            // Act
            ActionResult<PersonVm> response = await _personController.GetPersonById(1);

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

            _mediator.Verify(x => x.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}