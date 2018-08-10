using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SchoolManagement.Models;
namespace Admin2.Models
{
    public class dbcontext:DbContext
    {
        public dbcontext() : base("dbcontext")
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<dbcontext, SchoolFee.Migrations.Configuration>("dbcontext"));
        }

        public System.Data.Entity.DbSet<SchoolManagement.Models.Session> Sessions { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.Section> Sections { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.SchoolClass> SchoolClasses { get; set; }



        public System.Data.Entity.DbSet<SchoolManagement.Models.TransportCharges> TransportCharges { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.FeeModule> FeeModules { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.Month> Months { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.CatData> CatDatas { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<SchoolManagement.Models.FeeStructure> FeeStructures { get; set; }

        public System.Data.Entity.DbSet<SchoolFee.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<SchoolFee.Models.MonthlyFee> MonthlyFees { get; set; }

        public System.Data.Entity.DbSet<SchoolFee.Models.ExpenseType> ExpenseTypes { get; set; }

        public System.Data.Entity.DbSet<SchoolFee.Models.Expense> Expenses { get; set; }

        public System.Data.Entity.DbSet<SchoolFee.Models.MasterFee> MasterFees { get; set; }
    }
}