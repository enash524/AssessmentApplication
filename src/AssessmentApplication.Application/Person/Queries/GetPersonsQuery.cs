using System.Collections.Generic;
using MediatR;

namespace AssessmentApplication.Application.Person.Queries
{
    public class GetPersonsQuery : IRequest<List<PersonVm>>
    {
    }
}
