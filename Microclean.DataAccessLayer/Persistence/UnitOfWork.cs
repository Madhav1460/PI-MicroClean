using Microclean.DataAccessLayer.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.DataAccessLayer.Persistence
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public ApiResultCode SaveChanges()
        {
            try
            {
                Context.SaveChanges();
                return new ApiResultCode(ApiResultType.Success, 1, "Save successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.DataAccessLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Exception during saveing");
            }
        }
        public async Task<ApiResultCode> SaveChangesAsync()
        {
            try
            {
                _ = await Context.SaveChangesAsync();
                return new ApiResultCode(ApiResultType.Success, 1, "Save successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.DataAccessLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Exception during saveing");
            }
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
            return (IRepository<TEntity>)_repositories[type];
        }
        public TContext Context { get; }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
