using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Entity Class to represent a student object  
    /// </summary>
    public class Student
    {
        #region Properties 
        /// <summary>
        /// Primary key auto seeded 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        /// <summary>
        /// FirstName of the student this is a required field
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of the student this is a required field
        /// </summary>
        [Required]
        public string LastName { get; set; }


        #endregion

        #region Relationships

        /// <summary>
        /// One to many relationship between student and grade entity
        /// </summary>
        public ICollection<Grade> Grades { get; set; }

        public ICollection<Course> Courses { get; set; }

     
        #endregion

    }
}
