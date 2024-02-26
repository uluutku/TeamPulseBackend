using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Domain.Entities;
using HRAppBackend.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence.Concrete.Repositories
{
    public class LeaveRepository : BaseRepository<Leave>, ILeaveRepository
    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Leave>> GetLeavesByUserIdAsync(string userId)
        {
            return await _context.Leaves.Where(x => x.Employee.AppUserId == userId).ToListAsync();
        }
    }
}
