using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Data.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly ILogger<BaseRepository> Logger;
        protected readonly IMapper Mapper;
        private readonly IDbConnection DbConnection;

        protected BaseRepository(IDbConnection dbConnection, ILogger<BaseRepository> logger, IMapper mapper)
        {
            DbConnection = dbConnection;
            Logger = logger;
            Mapper = mapper;
        }

        public async Task ExecuteAsync(string query, object parameters = null, CommandType? commandType = null)
        {
            try
            {
                using (DbConnection)
                {
                    await DbConnection.ExecuteAsync(query, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                // TODO - Handle the exception
                Logger.LogError("ExecuteAsync", ex);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null, CommandType? commandType = null)
        {
            try
            {
                using (DbConnection)
                {
                    return await DbConnection.QueryAsync<T>(query, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                // TODO - Handle the exception
                Logger.LogError("QueryAsync", ex);
                return Enumerable.Empty<T>();
            }
        }

        public async Task<T> QueryFirstAsync<T>(string query, object parameters = null, CommandType? commandType = null)
        {
            try
            {
                using (DbConnection)
                {
                    return await DbConnection.QueryFirstAsync<T>(query, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                // TODO - Handle the exception
                Logger.LogError("QueryFirstAsync", ex);
                return default; // Or however you want to handle the return
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null, CommandType? commandType = null)
        {
            try
            {
                using (DbConnection)
                {
                    return await DbConnection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                // TODO - Handle the exception
                Logger.LogError("QueryFirstOrDefaultAsync", ex);
                return default; // Or however you want to handle the return
            }
        }

        public async Task<T> QuerySingleAsync<T>(string query, object parameters = null, CommandType? commandType = null)
        {
            try
            {
                using (DbConnection)
                {
                    return await DbConnection.QuerySingleAsync<T>(query, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                // TODO - Handle the exception
                Logger.LogError("QuerySingleAsync", ex);
                return default; // Or however you want to handle the return
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null, CommandType? commandType = null)
        {
            try
            {
                using (DbConnection)
                {
                    return await DbConnection.QuerySingleOrDefaultAsync<T>(query, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                // TODO - Handle the exception
                Logger.LogError("QuerySingleOrDefaultAsync", ex);
                return default; // Or however you want to handle the return
            }
        }
    }
}
