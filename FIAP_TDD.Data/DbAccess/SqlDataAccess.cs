using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;


namespace FIAP_TDD.Data.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "Default")
        {
            using IDbConnection connection = GetConnection(connectionId);
            return await connection.QueryAsync<T>(storedProcedure,
                                                  parameters,
                                                  commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storedProcedure,
                                      T parameters,
                                      string connectionId = "Default")
        {
            using IDbConnection connection = GetConnection(connectionId);
            await connection.ExecuteAsync(storedProcedure,
                                          parameters,
                                          commandType: CommandType.StoredProcedure);
        }


        private IDbConnection GetConnection(string connectionId)
        {
            return new SqlConnection(_config.GetConnectionString(connectionId));
        }
    }
}
