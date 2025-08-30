using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpFunction.Models
{
    public class Students
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }

    public class EmployeeSkill
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public List<string> Skills { get; set; } = [];
    }
}
