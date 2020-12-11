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

// needed to inherit DbContext, DbSet. DbContextOptions
using Microsoft.EntityFrameworkCore;

// needed to inherit IdentityDbContext
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Notes.Models
{
    /* Inherit from DbContext, which is part of the Microsoft Entity 
     * Framework, which is an Object-Relational mapper (maps objects to 
     * database tables, and handles creating, retrieving, updating, and 
     * deleting objects in the database.) 
     * By specifing a type for the IdentityDbContext (using <User>), the 
     * default IdentityUser can be overriden with my custom user class, which
     * links to the Notes table.
     */
    public class ApplicationContext : IdentityDbContext<User>
    {
        // Constructor for this class; calls the DbContext constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        /* link to the User class
         * Since User inherits from IdentityUser, and my custom User class
         * was specified for the IdentityDbContext, it is fine to override
         * the base Users property.
         */
        public override DbSet<User> Users { get; set; }
        //public override DbSet<User> Users { get; set; } //TODO: can I override it?

        //TODO
        //public DbSet<UserNote> UserNotes { get; set; }

        /* This handles linking the note to the database (since its the only 
         * reference to the Note class */
        public DbSet<Note> Notes { get; set; }

        // And this handles linking the groups
        public DbSet<Group> Groups { get; set; }
    }
}
