using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Mhd.Framework.Efcore
{
    public static class Extension
    {
        public static List<T> SqlReader<T>(this DbContext context, string query, CommandType commandType, Func<DbDataReader, T> map) where T : class, new()
        {
            List<T> entities;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    entities = new List<T>();

                    while (result.Read())
                        entities.Add(map(result));
                }
                context.Database.CloseConnection();
            }

            return entities;
        }

        public static async Task<List<T>> SqlReaderAsync<T>(this DbContext context, string query, CommandType commandType, Func<DbDataReader, T> map, CancellationToken cancellationToken = default) where T : class, new()
        {
            List<T> entities;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                context.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync(cancellationToken))
                {
                    entities = new List<T>();

                    while (result.Read())
                        entities.Add(map(result));
                }
                context.Database.CloseConnection();
            }

            return entities;
        }

        public static object SqlScalar(this DbContext context, string query, CommandType commandType)
        {
            object obj;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                context.Database.OpenConnection();
                obj = command.ExecuteScalar();
                context.Database.CloseConnection();
            }

            return obj;
        }

        public static async Task<object> SqlScalarAsync(this DbContext context, string query, CommandType commandType, CancellationToken cancellationToken = default)
        {
            object obj;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                context.Database.OpenConnection();
                obj = await command.ExecuteScalarAsync(cancellationToken);
                context.Database.CloseConnection();
            }

            return obj;
        }

        public static int SqlNonQuery(this DbContext context, string query, CommandType commandType)
        {
            int obj;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                context.Database.OpenConnection();
                obj = command.ExecuteNonQuery();
                context.Database.CloseConnection();
            }

            return obj;
        }

        public static async Task<int> SqlNonQueryAsync(this DbContext context, string query, CommandType commandType, CancellationToken cancellationToken = default)
        {
            int obj;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                context.Database.OpenConnection();
                obj = await command.ExecuteNonQueryAsync(cancellationToken);
                context.Database.CloseConnection();
            }

            return obj;
        }
        public static int SqlNonQueryWithParameters(this DbContext context, string query, CommandType commandType, SqlParameter[] sqlParameters)
        {
            int obj;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(sqlParameters);
                context.Database.OpenConnection();
                obj = command.ExecuteNonQuery();
                context.Database.CloseConnection();
            }

            return obj;
        }

        public static async Task<int> SqlNonQueryWithParametersAsync(this DbContext context, string query, CommandType commandType, SqlParameter[] sqlParameters, CancellationToken cancellationToken = default)
        {
            int obj;

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(sqlParameters);
                context.Database.OpenConnection();
                obj = await command.ExecuteNonQueryAsync(cancellationToken);
                context.Database.CloseConnection();
            }

            return obj;
        }
    }
}
