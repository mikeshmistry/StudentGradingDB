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
    /// Unit test class to test teacher repository class
    /// </summary>
    [TestClass]
   public class TeacherRepositoryTests
    {
        #region Add, Update, Delete Tests

        /// <summary>
        /// Test to insert a single teacher into the database
        /// </summary>
        [TestMethod]
        public void AddTeacher()
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

                //create the teacher repository 
                var teacherRepository = new TeacherRepository(context);

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

            finally
            {

                //close the connection
                connection.Close();
            }
        }


        /// <summary>
        /// Test to update a single teacher into the database
        /// </summary>
        [TestMethod]
        public void UpdateTeacher()
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

                //create the teacher repository 
                var teacherRepository = new TeacherRepository(context);

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
            finally
            {

                //close the connection
                connection.Close();
            }
        }

        /// <summary>
        /// Test to delete a single teacher from the database
        /// </summary>
        [TestMethod]
        public void DeleteTeacher()
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

                //create the teacher repository 
                var teacherRepository = new TeacherRepository(context);

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
