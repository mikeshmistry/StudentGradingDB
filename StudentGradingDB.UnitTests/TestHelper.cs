﻿using DatabaseContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace StudentGradingDB.UnitTests
{
    /// <summary>
    ///  Helper class to help with common test configurations options
    /// </summary>
  public abstract class TestHelper 
    {
        #region Properties

        /// <summary>
        /// Property to set the SQLite test database
        /// </summary>
        protected SqliteConnection Connection { get; set; }

        /// <summary>
        /// Property to set the context of the database and open it
        /// </summary>
        protected StudentGradingContext Context { get; set; }

        #endregion

        /// <summary>
        ///  Initializes the connection, context and other setup procedures 
        /// </summary>
       protected void SetupConnectionAndContext()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            
            //create the options
            var options = new DbContextOptionsBuilder<StudentGradingContext>()
                       .UseSqlite(Connection)
                       .Options;

            //set the context
            Context = new StudentGradingContext(options);

            //Opens the connection
            Connection.Open();

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

        }

    }
}
