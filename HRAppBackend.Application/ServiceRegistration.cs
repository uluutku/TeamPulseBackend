using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Application.Services.Concrete;
using HRAppBackend.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application
{
	public static class ServiceRegistration
	{
		public static void RepositoriesInjections(this IServiceCollection services) 
		{
			services.AddTransient(typeof(IBaseService<>), typeof(BaseManager<>));
			services.AddTransient<IEmployeeService, EmployeeManager>();
			services.AddTransient<IProfessionService, ProfessionManager>();
			services.AddTransient<IDepartmentService, DepartmentManager>();
			services.AddTransient<IAuthenticationService, AuthenticateManager>();
			services.AddTransient<IExpenseService, ExpenseManager>();
			services.AddTransient<IAdvancePaymentService, AdvancePaymentManager>();
			services.AddTransient<ILeaveService, LeaveManager>();
			services.AddTransient<ICompanyService, CompanyManager>();

			
		}
	}
}
