using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// Entity class to represent a course object
    /// </summary>
    public class Course
    {
        #region Properties

        /// <summary>
        /// Primary key auto seeded 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        /// <summary>
        /// Name of the course this is required
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the course this is required
        /// </summary>
        [Required]
        public string Description { get; set; }
        #endregion

        #region RelationShips

        /// <summary>
        /// One to many relationship between student and grade entity 
        /// </summary>
        public Student Student { get; set; }

        #endregion 
    }
}