using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sxpTestProj.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Employee> Employees = new List<Employee>();
    }
}
