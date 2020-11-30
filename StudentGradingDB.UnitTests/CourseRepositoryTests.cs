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

        #endregion


    }
}
