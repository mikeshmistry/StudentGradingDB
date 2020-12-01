using Microsoft.EntityFrameworkCore;
using Entities;
using DatabaseContext;
using System.Linq;

namespace Repositories
{
    /// <summary>
    /// Course repository class that handles common queries 
    /// </summary>
    public class CourseRepository : Repository<Course>
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
        public CourseRepository(DbContext context) : base(context)
        {

        }

        #endregion

        #region Other Queries

        /// <summary>
        /// Query to assign a teacher to a course
        /// </summary>
        /// <param name="teacherId">The id of the teacher</param>
        /// <param name="courseId">The course to assign the teacher to</param>
        /// <returns>True if the teacher is assigned to a course</returns>
        public bool AssignTeacherToCourse(int teacherId, int courseId)
        {
            var added = false;

            //check to see if the teacher exists
            var teacher = StudentGradingContext.Teachers
                          .Where(teacher => teacher.TeacherId == teacherId)
                          .Include(teacher => teacher.Courses)
                          .FirstOrDefault();


            //check to see if the course exists
            var course = StudentGradingContext.Courses
                         .Where(course => course.CourseId == courseId)
                         .FirstOrDefault();

            //the teacher exists and the course is a valid course
            if (teacher != null && course != null)
            {
                var isAssignedToCourse = IsCourseAssignedToTeacher(teacherId, courseId);

                //course is not assigned
                if (isAssignedToCourse == false)
                {
                    //add the course to the teacher
                    teacher.Courses.Add(course);
                    StudentGradingContext.SaveChanges();
                    added = true;

                }


            }

           
            return added;
        }



        /// <summary>
        /// Query to check to see if a teacher is already assigned to a course
        /// </summary>
        /// <param name="teacherId">The id of the teacher to check</param>
        /// <param name="courseId">The id of the course to check</param>
        /// <returns>True if the course is already assigned to the teacher. False otherwise</returns>
        private bool IsCourseAssignedToTeacher(int teacherId, int courseId)
        {
            var isAssignedToCourse = false;

            var teacherCourses = StudentGradingContext.Teachers
                            .Where(teacher => teacher.TeacherId == teacherId)
                            .Include(teacher => teacher.Courses)
                            .FirstOrDefault();

            //if the teacher is found check to see if the course is already assigned 
            if (teacherCourses != null)
            {
                var courses = teacherCourses.Courses
                             .Where(course => course.CourseId == courseId)
                             .FirstOrDefault();

                //already assigned to the course
                if (courses != null)
                    isAssignedToCourse = true;
            }

            return isAssignedToCourse;
        }

        /// <summary>
        /// Query to enroll a student into a given course
        /// </summary>
        /// <param name="studentId">The student id</param>
        /// <param name="courseId">The id of the course to enroll the student</param>
        /// <returns>True if the student was enrolled in the course.False otherwise</returns>
        public bool EnrollStudentIntoCourse(int studentId,int courseId)
        {
            var added = false;

            //check to see if the student exists
            var student = StudentGradingContext.Students
                          .Where(student => student.StudentId == studentId)
                          .Include(c => c.Courses)
                          .FirstOrDefault();


            //check to see if the course exists
            var course = StudentGradingContext.Courses
                         .Where(course => course.CourseId == courseId)
                         .FirstOrDefault();

            //the teacher exists and the course is a valid course
            if (student != null && course != null)
            {
                var isAssignedToCourse = IsStudentEnrolledinCourse(studentId, courseId);

                //course is not assigned
                if (isAssignedToCourse == false)
                {
                    //add the course to the student
                    student.Courses.Add(course);
                    StudentGradingContext.SaveChanges();
                    added = true;

                }


            }

            return added;

        }


        /// <summary>
        /// Query to check to see if a student is already enrolled into to a course
        /// </summary>
        /// <param name="studentId">The id of the student to check</param>
        /// <param name="courseId">The id of the course to check</param>
        /// <returns>True if the student is already enrolled into to the course. False otherwise</returns>
        private bool IsStudentEnrolledinCourse(int studentId, int courseId)
        {
            var isAssignedToCourse = false;

            var studentCourses = StudentGradingContext.Students
                            .Where(student => student.StudentId == studentId)
                            .Include(teacher => teacher.Courses)
                            .FirstOrDefault();

            //if the student is found check to see if the course is already assigned 
            if (studentCourses != null)
            {
                var courses = studentCourses.Courses
                             .Where(course => course.CourseId == courseId)
                             .FirstOrDefault();

                //already assigned to the course
                if (courses != null)
                    isAssignedToCourse = true;
            }

            return isAssignedToCourse;
        }

        #endregion


    }
}
