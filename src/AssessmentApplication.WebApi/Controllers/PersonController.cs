using System.Threading.Tasks;
using AssessmentApplication.Application.Person.Queries;
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
        public async Task<ActionResult<PersonVm>> GetPersonById(int id)
        {
            Logger.LogInformation($"GetPersonById: {id}");
            GetPersonByIdQuery query = new GetPersonByIdQuery
            {
                BusinessEntityId = id
            };
            PersonEntity entity = await Mediator.Send(query);
            PersonVm vm = Mapper.Map<PersonVm>(entity);

            return Ok(vm);
        }
    }
}