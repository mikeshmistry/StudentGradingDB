using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DatabaseContext;

namespace Repositories
{
    /// <summary>
    /// Student repository class that handles common queries
    /// </summary>
    public class StudentRepository : Repository<Student>
    {
       
        #region Constructors 

        /// <summary>
        /// Constructor to take in any context 
        /// </summary>
        /// <param name="context">The context object for the database</param>
        public StudentRepository(DbContext context) : base(context)
        {

        }

        #endregion

        #region Other Queries

        /// <summary>
        /// Query to get all the courses for the given student 
        /// </summary>
        /// <param name="id">The id of the student</param>
        /// <returns>A list of course null if not found</returns>
        public List<Course> GetEnrolledCourses(int id)
        {
            List<Course> studentCourses = null;

            var courses = Context.Set<Student>()
                          .Where(c => c.StudentId == id)
                          .Include(c => c.Courses)
                          .AsNoTracking()
                          .FirstOrDefault();


            if (courses != null)
                studentCourses = courses.Courses.ToList();



            return studentCourses;

        }

        /// <summary>
        /// Query to get all grades for the given student, this includes the course
        /// </summary>
        /// <param name="id">the id of the student</param>
        /// <returns>A list of student grades null if not found</returns>
        public List<Grade> GetGrades(int id)
        {
            List<Grade> studentGrades = new List<Grade>();


            studentGrades = Context.Set<Grade>()
                         .Where(grade => grade.Student.StudentId == id)
                         .Include(grade => grade.Course)
                         .AsNoTracking()
                         .ToList<Grade>();


            return studentGrades;
        }


        /// <summary>
        /// Method to delete a student from the database
        /// </summary>
        /// <param name="teacherId">The id of the student to delete</param>
        public void DeleteStudent(int studentId)
        {
            var student = Context.Set<Student>()
                         .Where(student => student.StudentId == studentId)
                         .Include(student => student.Courses)
                         .Include(student => student.Grades)
                         .FirstOrDefault();

            if (student != null)
                Remove(student);
        }

        #endregion
    }
}
