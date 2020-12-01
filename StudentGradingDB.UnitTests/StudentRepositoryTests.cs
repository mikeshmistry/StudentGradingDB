using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System.Collections.Generic;
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

        #endregion

        #region Other Query Tests

        #region GetEnrolledCourses Tests

        /// <summary>
        /// Test to get enrolled courses should return null for a student that is not found
        /// </summary>
        [TestMethod]
        public void GetEnrolledCourses_StudentNotFound_ReturnsNull()
        {
            var coursesList = studentRepository.GetEnrolledCourses(1223);

            //should return a null course list
            Assert.IsNull(coursesList);
        }

        [TestMethod]
        public void GetEnrolledCourse_StudentFound_ReturnsCourses()
        {
            //create the new student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add the student to the database
            studentRepository.Add(newStudent);

            //add some courses
            var courseRepository = new CourseRepository(Context);

            var courseList = new List<Course>();

            // add a list of courses
            courseList.Add(new Course { Name = "Introduction to C#", Description = "Introduces students to C# programming" });
            courseList.Add(new Course { Name = "Introduction to Java", Description = "Introduces students to Java programming" });

            //add the course using add range
            courseRepository.AddRange(courseList);

            //use the find method to find a student 
            var foundstudent = studentRepository.Find(student => student.FirstName == "Mikesh" && student.LastName == "Mistry").FirstOrDefault();

            //found the student
            if (foundstudent != null)
            {
                //enroll the student into the two course
                foreach (var course in courseList)
                {
                    courseRepository.EnrollStudentIntoCourse(foundstudent.StudentId, course.CourseId);
                }


                //get the found student
                var studentCourseList = studentRepository.GetId(foundstudent.StudentId);

                if (studentCourseList != null)
                {
                    //the count of the courseList used to add the courses to the student
                    var courseListCount = courseList.Count();
                    var studentEnrolledCoursesCount = studentCourseList.Courses.Count();

                    //insure the counts are the same
                    Assert.AreEqual(courseListCount, studentEnrolledCoursesCount);

                    //insure that ids are the same
                    Assert.AreEqual(foundstudent.StudentId, studentCourseList.StudentId);


                }


            }

        }

        #endregion

        #region GetGrades

        /// <summary>
        /// Test to get grades for a student that does not exist
        /// </summary>
        [TestMethod]
        public void GetGrades_StudentNotFound_ReturnsNull()
        {
            //get the grades for a student that does not exist
            var studentGrades = studentRepository.GetGrades(1234);

            //should be a null list
            Assert.IsNull(studentGrades);
        }


        [TestMethod]
        public void GetGrades_StudentFound_ReturnsGrades()
        {
            //create the new student
            var newStudent = new Student()
            {
                FirstName = "Mikesh",
                LastName = "Mistry"
            };

            //add the student to the database
            studentRepository.Add(newStudent);

            //add some courses
            var courseRepository = new CourseRepository(Context);

            var courseList = new List<Course>();

            // add a list of courses
            courseList.Add(new Course { Name = "Introduction to C#", Description = "Introduces students to C# programming" });
            courseList.Add(new Course { Name = "Introduction to Java", Description = "Introduces students to Java programming" });

            //add the course using add range
            courseRepository.AddRange(courseList);

            //use the find method to find a student 
            var foundstudent = studentRepository.Find(student => student.FirstName == "Mikesh" && student.LastName == "Mistry").FirstOrDefault();




            //found the student
            if (foundstudent != null)
            {
                //enroll the student into the two course
                foreach (var course in courseList)
                {
                    courseRepository.EnrollStudentIntoCourse(foundstudent.StudentId, course.CourseId);
                }


                //get the found student
                var studentCourseList = studentRepository.GetId(foundstudent.StudentId);

                if (studentCourseList != null)
                {
                    //check to see if the grade was added
                    var gradeAdded = false;

                    //assign a student a grade
                    var gradeRepository = new GradeRepository(Context);

                    foreach (var course in studentCourseList.Courses)
                    {
                        gradeAdded = gradeRepository.AssignGradeToStudent(studentCourseList.StudentId, course.CourseId, "A+");
                        Assert.IsTrue(gradeAdded);
                    }

                
                    Assert.AreEqual(2, studentCourseList.Grades.Count());
                }


            }




        }

        #endregion



        #endregion
    }
}

