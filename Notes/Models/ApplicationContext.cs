/** ApplicationContext.cs
 * 
 * TODO
 * 
 * Author: Haran
 * Date: December 2nd, 2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// needed to inherit DbContext
using Microsoft.EntityFrameworkCore;

namespace Notes.Models
{
    /* Inherit from DbContext, which is part of the Microsoft Entity 
     * Framework, which is an Object-Relational mapper (maps objects to 
     * database tables, and handles creating, retrieving, updating, and 
     * deleting objects in the database.) */
    public class ApplicationContext : DbContext
    {
        // Constructor for this class; calls the DbContext constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        // link to the User class
        public DbSet<User> Users { get; set; }

        /* This handles linking the note to the database (since its the only 
         * reference to the Note class */
        public DbSet<Note> Notes { get; set; }

        // And this handles linking the groups
        public DbSet<Group> Groups { get; set; }
    }
}
