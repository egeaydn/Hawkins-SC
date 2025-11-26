using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hawkins.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Hawkins_SC_DataAccess.Repositories.Concrete
{
	public class Repository<T> where T : class
	{
		protected readonly HawkinsDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public Repository(HawkinsDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_dbSet = _context.Set<T>();
		}

		public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

		public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

		public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
			=> await _dbSet.Where(predicate).AsNoTracking().ToListAsync();

		public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
			=> await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

		public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
			=> predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

		public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
			=> await _dbSet.AnyAsync(predicate);

		public virtual async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public virtual void Update(T entity) => _dbSet.Update(entity);

		public virtual void Delete(T entity) => _dbSet.Remove(entity);
	}
}