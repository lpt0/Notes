/** User.cs
 * 
 * This model defines the properties of a user of this website.
 * 
 * Author: Haran
 * Date: December 4th, 2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

/* Needed to inherit IdentityUser, which has everything needed for 
 * identity framework logging in/registering */
using Microsoft.AspNetCore.Identity;

namespace Notes.Models
{
    public class User : IdentityUser
    {
        // Inherits ID from IdentityUser

        // IdentityUser's "UserName" property is used instead (w

        /// <summary>
        /// Notes created by this user.
        /// </summary>
        public List<Note> Notes { get; set; }
    }
}
