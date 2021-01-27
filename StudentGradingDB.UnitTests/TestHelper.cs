using DatabaseContext;
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
        /// Property to set the context of the database and open it
        /// </summary>
        protected StudentGradingTestContext Context { get; set; }

        #endregion

        /// <summary>
        ///  Initializes the connection, context and other setup procedures 
        /// </summary>
       protected void SetupConnectionAndContext()
        {
          
            
            //set the context
            Context = new StudentGradingTestContext();

            //Opens the connection
            Context.Connection.Open(); ;

            //Delete the In Memory Database for testing
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

        }

    }
}
