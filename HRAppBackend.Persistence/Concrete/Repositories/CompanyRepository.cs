using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Domain.Entities;
using HRAppBackend.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence.Concrete.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }


}
