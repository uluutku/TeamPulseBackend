using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Application.Services.Concrete;
using HRAppBackend.Persistence.Concrete.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence
{
	public static class ServiceRegistration
	{
		public static void RepositoriesInjections(this IServiceCollection services)
		{
			services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
			services.AddTransient<IEmployeeRepository, EmployeeRepository>();
			services.AddTransient<IProfessionRepository, ProfessionRepository>();
			services.AddTransient<IDepartmentRepository, DepartmenRepository>();
			services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
			services.AddTransient<IExpenseRepository, ExpenseRepository>();
			services.AddTransient<IAdvancePaymentRepository, AdvancePaymentRepository>();
			services.AddTransient<ILeaveRepository, LeaveRepository>();
			services.AddTransient<ICompanyRepository, CompanyRepository>();

        }
	}
}
