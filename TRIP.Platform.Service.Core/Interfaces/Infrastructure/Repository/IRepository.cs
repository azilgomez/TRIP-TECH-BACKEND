using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id, CancellationToken cancelToken);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancelToken);
        Task<TEntity> FindAsync(object id, CancellationToken cancellationToken);
        public Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancelToken);
        public Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancelToken);
        Task Add(TEntity entity, CancellationToken cancelToken);
        void AddRange(IEnumerable<TEntity> entities, CancellationToken cancelToken);
        void Remove(TEntity entity, CancellationToken cancelToken);
        void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancelToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> ExecuteQueryForEntity(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken);
        Task<TEntity> ExecuteQueryForEntity(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false);
        Task<TEntity> ExecuteQueryForEntity<Trequest>(string schema, string storedProcedureName, Trequest data, CancellationToken cancellationToken) where Trequest : class;
        Task<TType> ExecuteQueryForOtherEntity<TType>(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false) where TType : class;
        Task<TType> ExecuteQueryForOtherEntity<TType>(string schema, string storedProcedureName, TType data, CancellationToken cancellationToken) where TType : class;


        Task<IEnumerable<TEntity>> ExecuteQueryForEntities(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ExecuteQueryForEntities(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false);
        Task<IEnumerable<TEntity>> ExecuteQueryForEntities<TRequest>(string schema, string storedProcedureName, TRequest data, CancellationToken cancellationToken) where TRequest : class;
        Task<IEnumerable<TEntity>> ExecuteQueryForEntities(string schema, string storedProcedureName, CancellationToken cancellationToken);
        Task<IEnumerable<TType>> ExecuteQueryForOtherEntities<TType>(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken, bool noTracking = false) where TType : class;
        Task<IEnumerable<TType>> ExecuteQueryForOtherEntities<TType>(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false) where TType : class;
        Task<IEnumerable<TType>> ExecuteQueryForOtherEntities<TType>(string schema, string storedProcedureName, CancellationToken cancellationToken, bool noTracking = false) where TType : class;

        Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken);
        Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, TimeSpan? customTimeout = null);
        Task<bool> ExecuteNonQuery<Trequest>(string schema, string storedProcedureName, Trequest data, CancellationToken cancellationToken) where Trequest : class;

        //Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken);
        //Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, TimeSpan? customTimeout = null);
        Task<bool> TransExecuteNonQuery(string schema, string storedProcedureName, string strParam, List<SqlParameter> sqlParams, CancellationToken cancellationToken, TimeSpan? customTimeout = null);
    }
}
