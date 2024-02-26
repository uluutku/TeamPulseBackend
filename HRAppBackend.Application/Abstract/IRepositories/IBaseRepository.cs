using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Abstract.IRepositories
{
	public interface IBaseRepository<T> where T : class
	{
		Task<bool> AddAsync(T entity);
		Task<bool> UpdateAsync(T entity);
		Task<bool> UpdateAsync(int id);
		Task<bool> DeleteAsync(T entity);
		Task<bool> DeleteAsync(int id);		
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp = null);
		Task<T> GetByIdAsync(int id);
		Task<T> GetByWhereAsync(Expression<Func<T, bool>> exp = null);
		Task<bool> AnyAsync(Expression<Func<T, bool>> exp = null);
		Task<bool> SaveAsync();
	}
}
