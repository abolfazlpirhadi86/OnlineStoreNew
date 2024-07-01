using Common.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductManagement.Infrastructure.Persistence.DataBase.Repositories
{
    internal class DapperRepository
    {
        private IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;
        public DapperRepository(IConfiguration configuration, SqlConnectionProvider connectionProvider)
        {
        }


        public virtual void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var parameters = (object)Mapping(entity);
            if (_transaction != null && _transaction.Connection != null)
                entity.Id = _transaction.Connection.Insert<long>(_transaction, GetTableName<T>(), parameters);
            else
                entity.Id = Connection.Insert<long>(GetTableName<T>(), parameters);


        }

        #region privateMethod
        internal virtual dynamic Mapping<T>(T item) where T : class
        {
            return item;
        }
        #endregion
    }
}
