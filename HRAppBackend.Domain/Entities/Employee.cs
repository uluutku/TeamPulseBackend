using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Domain.Entities
{
	public class Employee
	{
        public int Id { get; set; }
        public string? Address { get; set; }
		public string? City { get; set; }
		public string? County { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? MiddleName { get; set; }
		public string? SecondLastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? PlaceOfBirth { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int ProfessionId { get; set; }
		public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
		public string? FileName { get; set; }
		public string? FilePath { get; set; }
		public string TRIdentityNumber { get; set; }
		public decimal Salary { get; set; }
		public Status Status { get; set; }
        public string AppUserId { get; set; }

        public Employee()
        {
            Expenses = new List<Expense>();
			AdvancePayments = new List<AdvancePayment>();
			Leaves = new List<Leave>();
        }

        //Navigations 
        public AppUser AppUser { get; set; }
        public Profession Profession { get; set; }
		public Department Department { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<AdvancePayment> AdvancePayments { get; set; }
		public List<Leave> Leaves { get; set; }
        public Company Company { get; set; }
    }
}
