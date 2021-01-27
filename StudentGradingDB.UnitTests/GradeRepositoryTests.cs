using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentGradingDB.UnitTests
{
    /// <summary>
    /// Unit test class for Grade Repository
    /// </summary>
    [TestClass]
    public class GradeRepositoryTests : TestHelper
    {
        #region Repository

        /// <summary>
        /// Teacher repository 
        /// </summary>
        private GradeRepository gradeRepository;

        /// <summary>
        /// Student repository
        /// </summary>
        private StudentRepository studentRepository;

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

            //setup the grade repository
            gradeRepository = new GradeRepository(Context);
            studentRepository = new StudentRepository(Context);
            courseRepository = new CourseRepository(Context);

        }


        /// <summary>
        /// Runs after each test
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            //close the connection
            Context.Connection.Close();
        }

        #endregion

        #region Add, Update, Delete Tests

        /// <summary>
        /// Test to insert a grade into the database with a student and a course
        /// </summary>
        [TestMethod]
        public void AddGrade()
        {

            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();

            
            //create a new grade
            var newGrade = new Grade()
            {
                Student = newStudent,
                Course = newCourse,
                LetterGrade = "A+"
            };

            //add the grade
            gradeRepository.Add(newGrade);



            //find the grade
            var foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId
               
                                                  ).FirstOrDefault();
            //test to see if we found the grade
            Assert.IsNotNull(foundGrade);

            //check are all the values the same
            Assert.AreEqual(foundGrade.Course.CourseId,foundCourse[0].CourseId);
            Assert.AreEqual(foundGrade.Student.StudentId,foundStudent[0].StudentId);
            Assert.AreEqual(foundGrade.LetterGrade, newGrade.LetterGrade);

        }

        /// <summary>
        /// Test to update a student grade in the database
        /// </summary>
        [TestMethod]
        public void UpdateGrade()
        {

            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();


            //create a new grade
            var newGrade = new Grade()
            {
                Student = newStudent,
                Course = newCourse,
                LetterGrade = "A+"
            };

            //add the grade
            gradeRepository.Add(newGrade);



            //find the grade
            var foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId

                                                  ).FirstOrDefault();
            //test to see if we found the grade
            Assert.IsNotNull(foundGrade);

            //check are all the values the same
            Assert.AreEqual(foundGrade.Course.CourseId, foundCourse[0].CourseId);
            Assert.AreEqual(foundGrade.Student.StudentId, foundStudent[0].StudentId);
            Assert.AreEqual(foundGrade.LetterGrade, newGrade.LetterGrade);

            //now update the grade
            foundGrade.LetterGrade = "D+";
            gradeRepository.Update(foundGrade);

            //find the updated
            foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId

                                                  ).FirstOrDefault();
            //test to see if we found the grade
            Assert.IsNotNull(foundGrade);

            //check are all the values the same
            Assert.AreEqual(foundGrade.Course.CourseId, foundCourse[0].CourseId);
            Assert.AreEqual(foundGrade.Student.StudentId, foundStudent[0].StudentId);
            Assert.AreEqual(foundGrade.LetterGrade, newGrade.LetterGrade);

        }



        /// <summary>
        /// Test to delete a grade for a student from the database
        /// </summary>
        [TestMethod]
        public void DeleteGrade()
        {
            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();


            //create a new grade
            var newGrade = new Grade()
            {
                Student = newStudent,
                Course = newCourse,
                LetterGrade = "A+"
            };

            //add the grade
            gradeRepository.Add(newGrade);

            //find the grade
            var foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId

                                                  ).FirstOrDefault();
            //test to see if we found the grade
            Assert.IsNotNull(foundGrade);

            //delete it 
            gradeRepository.Remove(newGrade);

            foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId

                                                  ).FirstOrDefault();
            
            //after deleting found grade should be null
            Assert.IsNull(foundGrade);
        }

        #endregion

        #region Other Query Tests

        #region AssignGradeToStudent Tests

        /// <summary>
        /// Test to assign a grade to a student when the student and course are not found
        /// </summary>
        [TestMethod]
        public void AssignGradeToStudent_StudentAndCourseNotFound_ReturnsFalse()
        {
            var result = false;

            result = gradeRepository.AssignGradeToStudent(1, 1, "A+");

            Assert.IsFalse(result);
        }


        /// <summary>
        /// Test to assign a grade to a student when the student is not found and course is found
        /// </summary>
        [TestMethod]
        public void AssignGradeToStudent_StudentNotFoundAndCourseFound_ReturnsFalse()
        {
            var result = false;

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();

            result = gradeRepository.AssignGradeToStudent(1, foundCourse[0].CourseId, "A+");

            Assert.IsFalse(result);
        }


        /// <summary>
        /// Test to assign a grade to a student when the student is found and course is not found
        /// </summary>
        [TestMethod]
        public void AssignGradeToStudent_StudentFoundAndCourseNotFound_ReturnsFalse()
        {
            var result = false;

            //create a course
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add the course to the database
            studentRepository.Add(newStudent);

            //find the student
            var foundStudent = studentRepository.GetAll().ToList();

            result = gradeRepository.AssignGradeToStudent(foundStudent[0].StudentId,4, "A+");

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test method to assign a grade to a student when student and course are found
        /// </summary>
        [TestMethod]
        public void AssignGradeToStudent_ReturnsTrue()
        {

            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();

            var result = gradeRepository.AssignGradeToStudent(foundStudent[0].StudentId, foundCourse[0].CourseId, "A+");

            //should add the return true
            Assert.IsTrue(result);

        }


        /// <summary>
        /// Test method to assign a grade to a student when student and course are found and a grade is already assigned should update the grade and return true
        /// </summary>
        [TestMethod]
        public void AssignGradeToStudent_UpdateExistingGradeRecord_ReturnsTrue()
        {

            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();

            var result = gradeRepository.AssignGradeToStudent(foundStudent[0].StudentId, foundCourse[0].CourseId, "A+");

            //should add the return true
            Assert.IsTrue(result);

            //find the grade
            var foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId

                                                  ).FirstOrDefault();
            //test to see if we found the grade
            Assert.IsNotNull(foundGrade);

            //check are all the values the same
            Assert.AreEqual(foundGrade.Course.CourseId, foundCourse[0].CourseId);
            Assert.AreEqual(foundGrade.Student.StudentId, foundStudent[0].StudentId);
            Assert.AreEqual(foundGrade.LetterGrade, "A+");

            //Update the grade should still return true and grade should be A-
            result = gradeRepository.AssignGradeToStudent(foundStudent[0].StudentId, foundCourse[0].CourseId, "A-");

            //should add the return true
            Assert.IsTrue(result);

            //find the grade
            foundGrade = gradeRepository.Find(grade => grade.Student.StudentId == foundStudent[0].StudentId
                                                  && grade.Course.CourseId == foundCourse[0].CourseId

                                                  ).FirstOrDefault();

            //check are all the values the same
            Assert.AreEqual(foundGrade.Course.CourseId, foundCourse[0].CourseId);
            Assert.AreEqual(foundGrade.Student.StudentId, foundStudent[0].StudentId);
            Assert.AreEqual(foundGrade.LetterGrade, "A-");

        }

        #endregion


         #region DoesGradeExist Tests

        /// <summary>
        /// Test to see if a grade record exists for a student
        /// </summary>
        [TestMethod]
        public void DoesGradeExist_StudentAndCourseNotFound_ReturnsFalse()
        {
            var result = gradeRepository.DoesGradeExist(1, 1);

            //should return false
            Assert.IsFalse(result);
        }



        /// <summary>
        /// Test to see if a grade record exists for a student that is found and course is not found
        /// </summary>
        [TestMethod]
        public void DoesGradeExist_StudentFoundAndCourseNotFound_ReturnsFalse()
        {
            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            var result = gradeRepository.DoesGradeExist(foundStudent[0].StudentId, 1);

            //should return false
            Assert.IsFalse(result);
        }


        /// <summary>
        /// Test to see if a grade record exists for a student that is not found and course is found
        /// </summary>
        [TestMethod]
        public void DoesGradeExist_StudentNotFoundAndCourseFound_ReturnsFalse()
        {
            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();


            var result = gradeRepository.DoesGradeExist(1, foundCourse[0].CourseId);

            //should return false
            Assert.IsFalse(result);
        }


        /// <summary>
        /// Test to see if a grade record exists for a student when record already exists in grade table
        /// </summary>
        [TestMethod]
        public void DoesGradeExist_ReturnsTrue()
        {

            //create the student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };


            //add the student
            studentRepository.Add(newStudent);

            //Find the student
            var foundStudent = studentRepository.GetAll().ToList();

            //create a course
            var newCourse = new Course()
            {
                Name = "Introduction To C# Programming",
                Description = "Introduction To C# Programming"
            };

            //add the course to the database
            courseRepository.Add(newCourse);

            //find the course
            var foundCourse = courseRepository.GetAll().ToList();

            var result = gradeRepository.AssignGradeToStudent(foundStudent[0].StudentId, foundCourse[0].CourseId, "A+");

            //should add the record when calling AssignGradeToStudent and return true
            Assert.IsTrue(result);

            result = gradeRepository.DoesGradeExist(foundStudent[0].StudentId, foundCourse[0].CourseId);

            //should return true as a grade record does exist
            Assert.IsTrue(result);
        }
        #endregion

        #endregion

    }
}
