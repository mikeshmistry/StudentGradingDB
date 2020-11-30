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

        #endregion
    }
}
