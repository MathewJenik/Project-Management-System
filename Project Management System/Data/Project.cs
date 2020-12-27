using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Data
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ProjTask> Tasks { get; set; }
        public string UserID { get; set; }


    }
}
