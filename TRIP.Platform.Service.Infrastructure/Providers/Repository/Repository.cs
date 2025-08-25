using EY.CTP.SRED.Platform.Service.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository;
using TRIP.Platform.Service.Infrastructure.Extensions;

namespace TRIP.Platform.Service.Infrastructure.Providers.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext dbContext) => this._context = dbContext;
        public async Task Add(TEntity entity, CancellationToken cancelToken) =>
             await this._context.AddAsync(entity, cancelToken); // if this does not works add this :- await Context.Set<TEntityModel>().AddAsync(entity);


        public async void AddRange(IEnumerable<TEntity> entities, CancellationToken cancelToken) =>
            await this._context.AddRangeAsync(entities, cancelToken);

        public async Task<TEntity> FindAsync(object id, CancellationToken cancellationToken) =>
             await this._context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancelToken) =>
             await this._context.Set<TEntity>().Where(predicate).ToListAsync(cancelToken);
        public async Task<TEntity> Get(int id, CancellationToken cancelToken) =>
            await this._context.Set<TEntity>().FindAsync(id, cancelToken);
        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancelToken) =>
            await this._context.Set<TEntity>().ToListAsync(cancelToken);
        public void Remove(TEntity entity, CancellationToken cancelToken) =>
            this._context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancelToken) =>
            this._context.Set<TEntity>().RemoveRange(entities);
        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancelToken) =>
           await this._context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken) =>
           this._context.Set<TEntity>().Update(entity);


        public async Task<SqlParameter> BuildTableParameter(StoredProcParam param, System.Collections.IEnumerable enumerableValues)
        {
            var table = new DataTable();
            List<string> columnNames = new List<string>();
            //Get the Custom Table Columns
            IEnumerable<CustomTableTypeColumn> customTableTypeColumns = await this.GetCustomTableTypeColumn(param.USER_DEFINED_TYPE_SCHEMA, param.USER_DEFINED_TYPE_NAME);

            //Create the Data Table
            foreach (CustomTableTypeColumn customTableTypeColumn in customTableTypeColumns)
            {
                table.Columns.Add(customTableTypeColumn.COLUMN_NAME, customTableTypeColumn.ToType());
                columnNames.Add(customTableTypeColumn.COLUMN_NAME); //Create a collection of columns to populate
            }

            //Create a dictionary of column names
            Dictionary<string, PropertyInfo> propList = null;
            Dictionary<string, object> propDefaultValue = null;

            //Enumerate thru the collection and add it to the table
            foreach (var enumerableValue in enumerableValues)
            {
                var newrow = table.NewRow();

                if (propList == null)
                {
                    propList = new Dictionary<string, PropertyInfo>();
                    propDefaultValue = new Dictionary<string, object>();
                    //Initiliaze property list
                    var propInfos = enumerableValue.GetType().GetProperties();
                    foreach (var columnName in columnNames)
                    {
                        var propInfo = propInfos.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                        if (propInfo == null)
                        {
                            foreach (var prop in propInfos)
                            {
                                var customAttr = prop.CustomAttributes.FirstOrDefault(i => i.AttributeType.Equals(typeof(ColumnAttribute)) && i.ConstructorArguments[0].Value.ToString().Equals(columnName, StringComparison.OrdinalIgnoreCase));
                                if (customAttr != null)
                                {
                                    propInfo = prop;
                                    break;
                                }
                            }
                        }
                        if (propInfo == null)
                        {
                            throw new NotImplementedException($"Column {columnName} was not found on object {enumerableValue.GetType().FullName}");
                        }
                        //Add to Property List
                        propList.Add(columnName, propInfo);
                        //Find If there is a default value
                        var defaultAttr = propInfo.CustomAttributes.FirstOrDefault(i => i.AttributeType.Equals(typeof(DefaultValueAttribute)));
                        if (defaultAttr != null)
                        {
                            propDefaultValue.Add(columnName, defaultAttr.ConstructorArguments[0].Value);
                        }
                        else
                        {
                            propDefaultValue.Add(columnName, propInfo.ToDefaultValue(enumerableValue));
                        }
                    }
                }

                foreach (var columnName in columnNames)
                {
                    var value = propList[columnName].GetValue(enumerableValue);
                    newrow[columnName] = value ?? propDefaultValue[columnName];
                }
                table.Rows.Add(newrow);
            }

            return await Task.FromResult(new SqlParameter
            {
                ParameterName = param.PARAMETER_NAME,
                Value = table,
                TypeName = $"{param.USER_DEFINED_TYPE_SCHEMA}.{param.USER_DEFINED_TYPE_NAME}",
                SqlDbType = SqlDbType.Structured,
                Direction = param.ToParameterDirection()
            });
        }

        #region Helper Methods

        private async Task<(string, IEnumerable<SqlParameter>)> BuildParams<TType>(string schema, string storedProcedureName, TType data) where TType : class
        {
            var listRequest = new List<KeyValuePair<string, object>>();
            foreach (var pi in data.GetType().GetProperties())
            {
                var propName = pi.Name;
                var ca = pi.CustomAttributes.FirstOrDefault(i => i.AttributeType.Equals(typeof(ColumnAttribute)));
                if (ca != null)
                {
                    propName = ca.ConstructorArguments[0].Value.ToString();
                }

                listRequest.Add(new KeyValuePair<string, object>
                        (propName, pi.GetValue(data, null)));
            }

            var sbParameters = new StringBuilder();
            var paramValues = new List<SqlParameter>();
            var spParameterDatas = await this.GetStoredProcedureParameters(schema, storedProcedureName);

            foreach (var spParameterData in spParameterDatas)
            {
                var paramValue = listRequest.FirstOrDefault(i => $"@{i.Key}".Equals(spParameterData.PARAMETER_NAME, StringComparison.OrdinalIgnoreCase));
                if (spParameterData.DATA_TYPE.ToUpper().Equals("TABLE TYPE"))
                {
                    if (paramValue.Value is System.Collections.IEnumerable enumerableValue)
                    {
                        var tableValuedParameter = this.BuildTableParameter(spParameterData, enumerableValue);
                        paramValues.Add(new SqlParameter
                        {
                            ParameterName = spParameterData.PARAMETER_NAME,
                            Value = await tableValuedParameter,
                            SqlDbType = SqlDbType.Structured,
                            Direction = spParameterData.ToParameterDirection()
                        });
                    }
                    else
                    {
                        paramValues.Add(new SqlParameter
                        {
                            ParameterName = spParameterData.PARAMETER_NAME,
                            Value = DBNull.Value,
                            SqlDbType = SqlDbType.Structured,
                            Direction = spParameterData.ToParameterDirection()
                        });
                    }
                }
                else
                {
                    paramValues.Add(new SqlParameter
                    {
                        ParameterName = spParameterData.PARAMETER_NAME,
                        Value = paramValue.Value ?? DBNull.Value,
                        SqlDbType = spParameterData.ToSqlDbType(),
                        Direction = spParameterData.ToParameterDirection()
                    });
                }




                sbParameters.Append($"{spParameterData.PARAMETER_NAME} " +
                    $"{((spParameterData.ToParameterDirection() == ParameterDirection.InputOutput || spParameterData.ToParameterDirection() == ParameterDirection.Output) ? "OUT" : "")},");
            }
            var strParam = sbParameters.ToString().Substring(0, sbParameters.Length - 1);
            return (strParam, paramValues);
        }
        private List<SqlParameter> AddReturnParameter(List<SqlParameter> sqlParams)
        {
            if (sqlParams.FindIndex(p => p.ParameterName.ToLower().Equals("@returnval")) < 0)   //check if returnval is not yet added 
                sqlParams.Add(new SqlParameter("@returnval", SqlDbType.Int) { Direction = ParameterDirection.Output });
            return sqlParams;
        }
        private async Task<IEnumerable<CustomTableTypeColumn>> GetCustomTableTypeColumn(string schema, string customTableType)
        {
            try
            {
                var paramSchema = new SqlParameter("schema", schema);
                var paramCustomTableType = new SqlParameter("customTableType", customTableType);
                var sql = $"SELECT C.NAME AS COLUMN_NAME, T.NAME AS [TYPE_NAME], C.IS_NULLABLE AS [NULLABLE] " +
                        $"FROM SYS.COLUMNS C " +
                        $"INNER JOIN SYS.TYPES T " +
                        $"ON T.USER_TYPE_ID = C.USER_TYPE_ID " +
                        $"WHERE C.OBJECT_ID = (SELECT T.TYPE_TABLE_OBJECT_ID FROM SYS.TABLE_TYPES T " +
                        $"INNER JOIN SYS.SCHEMAS S ON T.schema_id = S.schema_id " +
                        $"WHERE S.name = @schema and T.name = @customTableType) " +
                        $"ORDER BY C.COLUMN_ID";

                return await this._context.Set<CustomTableTypeColumn>().FromSqlRaw(sql, paramSchema, paramCustomTableType).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<IEnumerable<StoredProcParam>> GetStoredProcedureParameters(string schema, string storedProcedureName)
        {
            try
            {
                var paramSchema = new SqlParameter("schema", schema);
                var paramName = new SqlParameter("spName", storedProcedureName);



                var sql = $"SELECT PARAMETER_MODE, PARAMETER_NAME, DATA_TYPE, USER_DEFINED_TYPE_SCHEMA, USER_DEFINED_TYPE_NAME " +
                          $"FROM INFORMATION_SCHEMA.PARAMETERS " +
                          $"WHERE SPECIFIC_SCHEMA = @schema and specific_name= @spName " +
                          $"ORDER BY ORDINAL_POSITION";



                return await this._context.Set<StoredProcParam>().FromSqlRaw(sql, paramSchema, paramName).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Using procedures

        public async Task<TEntity> ExecuteQueryForEntity(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken)
        {
            var result = await this._context.Set<TEntity>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParam).ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<TEntity> ExecuteQueryForEntity(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false)
        {
            if (noTracking)
            {
                var resultWithNoTracking = await this._context.Set<TEntity>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).AsNoTracking().ToListAsync(cancellationToken);
                return resultWithNoTracking.FirstOrDefault();
            }
            var result = await this._context.Set<TEntity>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<TEntity> ExecuteQueryForEntity<Trequest>(string schema, string storedProcedureName, Trequest data, CancellationToken cancellationToken) where Trequest : class
        {
            var (strParam, sqlParams) = await this.BuildParams(schema, storedProcedureName, data);
            return await this.ExecuteQueryForEntity(schema, storedProcedureName, strParam, sqlParams, cancellationToken);
        }

        public async Task<TType> ExecuteQueryForOtherEntity<TType>(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false) where TType : class
        {
            if (noTracking)
            {
                var resultWithNoTracking = await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).ToListAsync(cancellationToken);
                return resultWithNoTracking.FirstOrDefault();
            }
            var result = await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<TType> ExecuteQueryForOtherEntity<TType>(string schema, string storedProcedureName, string strParam, SqlParameter sqlParams, CancellationToken cancellationToken) where TType : class
        {
            var result = await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams).ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<TType> ExecuteQueryForOtherEntity<TType>(string schema, string storedProcedureName, TType data, CancellationToken cancellationToken) where TType : class
        {
            var (strParam, sqlParams) = await this.BuildParams(schema, storedProcedureName, data);
            return await this.ExecuteQueryForOtherEntity<TType>(schema, storedProcedureName, strParam, sqlParams, cancellationToken);
        }
        #endregion

        #region method for returning list of entity
        public async Task<IEnumerable<TEntity>> ExecuteQueryForEntities(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken)
            => await this._context.Set<TEntity>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParam).ToListAsync(cancellationToken);

        public async Task<IEnumerable<TEntity>> ExecuteQueryForEntities(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false)
        {
            if (noTracking)
            {
                return await this._context.Set<TEntity>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).AsNoTracking().ToListAsync(cancellationToken);
            }
            return await this._context.Set<TEntity>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ExecuteQueryForEntities<TRequest>(string schema, string storedProcedureName, TRequest data, CancellationToken cancellationToken) where TRequest : class
        {
            var (strParam, sqlParams) = await this.BuildParams(schema, storedProcedureName, data);
            return await this.ExecuteQueryForEntities(schema, storedProcedureName, strParam, sqlParams, cancellationToken);
        }
        public async Task<IEnumerable<TEntity>> ExecuteQueryForEntities(string schema, string storedProcedureName, CancellationToken cancellationToken)
            => await this._context.Set<TEntity>().FromSqlRaw($"exec @returnval = {schema}.{storedProcedureName}", new SqlParameter("@returnval", SqlDbType.Int) { Direction = ParameterDirection.Output }).ToListAsync(cancellationToken);

        public async Task<IEnumerable<TType>> ExecuteQueryForOtherEntities<TType>(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken, bool noTracking = false) where TType : class
        {
            if (noTracking)
            {
                return await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParam).AsNoTracking().ToListAsync(cancellationToken);
            }
            return await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParam).ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<TType>> ExecuteQueryForOtherEntities<TType>(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, bool noTracking = false) where TType : class
        {
            if (noTracking)
            {
                return await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).AsNoTracking().ToListAsync(cancellationToken);
            }
            return await this._context.Set<TType>().FromSqlRaw($"exec {schema}.{storedProcedureName} {strParam}", sqlParams.ToArray()).ToListAsync(cancellationToken);
        }

        public async Task<DataSet> ExecuteQueryAsDataset(string schema, string storedProcedureName, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken) 
        {
            DataSet dsResult = new DataSet();
            using (SqlCommand cmd = (SqlCommand)_context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = string.Join('.', schema, storedProcedureName);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParams.ToArray());
                await cmd.Connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dsResult);
                cmd.Dispose();
                cmd.Connection.Close();
            }
            return dsResult;
        }

        public async Task<IEnumerable<TType>> ExecuteQueryForOtherEntities<TType>(string schema, string storedProcedureName, CancellationToken cancellationToken, bool noTracking = false) where TType : class
        {
            if (noTracking)
            {
                return await this._context.Set<TType>().FromSqlRaw($"exec @returnval = {schema}.{storedProcedureName}", new SqlParameter("@returnval", SqlDbType.Int) { Direction = ParameterDirection.Output }).AsNoTracking().ToListAsync(cancellationToken);
            }
            return await this._context.Set<TType>().FromSqlRaw($"exec @returnval = {schema}.{storedProcedureName}", new SqlParameter("@returnval", SqlDbType.Int) { Direction = ParameterDirection.Output }).ToListAsync(cancellationToken);
        }
        #endregion

        #region method for calling Non Query Procedures
        public async Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, string strParam, SqlParameter sqlParam, CancellationToken cancellationToken)
            => await this.ExecuteNonQuery(schema, storedProcedureName, strParam, new List<SqlParameter>() { sqlParam }, cancellationToken);

        public async Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, CancellationToken cancellationToken)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            var updateSQLParams = this.AddReturnParameter(parms);
            return await this.ExecuteNonQuery(schema, storedProcedureName, "", updateSQLParams, cancellationToken);
        }

        public async Task<bool> ExecuteNonQuery(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, TimeSpan? customTimeout = null)
        {
            var updateSQLParams = this.AddReturnParameter(sqlParams.ToList());
            // Incaase you the timeout times from config
            //this._context.Database.SetCommandTimeout( read from config);
            await this._context.Database.ExecuteSqlRawAsync($"exec @returnval = {schema}.{storedProcedureName} {strParam}", updateSQLParams, cancellationToken);
            return updateSQLParams.FirstOrDefault(p => p.ParameterName.ToLower().Equals("@returnval")).Value.Equals(0);
        }
      
        public async Task<int> ExecuteNonQueryWithIntOutput(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken, TimeSpan? customTimeout = null)
        {
                var updateSQLParams = this.AddReturnParameter(sqlParams.ToList());
                await this._context.Database.ExecuteSqlRawAsync($"exec @returnval = {schema}.{storedProcedureName} {strParam}", updateSQLParams, cancellationToken);
                return Convert.ToInt32(updateSQLParams.FirstOrDefault(p => p.ParameterName.ToLower().Equals("@returnval")).Value);
        }

        public async Task<bool> ExecuteNonQuery<Trequest>(string schema, string storedProcedureName, Trequest data, CancellationToken cancellationToken) where Trequest : class
        {
            var (strParam, sqlParams) = await this.BuildParams(schema, storedProcedureName, data);
            return await this.ExecuteNonQuery(schema, storedProcedureName, strParam, sqlParams, cancellationToken);
        }

        public async Task<long> ExecuteNonQueryWithOutput(string schema, string storedProcedureName, string strParam, IEnumerable<SqlParameter> sqlParams, CancellationToken cancellationToken)
        {
            var updateSQLParams = this.AddReturnParameter(sqlParams.ToList());
            await this._context.Database.ExecuteSqlRawAsync($"exec @returnval = {schema}.{storedProcedureName} {strParam}", updateSQLParams, cancellationToken);
            return Convert.ToInt64(updateSQLParams.FirstOrDefault(p => p.ParameterName.ToLower().Equals("@returnval")).Value);
        }
        #endregion

        #region method for transaction Non Query Procedure
        public async Task<bool> TransExecuteNonQuery(string schema, string storedProcedureName, string strParam, List<SqlParameter> sqlParams, CancellationToken cancellationToken, TimeSpan? customTimeout = null)
        {
            using (var transaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    List<SqlParameter> updateSQLParams = AddReturnParameter(sqlParams);
                    await this._context.Database.ExecuteSqlRawAsync($"exec @returnval = {schema}.{storedProcedureName} {strParam}", updateSQLParams, cancellationToken);

                    await transaction.CommitAsync();
                    
                    return updateSQLParams.FirstOrDefault(p => p.ParameterName.ToLower().Equals("@returnval")).Value.Equals(0);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return false;
            }
        }
        #endregion

    }
}
