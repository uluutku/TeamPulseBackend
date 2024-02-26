using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Domain.Entities;
using HRAppBackend.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence.Concrete.Repositories
{
	public class ProfessionRepository : BaseRepository<Profession>, IProfessionRepository
	{
		private readonly AppDbContext _context;

		public ProfessionRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
