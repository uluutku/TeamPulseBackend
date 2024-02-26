using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.ExpenseDTOs
{
    public class UpdateExpanseDTO
    {
        public string Type { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string InvoicePath { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
