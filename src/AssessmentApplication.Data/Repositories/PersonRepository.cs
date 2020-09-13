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
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(IDbConnection dbConnection, ILogger<PersonRepository> logger, IMapper mapper)
            : base(dbConnection, logger, mapper)
        { }

        public async Task<PersonEntity> GetPersonByIdAsync(int businessEntityId, CancellationToken cancellationToken)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@businessEntityId", businessEntityId);

            PersonDto dto = await QuerySingleOrDefaultAsync<PersonDto>("SELECT * FROM Person.Person WHERE BusinessEntityID = @businessEntityId", parms, CommandType.Text);
            PersonEntity entity = Mapper.Map<PersonEntity>(dto);

            return entity;
        }
    }
}
