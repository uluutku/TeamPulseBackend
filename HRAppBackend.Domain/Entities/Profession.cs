using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Domain.Entities
{
	public class Profession
	{
        public Profession()
        {
			Employees = new List<Employee>();
		}
        public int Id { get; set; }
        public string Name { get; set; }


        //Navigation Property
        public List<Employee> Employees { get; set; }        

    }
}
