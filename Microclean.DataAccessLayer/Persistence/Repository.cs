using Microclean.DataAccessLayer.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.DataAccessLayer.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public virtual async Task<ApiResultCode> Add(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                return new ApiResultCode(ApiResultType.Success, 1, "Added successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Error During Saving");
            }
        }

        public virtual async Task<ApiResultCode> AddAll(IEnumerable<TEntity> entities)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);
                return new ApiResultCode(ApiResultType.Success, 1, "All items added successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Error During List Saving");
            }
        }

        public virtual async Task<ApiResultCode> Remove(dynamic Id)
        {
            try
            {
                var result = await GetByID(Id);
                if (result.HasSuccess)
                {
                    _context.Set<TEntity>().Remove(result.UserObject);
                    return new ApiResultCode(ApiResultType.Success, 1, "Deleted successfully");
                }
                return new ApiResultCode(ApiResultType.Error, 0, "Error during delete");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Error during item delete");
            }
        }

        public virtual async Task<ApiResultCode> RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().RemoveRange(entities);
                await _context.SaveChangesAsync();
                return new ApiResultCode(ApiResultType.Success, 1, "Deleted successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Error during item delete");
            }

        }

        public virtual ApiResultCode Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return new ApiResultCode(ApiResultType.Success, 1, "Item updated successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Error during item update");
            }
        }

        public virtual ApiResultCode UpdateAll(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().AddRange(entities);
                return new ApiResultCode(ApiResultType.Success, 1, "Deleted successfully");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, 3, "Please contact system administrator");
            }
        }

        public virtual async Task<ApiResult<IEnumerable<TEntity>>> Get<TEntity2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity2>> order)
        {
            try
            {
                var result = await _context.Set<TEntity>().AsNoTracking().Where(predicate).OrderBy(order).ToListAsync();
                if (result == null)
                    return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }
        public virtual async Task<ApiResult<IEnumerable<TEntity>>> GetOderbyDesc<TEntity2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity2>> order)
        {
            try
            {
                var result = await _context.Set<TEntity>().AsNoTracking().Where(predicate).OrderByDescending(order).ToListAsync();
                if (result == null)
                    return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }

        public virtual async Task<ApiResult<IEnumerable<TEntity>>> GetAll()
        {
            try
            {
                var result = await _context.Set<TEntity>().ToListAsync();
                if (result == null)
                    return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 1, "No data in given request"));

                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 3, "Please contact system administrator"));
            }
        }

        public virtual async Task<ApiResult<TEntity>> GetByID(dynamic Id)
        {
            try
            {
                var result = await _context.Set<TEntity>().FindAsync(Id);
                if (result == null)
                    return new ApiResult<TEntity>(new ApiResultCode(ApiResultType.Error, 0, "No data fount in ginven request"));

                return new ApiResult<TEntity>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<TEntity>(new ApiResultCode(ApiResultType.Error, 3, "Please contact system administrator"));
            }
        }

        public virtual async Task<ApiResult<TEntity>> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
                if (result == null)
                    return new ApiResult<TEntity>(new ApiResultCode(ApiResultType.Error, 0, "Data not found"));

                return new ApiResult<TEntity>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<TEntity>(new ApiResultCode(ApiResultType.Error, 3, "Please contact system administrator"));
            }
        }

        public virtual async Task<ApiResult<IEnumerable<TEntity>>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().AsNoTracking().Where(predicate).AsNoTracking().ToListAsync();
                if (result == null)
                    return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }

        public virtual async Task<ApiResult<IEnumerable<TType>>> GetSelectedDataAsync<TType>(Expression<Func<TEntity, TType>> select)
        {
            try
            {
                var result = await _context.Set<TEntity>().Select(select).ToListAsync();

                if (result == null)
                    return new ApiResult<IEnumerable<TType>>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<IEnumerable<TType>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TType>>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }

        public virtual async Task<ApiResult<IEnumerable<TType>>> GetSelectedDataAsync<TType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TType>> select)
        {
            try
            {
                var result = await _context.Set<TEntity>().Where(predicate).Select(select).ToListAsync();

                if (result == null)
                    return new ApiResult<IEnumerable<TType>>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<IEnumerable<TType>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TType>>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }

        public virtual async Task<ApiResult<bool>> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().AnyAsync(predicate);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }

        public virtual async Task<ApiResult<int>> Count(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().Where(predicate).CountAsync();

                if (result <= 0)
                    return new ApiResult<int>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<int>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<int>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }

        public virtual async Task<ApiResult<IEnumerable<TEntity>>> GetPagedRecords(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize)
        {
            try
            {
                var result = await _context.Set<TEntity>().Where(predicate).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();

                if (result == null)
                    return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 0, "No data in given request"));

                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<TEntity>>(new ApiResultCode(ApiResultType.Error, 3, "No data in given request"));
            }
        }
    }
}
