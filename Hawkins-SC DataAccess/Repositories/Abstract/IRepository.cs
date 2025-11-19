using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface IRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(Guid id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
		Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		void Update(T entity);
		void UpdateRange(IEnumerable<T> entities);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
	}
}
