
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseContext
{
    /// <summary>
    /// Context class that will be the main connection for the student grading database
    /// </summary>
   public class StudentGradingContext : DbContext
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StudentGradingContext()
        {

        }


        /// <summary>
        /// Constructor that takes in a context options
        /// </summary>
        /// <param name="options">The options object</param>
        public StudentGradingContext(DbContextOptions<StudentGradingContext> options) :base(options)
        {
            
        }

        #endregion 

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
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
