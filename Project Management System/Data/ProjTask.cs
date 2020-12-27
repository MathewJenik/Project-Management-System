using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Data
{
    public class ProjTask
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public List<SubTasks> SubTasks { get; set; }
    }
}
