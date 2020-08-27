using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Person;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AssessmentApplication.Application.Tests.Queries.Person
{
    public class GetPersonByIdQueryHandlerTests
    {
        private readonly GetPersonByIdQueryHandler _handler;
        private readonly Mock<ILogger<GetPersonByIdQueryHandler>> _logger;
        private readonly Mock<IPersonRepository> _repository;
        private readonly GetPersonByIdQueryValidator _validator;

        public GetPersonByIdQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetPersonByIdQueryHandler>>();
            _repository = new Mock<IPersonRepository>();
            _validator = new GetPersonByIdQueryValidator();
            _handler = new GetPersonByIdQueryHandler(
                _logger.Object,
                _repository.Object,
                _validator);
        }

        [Fact]
        public async Task Request_With_Invalid_BusinessEntityId_Should_Return_Invalid_Result()
        {
            // Arrange

            // Act
            QueryResult<PersonEntity> actual = await _handler.Handle(new GetPersonByIdQuery(), new CancellationToken());

            actual
                .Should()
                .NotBeNull();

            actual
                .QueryResultType
                .Should()
                .Be(QueryResultType.Invalid);
        }

        [Fact]
        public async Task Should_Call_GetPersonByIdAsync_In_Repository()
        {
            // Arrange
            QueryResult<PersonEntity> expected = new QueryResult<PersonEntity>
            {
                Result = new PersonEntity(),
                QueryResultType = QueryResultType.Success
            };

            _repository
                .Setup(x => x.GetPersonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PersonEntity());

            // Act
            QueryResult<PersonEntity> actual = await _handler.Handle(new GetPersonByIdQuery() { BusinessEntityId = 1 }, new CancellationToken());

            // Assert
            actual
                .Should()
                .BeEquivalentTo(expected);

            _repository
                .Verify(x => x.GetPersonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Return_NotFound_If_No_Entity_Exists()
        {
            // Arrange
            _repository
                .Setup(x => x.GetPersonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PersonEntity)null);

            // Act
            QueryResult<PersonEntity> actual = await _handler.Handle(new GetPersonByIdQuery() { BusinessEntityId = 1 }, new CancellationToken());

            // Assert
            actual
                .Should()
                .NotBeNull();

            actual
                .QueryResultType
                .Should()
                .Be(QueryResultType.NotFound);
        }
    }
}
