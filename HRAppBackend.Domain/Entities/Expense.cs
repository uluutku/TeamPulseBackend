using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }
        public ExpenseType ExpenseType { get; set; } 
        public ApprovalStatus ApprovalStatus  { get; set; }
        public decimal Amount { get; set; } 
        public DateTime ExpenseDate { get; set; }
        public string InvoicePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedTime { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
