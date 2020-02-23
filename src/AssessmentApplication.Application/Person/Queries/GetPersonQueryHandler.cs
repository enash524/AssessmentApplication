using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AssessmentApplication.Application.Person.Queries
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonsQuery, List<PersonVm>>
    {
        private readonly IConfiguration _configuration;

        public GetPersonQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<PersonVm>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                IEnumerable<PersonVm> result = await connection.QueryAsync<PersonVm>("[Sales].[uspTest]");
                return result?.ToList();
            }
        }
    }
}
