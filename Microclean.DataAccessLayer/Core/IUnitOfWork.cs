using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.DataAccessLayer.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        ApiResultCode SaveChanges();
        Task<ApiResultCode> SaveChangesAsync();
    }
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
