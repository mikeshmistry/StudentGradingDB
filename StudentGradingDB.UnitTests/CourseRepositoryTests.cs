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
  public  class CourseRepositoryTests
  {

        #region Add, Update, Delete Tests

        /// <summary>
        /// Test to insert a single course into the database
        /// </summary>
        [TestMethod]
        public void AddCourse()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");

            //open the connection 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<StudentGradingContext>()
                       .UseSqlite(connection)
                       .Options;

                //create the context class
                var context = new StudentGradingContext(options);


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //create the course repository 
                var courseRepository = new CourseRepository(context);

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
            finally
            {

                //close the connection
                connection.Close();
            }
        }


        /// <summary>
        /// Test to update a single course into the database
        /// </summary>
        [TestMethod]
        public void UpdateCourse()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");

            //open the connection 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<StudentGradingContext>()
                       .UseSqlite(connection)
                       .Options;

                //create the context class
                var context = new StudentGradingContext(options);


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //create the course repository 
                var courseRepository = new CourseRepository(context);

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

            finally
            {

                //close the connection
                connection.Close();
            }
        }


        /// <summary>
        /// Test to delete a single course from the database
        /// </summary>
        [TestMethod]
        public void DeleteCourse()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");

            //open the connection 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<StudentGradingContext>()
                       .UseSqlite(connection)
                       .Options;

                //create the context class
                var context = new StudentGradingContext(options);


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //create the course repository 
                var courseRepository = new CourseRepository(context);

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
            finally
            {

                //close the connection
                connection.Close();
            }
        }


        #endregion

        #region Other Query Tests





        #endregion


    }
}
