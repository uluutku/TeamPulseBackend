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
	public class AdvancePaymentRepository : BaseRepository<AdvancePayment>, IAdvancePaymentRepository
	{
		private readonly AppDbContext _context;

		public AdvancePaymentRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
