using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.IO;
using MessMSPrism.Models;

namespace MessMSPrism
{
    public class MessMsContext : DbContext
    {
        public MessMsContext() : base("name=myConn")
        {

            Configuration.AutoDetectChangesEnabled = true;
            


            //connection.
        }
        
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        
    }
}   