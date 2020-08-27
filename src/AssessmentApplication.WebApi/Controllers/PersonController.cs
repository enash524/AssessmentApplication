using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Person;
using AssessmentApplication.Domain.Entities;
using AssessmentApplication.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.WebApi.Controllers
{
    public class PersonController : BaseController
    {
        // GET api/Person/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PersonVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonVm>> GetPersonById(int id)
        {
            Logger.LogInformation($"GetPersonById: {id}");
            GetPersonByIdQuery query = new GetPersonByIdQuery
            {
                BusinessEntityId = id
            };
            QueryResult<PersonEntity> entity = await Mediator.Send(query);

            if (entity.QueryResultType == QueryResultType.Invalid)
            {
                return BadRequest();
            }

            if (entity.Result == null || entity.QueryResultType == QueryResultType.NotFound)
            {
                return NotFound();
            }

            PersonVm vm = Mapper.Map<PersonVm>(entity.Result);

            return Ok(vm);
        }
    }
}
