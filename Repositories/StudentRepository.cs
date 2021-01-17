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
        #region Properties

        /// <summary>
        /// private property to get a connection to the Student grading context
        /// </summary>
        private StudentGradingContext StudentGradingContext
        {
            get
            {
                return Context as StudentGradingContext;
            }
        }

        #endregion

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

            var courses = StudentGradingContext.Students
                          .Where(c => c.StudentId == id)
                          .Include(c => c.Courses)
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
            List<Grade> studentGrades = null;


            var grades = StudentGradingContext.Students
                         .Where(grade => grade.StudentId == id)
                         .Include(student => student.Grades)
                         .FirstOrDefault();


            if (grades != null)
                studentGrades = grades.Grades.ToList();


            return studentGrades;
        }


        /// <summary>
        /// Method to delete a student from the database
        /// </summary>
        /// <param name="teacherId">The id of the student to delete</param>
        public void DeleteStudent(int studentId)
        {
            var student = StudentGradingContext.Students
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
