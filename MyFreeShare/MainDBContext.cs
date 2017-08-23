using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFreeShare.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyFreeShare
{
    public class MainDBContext:DbContext
    {
        public MainDBContext():base("DatabaseEntities")
        {}
        public DbSet<pengguna> dataPengguna { get; set; }
        public DbSet<File> dataFile { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}