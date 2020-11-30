using DatabaseContext;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;

namespace StudentGradingDB.UnitTests
{
    /// <summary>
    /// Unit test class to test student repository class
    /// </summary>
    [TestClass]
    class StudentRepositoryTests
    {

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
                var context = new StudentGradingContext();
                var studentRepository = new StudentRepository(context);

                var newStudent = new Student()
                {
                    FirstName = "Mikesh",
                    LastName = "Mistry"
                };

            }
            finally 
            {

                //close the connection
                connection.Close();
            }
        }

    }
}
