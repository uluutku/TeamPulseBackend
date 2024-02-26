using HRAppBackend.Domain.Entities;
using HRAppBackend.Persistence.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence.Context
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Profession> Professions { get; set; }
		public DbSet<Leave> Leaves { get; set; }
		public DbSet<Company> Companies { get; set; }



		protected override void OnModelCreating(ModelBuilder builder)
		{



			//builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(builder);


		}
	}
}
