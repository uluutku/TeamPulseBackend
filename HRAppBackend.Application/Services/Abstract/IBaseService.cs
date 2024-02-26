using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
	public interface IBaseService<T> 
	{
		Task<bool> TAddAsync(T entity);
		Task<bool> TUpdateAsync(T entity);
		Task<bool> TUpdateAsync(int id);
		Task<bool> TDeleteAsync(T entity);
		Task<bool> TDeleteAsync(int id);
		Task<List<T>> TGetAllAsync(Expression<Func<T, bool>> exp = null);
		Task<T> TGetByIdAsync(int id);
		Task<T> TGetByWhereAsync(Expression<Func<T, bool>> exp = null);
		Task<bool> TAnyAsync(Expression<Func<T, bool>> exp = null);
	}

}
