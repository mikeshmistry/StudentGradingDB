using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Entity class to represent a teacher object
    /// </summary>
    public class Teacher
    {
        #region Properties 

        /// <summary>
        /// Primary key auto seeded
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }

        /// <summary>
        /// FirstName of the teacher this is a required field
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of the teacher this is a required field
        /// </summary>
        [Required]
        public string LastName { get; set; }
        #endregion

        #region Relationships

        /// <summary>
        /// one to many relationship with courses
        /// </summary>
        public ICollection<Course> Courses { get; set; }
        #endregion
    }
}
