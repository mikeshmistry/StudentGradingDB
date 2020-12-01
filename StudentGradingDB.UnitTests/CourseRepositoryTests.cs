using DatabaseContext;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System.Linq;

namespace StudentGradingDB.UnitTests
{
  /// <summary>
  /// Unit test class to test course repository class
  /// </summary>
  [TestClass]
  public  class CourseRepositoryTests : TestHelper
  {
        #region Repository

        /// <summary>
        /// Course repository 
        /// </summary>
        private CourseRepository courseRepository;

        #endregion

        #region Setup and CleanUp for tests

        /// <summary>
        /// Runs once per test case
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            //setup the connection and context
            SetupConnectionAndContext();

            //setup the course repository
            courseRepository = new CourseRepository(Context);

        }


        /// <summary>
        /// Runs after each test
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            //close the connection
            Connection.Close();
        }

        #endregion

        #region Add, Update, Delete Tests

        /// <summary>
        /// Test to insert a single course into the database
        /// </summary>
        [TestMethod]
        public void AddCourse()
        {
                //create the new course
                var newCourse = new Course()
                {
                    Name = "Introduction To C# Programming",
                    Description = "Course to introduce students to C# programming"
                };

                //add the course to the database
                courseRepository.Add(newCourse);

                //get all the courses to a list
                var courses = courseRepository.GetAll().ToList();

                //only one course 
                Assert.AreEqual(1, courses.Count());

                //Name are equal
                Assert.AreEqual(newCourse.Name, courses[0].Name);

                //Description are equal
                Assert.AreEqual(newCourse.Description, courses[0].Description);
        }
           
        


        /// <summary>
        /// Test to update a single course into the database
        /// </summary>
        [TestMethod]
        public void UpdateCourse()
        {
            
                //create the new course
                var newCourse = new Course()
                {
                    Name = "Introduction To C# Programming",
                    Description = "Course to introduce students to C# programming"
                };

                //add the course to the database
                courseRepository.Add(newCourse);

                //get all the courses to a list
                var course = courseRepository.GetAll().ToList();

                //only one course 
                Assert.AreEqual(1, course.Count());

                //Name are equal
                Assert.AreEqual(newCourse.Name, course[0].Name);

                //Description are equal
                Assert.AreEqual(newCourse.Description, course[0].Description);

                //Change the course 
                var updatedCourse = course[0];

                updatedCourse.Name = "Advanced C# Topics";
                updatedCourse.Description = "Advanced topics on C# programming";

                //update the course
                courseRepository.Update(updatedCourse);

                //get the updated course by the id
                var changedCourse = courseRepository.GetId(updatedCourse.CourseId);

                //Updated Name are equal
                Assert.AreEqual(updatedCourse.Name, changedCourse.Name);

                //Updated Description are equal
                Assert.AreEqual(updatedCourse.Description, changedCourse.Description);

            }

           

        /// <summary>
        /// Test to delete a single course from the database
        /// </summary>
        [TestMethod]
        public void DeleteCourse()
        {

                //create the new course
                var newCourse = new Course()
                {
                    Name = "JavaScript Programming",
                    Description = "Introduction to JavaScript Programming"
                };

                //add the course to the database
                courseRepository.Add(newCourse);

                //get all the courses to a list should only be one
                var course = courseRepository.GetAll().ToList();

                //only one course 
                Assert.AreEqual(1, course.Count());

                //Name are equal
                Assert.AreEqual(newCourse.Name, course[0].Name);

                //Descriptions are equal
                Assert.AreEqual(newCourse.Description, course[0].Description);

                //Remove the course
                courseRepository.Remove(newCourse);

                var courseListAfterRemoved = courseRepository.GetAll();

                //count should be zero
                Assert.AreEqual(0, courseListAfterRemoved.Count());
            }


        #endregion

   #region Other Query Tests

        #region  AssignTeacherToCourse Tests

        /// <summary>
        /// Test to assign a course to a teacher that does not exist
        /// </summary>
        [TestMethod]
        public void AssignTeachToCourse_TeacherNotFound_ReturnsFalse()
        {
            var courseRepository = new CourseRepository(Context);

            //create the course
            var course = new Course()
            {
                Name = "Introduction to C#",
                Description = "Introduction to C# programming fundamentals"
            };

            //add a course
            courseRepository.Add(course);

            //find the newly added course
            var enrollingCourse = courseRepository.Find(course => course.Name == "Introduction to C#")
                                                  .FirstOrDefault();
            //found the course
            if (enrollingCourse != null)
            {
                //assign a course to a teacher that does not exist
                var assignedTeacherToCourse = courseRepository.AssignTeacherToCourse(1234, enrollingCourse.CourseId);

                //should return false indicating course was not assigned to the teacher
                Assert.IsFalse(assignedTeacherToCourse);
            }
        }

        /// <summary>
        /// Test to assign a teacher to a course that does not exist
        /// </summary>
        [TestMethod]
        public void AssignTeachToCourse_CourseNotFound_ReturnsFalse()
        {
            var teacherRepository = new TeacherRepository(Context);

            var teacher = new Teacher()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add a teacher
            teacherRepository.Add(teacher);

            //find the newly added teacher
            var enrollingTeacher = teacherRepository.Find(teacher => teacher.FirstName == "Mikesh")
                                                  .FirstOrDefault();
            //found the teacher
            if (enrollingTeacher != null)
            {
                //assign a teacher to a course that does not exist
                var assignedCourseToTeacher = courseRepository.AssignTeacherToCourse(enrollingTeacher.TeacherId, 1234);

                //should return false indicating course was not assigned to the teacher
                Assert.IsFalse(assignedCourseToTeacher);
            }
        }

        /// <summary>
        /// Test to assign a teacher to a course where both the teacher and course are not found
        /// </summary>
        [TestMethod]
        public void AssignTeachToCourse_TeacherNotFoundAndCourseNotFound_ReturnsFalse()
        {
           

                //assign a teacher to a course where both teacher and course are not found
                var assignedCourseToTeacher = courseRepository.AssignTeacherToCourse(64587, 1234);

                //should return false indicating course was not assigned to the teacher
                Assert.IsFalse(assignedCourseToTeacher);
         }

        /// <summary>
        /// Test to assign a teacher to a course successfully returns true
        /// </summary>
        [TestMethod]
        public void AssignTeacherToCourse_TeacherAndCourseFound_ReturnsTrue()
        {
            var teacherRepository = new TeacherRepository(Context);

            var teacher = new Teacher()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add a teacher
            teacherRepository.Add(teacher);

            //create the course
            var course = new Course()
            {
                Name = "Introduction to C#",
                Description = "Introduction to C# programming fundamentals"
            };

            //add a course
            courseRepository.Add(course);

            //find the newly added teacher
            var enrollingTeacher = teacherRepository.Find(teacher => teacher.FirstName == "Mikesh")
                                                  .FirstOrDefault();

            //find the newly added course
            var enrollingCourse = courseRepository.Find(course => course.Name == "Introduction to C#")
                                                  .FirstOrDefault();
            //teacher and course found
            if(enrollingTeacher !=null && enrollingCourse !=null)
            {
                var isEnrolled = courseRepository.AssignTeacherToCourse(enrollingTeacher.TeacherId, enrollingCourse.CourseId);
                Assert.IsTrue(isEnrolled);

                Assert.AreEqual(1, enrollingTeacher.Courses.Count());
            }
        }

        #endregion

        #region EnrollStudentIntoCourse
       
        /// <summary>
        /// Test to enroll a student into a course where the student does not exist
        /// </summary>
        [TestMethod]
        public void EnrollStudentIntoCourse_StudentNotFound_ReturnsFalse()
        {
            var courseRepository = new CourseRepository(Context);

            //create the course
            var course = new Course()
            {
                Name = "Introduction to C#",
                Description = "Introduction to C# programming fundamentals"
            };

            //add a course
            courseRepository.Add(course);

            //find the newly added course
            var enrollingCourse = courseRepository.Find(course => course.Name == "Introduction to C#")
                                                  .FirstOrDefault();
            //found the course
            if (enrollingCourse != null)
            {
                //assign a course to a student that does not exist
                var assignedStudentToCourse = courseRepository.EnrollStudentIntoCourse(1234, enrollingCourse.CourseId);

                //should return false indicating course was not assigned to the student
                Assert.IsFalse(assignedStudentToCourse);
            }
        }


        /// <summary>
        /// Test to enroll a student to a course that does not exist
        /// </summary>
        [TestMethod]
        public void EnrollStudentIntoCourse_CourseNotFound_ReturnsFalse()
        {
            var studentRepository = new StudentRepository(Context);

            var student = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add a student
            studentRepository.Add(student);

            //find the newly added teacher
            var enrollingStudent = studentRepository.Find(student => student.FirstName == "Mikesh")
                                                  .FirstOrDefault();
            //found the student
            if (enrollingStudent != null)
            {
                //assign a student to a course that does not exist
                var enrolledStudent = courseRepository.EnrollStudentIntoCourse(enrollingStudent.StudentId, 1234);

                //should return false indicating student was not enrolled in the course
                Assert.IsFalse(enrolledStudent);
            }
        }


        /// <summary>
        /// Test to enroll a student to a course where both the student and course are not found
        /// </summary>
        [TestMethod]
        public void EnrollStudentIntoCourse_StudentNotFoundAndCourseNotFound_ReturnsFalse()
        {

            //assign a teacher to a course where both teacher and course are not found
            var enrollCourseToStudent = courseRepository.EnrollStudentIntoCourse(64587, 1234);

            //should return false indicating course was not assigned to the teacher
            Assert.IsFalse(enrollCourseToStudent);
        }


        /// <summary>
        /// Test to enroll a student to a course successfully returns true
        /// </summary>
        [TestMethod]
        public void EnrollStudentIntoCourse_StudentAndCourseFound_ReturnsTrue()
        {
            var studentRepository = new StudentRepository(Context);

            var student = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add a student
            studentRepository.Add(student);

            //create the course
            var course = new Course()
            {
                Name = "Introduction to C#",
                Description = "Introduction to C# programming fundamentals"
            };

            //add a course
            courseRepository.Add(course);

            //find the newly added student
            var enrollingStudent = studentRepository.Find(teacher => teacher.FirstName == "Mikesh")
                                                  .FirstOrDefault();

            //find the newly added course
            var enrollingCourse = courseRepository.Find(course => course.Name == "Introduction to C#")
                                                  .FirstOrDefault();
            //student and course found
            if (enrollingStudent != null && enrollingCourse != null)
            {
                var isEnrolled = courseRepository.EnrollStudentIntoCourse(enrollingStudent.StudentId, enrollingCourse.CourseId);
                Assert.IsTrue(isEnrolled);

                Assert.AreEqual(1, enrollingStudent.Courses.Count());
            }
        }

        #endregion

        #endregion


    }
}
