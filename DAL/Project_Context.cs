using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;




namespace DAL
{
    class Project_Context : DbContext
    {
        public   DbSet<Report> Reports { get; set; }
        public  DbSet<Assessment> Assessments { get; set; }
        public  DbSet<Fall> Falls { get; set; }
        public DbSet<Location_> Locations { get; set; }



    }
}
