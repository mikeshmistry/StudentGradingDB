using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Entity class to represent a grade object
    /// </summary>
    public class Grade
    {
        #region Properties

        /// <summary>
        /// Primary key auto seeded
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string LetterGrade { get; set; }

        
        

        #endregion

        #region Relationships

        /// <summary>
        /// One to many relationship between student and grade entity 
        /// </summary>
        public Student Student { get; set; }


        /// <summary>
        /// One to one relationship between course and grade entity
        /// </summary>
        public Course Course { get; set; }

        #endregion

    }
}
