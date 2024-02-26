using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence.Concrete.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly AppDbContext _context;

		public BaseRepository(AppDbContext context)
        {
			_context = context;
		}


        public async Task<bool> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return await SaveAsync();
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> exp = null)
		{
			return await _context.Set<T>().AnyAsync(exp);
		}

		public async Task<bool> DeleteAsync(T entity)
		{
			await Task.Run(() => _context.Remove(entity));			
			return await SaveAsync();
		}

		public async Task<bool> DeleteAsync(int id)
		{
			T entity = await GetByIdAsync(id);
			await Task.Run(() => _context.Set<T>().Remove(entity));
			return await SaveAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? exp = null)
		{
			if (exp == null)
				return await _context.Set<T>().ToListAsync();
			else 
				return await _context.Set<T>().Where(exp).ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> GetByWhereAsync(Expression<Func<T, bool>> exp)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(exp);
		}
		

		public async Task<bool> SaveAsync()
		{
			try
			{
				return await _context.SaveChangesAsync() > 0;
			}
			catch (Exception ex)
			{
				Console.Write(ex.ToString());
				return false;
			}
		}

		public async Task<bool> UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
			return await SaveAsync();
		}

		public async Task<bool> UpdateAsync(int id)
		{
			T entity = await GetByIdAsync(id);
			_context.Set<T>().Update(entity);
			return await SaveAsync();
		}
	}
}
