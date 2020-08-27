using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Domain.Entities;

namespace AssessmentApplication.Data.Interfaces
{
    public interface IPersonRepository
    {
        Task<PersonEntity> GetPersonByIdAsync(int businessEntityId, CancellationToken cancellationToken);
    }
}
