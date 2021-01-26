
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseContext
{
    /// <summary>
    /// Context class that will be the main connection for testing student grading database
    /// </summary>
   public class StudentGradingTestContext : DbContext
    {

        #region Properties 

        #endregion

        public SqliteConnection Connection { get; set; }

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StudentGradingTestContext() : base()
        {
            Connection = new SqliteConnection("DataSource =:memory: ");
        }


     
        #endregion 

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        #region Entities 

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        #endregion 
    }
}
