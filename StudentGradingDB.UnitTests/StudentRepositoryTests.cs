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
   public class StudentRepositoryTests : TestHelper
    {
        #region Repository

        private StudentRepository studentRepository;

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

            //setup the student repository
            studentRepository = new StudentRepository(Context);

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
        /// Test to insert a single student into the database
        /// </summary>
        [TestMethod]
        public void AddStudent()
        {
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
           


        /// <summary>
        /// Test to update a single student into the database
        /// </summary>
        [TestMethod]
        public void UpdateStudent()
        {
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
            
        /// <summary>
        /// Test to delete a single student from the database
        /// </summary>
        [TestMethod]
        public void DeleteStudent()
        {
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
           
        }


        #endregion


        #region Other Query Tests

        #endregion
    }

