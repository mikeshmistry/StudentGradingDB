using DatabaseContext;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradingDB.UnitTests
{
    /// <summary>
    /// Unit test class to test teacher repository class
    /// </summary>
    [TestClass]
   public class TeacherRepositoryTests : TestHelper
    {
        #region Repository

        /// <summary>
        /// Teacher repository 
        /// </summary>
        private TeacherRepository teacherRepository;

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

            //setup the teacher repository
            teacherRepository = new TeacherRepository(Context);

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
        /// Test to insert a single teacher into the database
        /// </summary>
        [TestMethod]
        public void AddTeacher()
        {
           
                //create the new teacher
                var newTeacher = new Teacher()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

                //add the teacher to the database
                teacherRepository.Add(newTeacher);

                //get all the teachers to a list
                var teacher = teacherRepository.GetAll().ToList();

                //only one teacher 
                Assert.AreEqual(1, teacher.Count());

                //FirstName are equal
                Assert.AreEqual(newTeacher.FirstName, teacher[0].FirstName);

                //LastName are equal
                Assert.AreEqual(newTeacher.LastName, teacher[0].LastName);
            }

        /// <summary>
        /// Test to update a single teacher into the database
        /// </summary>
        [TestMethod]
        public void UpdateTeacher()
        {
           
                //create the new teacher
                var newTeacher = new Teacher()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

                //add the teacher to the database
                teacherRepository.Add(newTeacher);

                //get all the teachers to a list should only be one
                var teacher = teacherRepository.GetAll().ToList();

                //only one teacher 
                Assert.AreEqual(1, teacher.Count());

                //FirstName are equal
                Assert.AreEqual(newTeacher.FirstName, teacher[0].FirstName);

                //LastName are equal
                Assert.AreEqual(newTeacher.LastName, teacher[0].LastName);

                //Change the teacher first and last name
                var updatedTeacher = teacher[0];

                updatedTeacher.FirstName = "John";
                updatedTeacher.FirstName = "Smith";

                //update the teacher
                teacherRepository.Update(updatedTeacher);

                //get the updated student by the id
                var changedTeacher = teacherRepository.GetId(updatedTeacher.TeacherId);

                //Updated FirstName are equal
                Assert.AreEqual(updatedTeacher.FirstName, changedTeacher.FirstName);

                //Updated LastName are equal
                Assert.AreEqual(updatedTeacher.LastName, changedTeacher.LastName);
            }
         
        

        /// <summary>
        /// Test to delete a single teacher from the database
        /// </summary>
        [TestMethod]
        public void DeleteTeacher()
        {
                //create the new student
                var newTeacher = new Teacher()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

                //add the teacher to the database
                teacherRepository.Add(newTeacher);

                //get all the teacher to a list should only be one
                var teacher = teacherRepository.GetAll().ToList();

                //only one teacher 
                Assert.AreEqual(1, teacher.Count());

                //FirstName are equal
                Assert.AreEqual(newTeacher.FirstName, teacher[0].FirstName);

                //LastName are equal
                Assert.AreEqual(newTeacher.LastName, teacher[0].LastName);

                //Remove the teacher
                teacherRepository.Remove(newTeacher);

                var teacherListAfterRemoved = teacherRepository.GetAll();

                //count should be zero
                Assert.AreEqual(0, teacherListAfterRemoved.Count());
            }

        #endregion

        #region Other Query Tests

        #region GetAssignedCourse Tests
        /// <summary>
        /// Test to get courses assigned to a teacher that does not exists
        /// </summary>
        [TestMethod]
        public void GetAssignedCourse_TeacherNotFound_ReturnsFalse()
        {
            var coursesList = teacherRepository.GetAssignedCourses(1223);
            //should return a null course list
            Assert.IsNull(coursesList);

        }


        /// <summary>
        /// Test to get all enrolled courses for a teacher
        /// </summary>
        [TestMethod]
        public void GetAssignedCourse_TeacherFound_ReturnsCourses()
        {
            //create the new student
            var newTeacher = new Teacher()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add the teacher to the database
            teacherRepository.Add(newTeacher);

            //add some courses
            var courseRepository = new CourseRepository(Context);

            var courseList = new List<Course>();

            // add a list of courses
            courseList.Add(new Course { Name = "Introduction to C#", Description = "Introduces students to C# programming" });
            courseList.Add(new Course { Name = "Introduction to Java", Description = "Introduces students to Java programming" });

            //add the course using add range
            courseRepository.AddRange(courseList);

            //use the find method to find a teacher 
            var foundTeacher = teacherRepository.Find(teacher => teacher.FirstName == "Mikesh" && teacher.LastName == "Mistry").FirstOrDefault();

            //found the student
            if (foundTeacher != null)
            {
                //enroll the teacher into the two course
                foreach (var course in courseList)
                {
                    courseRepository.AssignTeacherToCourse(foundTeacher.TeacherId, course.CourseId);
                }


                //get the found teacher
                var teacherCourseList = teacherRepository.GetId(foundTeacher.TeacherId);

                if (teacherCourseList != null)
                {
                    //the count of the courseList used to add the courses to the teacher
                    var courseListCount = courseList.Count();
                    var teacherEnrolledCoursesCount = teacherCourseList.Courses.Count();

                    //insure the counts are the same
                    Assert.AreEqual(courseListCount, teacherEnrolledCoursesCount);

                    //insure that ids are the same
                    Assert.AreEqual(foundTeacher.TeacherId, teacherCourseList.TeacherId);

                }


            }

        }

        #endregion

        #endregion
    }
}
