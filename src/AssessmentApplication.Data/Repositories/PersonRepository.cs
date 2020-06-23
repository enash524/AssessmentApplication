using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Dto;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<PersonRepository> _logger;
        private readonly IMapper _mapper;

        public PersonRepository(IDbConnection dbConnection, ILogger<PersonRepository> logger, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PersonEntity> GetPersonByIdAsync(int businessEntityId, CancellationToken cancellationToken)
        {
            PersonDto dto;
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@businessEntityId", businessEntityId);

            using (_dbConnection)
            {
                dto = await _dbConnection.QueryFirstOrDefaultAsync<PersonDto>("SELECT * FROM Person.Person WHERE BusinessEntityID = @businessEntityId", parms, commandType: CommandType.Text);
            }

            PersonEntity entity = _mapper.Map<PersonEntity>(dto);

            return entity;
        }
    }
}