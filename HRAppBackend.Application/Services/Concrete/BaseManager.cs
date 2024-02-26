using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Concrete
{
	public class BaseManager<T> : IBaseService<T> where T : class
	{
		private readonly IBaseRepository<T> _baseRepository;

		public BaseManager(IBaseRepository<T> baseRepository)
		{
			_baseRepository = baseRepository;
		}


		public async Task<bool> TAddAsync(T entity)
		{
			if (entity != null)
			{
				await _baseRepository.AddAsync(entity);
				return true;
			}
			else
			{ return false; }
		}

		public async Task<bool> TAnyAsync(Expression<Func<T, bool>> exp)
		{
			return await _baseRepository.AnyAsync(exp);
		}

		public async Task<bool> TDeleteAsync(T entity)
		{
			if (entity != null)
			{
				await _baseRepository.DeleteAsync(entity);
				return true;
			}
			else
			{ return false; }
		}

		public async Task<bool> TDeleteAsync(int id)
		{
			if (id > 0)
			{
				await _baseRepository.DeleteAsync(id);
				return true;
			}
			else
			{ return false; }
		}

		public async Task<List<T>> TGetAllAsync(Expression<Func<T, bool>> exp = null)
		{
			return await _baseRepository.GetAllAsync(exp);
		}

		public async Task<T> TGetByIdAsync(int id)
		{
			return await _baseRepository.GetByIdAsync(id);
		}

		public async Task<T> TGetByWhereAsync(Expression<Func<T, bool>> exp = null)
		{
			return await _baseRepository.GetByWhereAsync(exp);
		}

		public async Task<bool> TUpdateAsync(T entity)
		{
			if (entity != null)
			{
				await _baseRepository.UpdateAsync(entity);
				return true;
			}
			else
			{ return false; }
		}

		public async Task<bool> TUpdateAsync(int id)
		{
			if (id > 0)
			{
				await _baseRepository.UpdateAsync(id);
				return true;
			}
			else
			{ return false; }
		}
	}
}
