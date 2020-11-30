using DatabaseContext;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentGradingDB.UnitTests
{
    /// <summary>
    /// Unit test class to test student repository class
    /// </summary>
    [TestClass]
   public class StudentRepositoryTests
    {

        #region Add, Update, Delete Tests

        /// <summary>
        /// Test to insert a single student into the database
        /// </summary>
        [TestMethod]
        public void AddStudent()
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

                //create the student repository 
                var studentRepository = new StudentRepository(context);

                //create the new student
                var newStudent = new Student()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

                //add the student to the database
                studentRepository.Add(newStudent);

                //get all the students to a list
                var allStudents = studentRepository.GetAll().ToList();

                //only one student 
                Assert.AreEqual(1, allStudents.Count());

                //FirstName are equal
                Assert.AreEqual(newStudent.FirstName, allStudents[0].FirstName);

                //LastName are equal
                Assert.AreEqual(newStudent.LastName, allStudents[0].LastName);
            }
            finally 
            {

                //close the connection
                connection.Close();
            }
        }


        /// <summary>
        /// Test to update a single student into the database
        /// </summary>
        [TestMethod]
        public void UpdateStudent()
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

                //create the student repository 
                var studentRepository = new StudentRepository(context);

                //create the new student
                var newStudent = new Student()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

                //add the student to the database
                studentRepository.Add(newStudent);

                //get all the students to a list should only be one
                var student = studentRepository.GetAll().ToList();

                //only one student 
                Assert.AreEqual(1, student.Count());

                //FirstName are equal
                Assert.AreEqual(newStudent.FirstName, student[0].FirstName);

                //LastName are equal
                Assert.AreEqual(newStudent.LastName, student[0].LastName);

                //Change the student first and last name
                var updatedStudent = student[0];

                updatedStudent.FirstName = "John";
                updatedStudent.FirstName = "Smith";

                //update the student
                studentRepository.Update(updatedStudent);

                //get the updated student by the id
                var changedStudent = studentRepository.GetId(updatedStudent.StudentId);

                //Updated FirstName are equal
                Assert.AreEqual(updatedStudent.FirstName, changedStudent.FirstName);

                //Updated LastName are equal
                Assert.AreEqual(updatedStudent.LastName, changedStudent.LastName);


            }
            finally
            {

                //close the connection
                connection.Close();
            }
        }

        /// <summary>
        /// Test to delete a single student from the database
        /// </summary>
        [TestMethod]
        public void DeleteStudent()
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

                //create the student repository 
                var studentRepository = new StudentRepository(context);

                //create the new student
                var newStudent = new Student()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

                //add the student to the database
                studentRepository.Add(newStudent);

                //get all the students to a list should only be one
                var student = studentRepository.GetAll().ToList();

                //only one student 
                Assert.AreEqual(1, student.Count());

                //FirstName are equal
                Assert.AreEqual(newStudent.FirstName, student[0].FirstName);

                //LastName are equal
                Assert.AreEqual(newStudent.LastName, student[0].LastName);

                //Remove the student
                studentRepository.Remove(newStudent);

                var studentListAfterRemoved = studentRepository.GetAll();

                //count should be zero
                Assert.AreEqual(0, studentListAfterRemoved.Count());


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
