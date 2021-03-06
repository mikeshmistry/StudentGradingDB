﻿using DatabaseContext;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    /// <summary>
    /// Teacher repository class that handles common queries
    /// </summary>
    public class TeacherRepository : Repository<Teacher>
    {

        #region Constructors 

        /// <summary>
        /// Constructor to take in any context 
        /// </summary>
        /// <param name="context">The context object for the database</param>
        public TeacherRepository(DbContext context) : base(context)
        {

        }

        #endregion

        #region Other Queries

        /// <summary>
        ///Query to get all the courses for the given teacher 
        /// </summary>
        /// <param name="id">the id of the teacher</param>
        /// <returns>A list of courses null if not found</returns>
        public List<Course> GetAssignedCourses(int id)
        {
            List<Course> teacherCourses = null;

            var courses = Context.Set<Teacher>()
                          .Where(c => c.TeacherId == id)
                          .Include(c => c.Courses)
                          .AsNoTracking()
                          .FirstOrDefault();


            if (courses != null)
                teacherCourses = courses.Courses.ToList();


            return teacherCourses;

        }



        /// <summary>
        /// Method to delete a teacher from the database
        /// </summary>
        /// <param name="teacherId">The id of the teacher to delete</param>
        public void DeleteTeacher(int teacherId)
        {
            var teacher = Context.Set<Teacher>()
                         .Where(teacher => teacher.TeacherId == teacherId)
                         .Include(teacher => teacher.Courses)
                         .FirstOrDefault();

            if (teacher != null)
                Remove(teacher);
        }


        #endregion
    }
}
