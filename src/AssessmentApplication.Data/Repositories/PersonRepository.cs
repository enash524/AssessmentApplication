using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Dto;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using Dapper;

namespace AssessmentApplication.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public PersonRepository(IMapper mapper, IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
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