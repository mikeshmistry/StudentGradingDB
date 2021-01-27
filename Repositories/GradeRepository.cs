using DatabaseContext;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repositories
{
    /// <summary>
    /// Grade repository class that handles common queries 
    /// </summary>
    public class GradeRepository : Repository<Grade>
    {

        #region Constructors 

        /// <summary>
        /// Constructor to take in any context 
        /// </summary>
        /// <param name="context">The context object for the database</param>
        public GradeRepository(DbContext context) : base(context)
        {

        }

        #endregion

        #region Other Queries

        /// <summary>
        /// Query that assigns a grade to a student
        /// </summary>
        /// <param name="studentId">The id of the student to add the grade to</param>
        /// <param name="courseId">The id of the course to assign the grade to </param>
        /// <param name="grade">the grade of the course that the student got</param>
        /// <returns>True if the grade was assigned to the student</returns>
        public bool AssignGradeToStudent(int studentId, int courseId, string grade)
        {
            var added = false;



            //check to see if the student exists
            var student = Context.Set<Student>()
                          .Where(student => student.StudentId == studentId)
                          .Include(g => g.Grades)
                          .FirstOrDefault();

            var course = Context.Set<Course>()
                        .Where(course => course.CourseId == courseId)
                        .FirstOrDefault();

            var gradeLength = grade.Length;

            //check to see if the student, course and grade is length is between 1 and 2 characters
            if (student != null && course != null && (gradeLength >= 1 && gradeLength <= 2))
            {
                //check to see if a record exists in grade
                var doesGradeExist = DoesGradeExist(studentId, courseId);

                // if the grade record does not exist add it and set added to be true
                if (doesGradeExist == false)
                {
                    var newGrade = new Grade();
                    newGrade.Student = student;
                    newGrade.LetterGrade = grade;
                    newGrade.Course = course;



                    student.Grades.Add(newGrade);
                    Context.SaveChanges();

                    added = true;
                }
                else
                {
                    //update the grade 
                    var updatedGrade = Find(grade => grade.Student.StudentId == student.StudentId
                                           && grade.Course.CourseId == course.CourseId
                                          ).FirstOrDefault();

                    //found the grade update the letter grade
                    if (updatedGrade != null)
                    {
                        updatedGrade.LetterGrade = grade;
                        Update(updatedGrade);
                        added = true;
                    }



                }


            }

            return added;
        }


        /// <summary>
        /// Query to check if a grade exist for a given student that is enrolled in a course
        /// </summary>
        /// <param name="studentId">The student Id to check</param>
        /// <param name="courseId">The course Id to check</param>
        /// <returns>True if the grade record does exist. False otherwise </returns>
        public bool DoesGradeExist(int studentId, int courseId)
        {
            var result = false;

            var gradeRecord = Context.Set<Grade>()
                            .Where(grade => grade.Student.StudentId == studentId && grade.Course.CourseId == courseId)
                            .FirstOrDefault();

            if (gradeRecord != null)
                result = true;

            return result;
        }

        #endregion


    }
}
