using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> projects { get; set; }
        public DbSet<ProjTask> projTasks { get; set; }
        public DbSet<TaskStatus> taskStatuses { get; set; }
        public DbSet<SubTasks> subTasks { get; set; }
        

    }
}
