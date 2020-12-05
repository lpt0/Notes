/** User.cs
 * 
 * TODO
 * 
 * Author: Haran
 * Date: December 4th, 2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class User
    {
        /// <summary>
        /// The user's ID.
        /// Also the primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's full username.
        /// Must have at least 3 characters, with a maximum of 20 characters.
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must have at least 3 characters, but less than 20.")]
        public string Username { get; set; }

        /// <summary>
        /// Notes created by this user.
        /// </summary>
        public List<Note> Notes { get; set; }
    }
}
