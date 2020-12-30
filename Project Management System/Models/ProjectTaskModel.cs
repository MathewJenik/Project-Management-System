using Project_Management_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Models
{
    public class ProjectTaskModel
    {

        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjTask projTask { get; set; }
    }
}
