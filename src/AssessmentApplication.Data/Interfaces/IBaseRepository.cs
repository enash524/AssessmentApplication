using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AssessmentApplication.Data.Interfaces
{
    public interface IBaseRepository
    {
        Task ExecuteAsync(string query, object parameters = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null, CommandType? commandType = null);

        Task<T> QueryFirstAsync<T>(string query, object parameters = null, CommandType? commandType = null);

        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null, CommandType? commandType = null);

        Task<T> QuerySingleAsync<T>(string query, object parameters = null, CommandType? commandType = null);

        Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null, CommandType? commandType = null);
    }
}
