/** ApplicationContext.cs
 * 
 * This context class is what connects the server, ASP.NET Core, with the 
 * database, SQL Server Express (since I don't remember paying for SQL 
 * Server).
 * This uses the Entity Framework Core (since it is .NET Core) to create
 * tables specified by the models used in the application (and specified
 * by including a DbSet for that model). It also takes care of creating the
 * necessary relationships between these models (and tables).
 * The controllers use this context class to communicate with the database,
 * and this can be seen whenever the `_context` variable is used in those
 * files - that variable refers to this context class.
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
