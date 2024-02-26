using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Concrete
{
	public class ProfessionManager : BaseManager<Profession>, IProfessionService
	{
		public ProfessionManager(IBaseRepository<Profession> baseRepository) : base(baseRepository)
		{
		}
	}
}
