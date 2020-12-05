/** Group.cs
 * 
 * TODO
 * 
 * Author: Haran
 * Date: December 2nd, 2020
 */
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class Group
    {
        /// <summary>
        /// The group's ID.
        /// Also the primary key in the table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The group's name.
        /// </summary>
        [Required]
        [StringLength(60, ErrorMessage = "The name can only have 60 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// An optional field, to describe this group and the notes within it.
        /// </summary>
        [StringLength(255, ErrorMessage = "The description can only have 255 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Notes associated with this group.
        /// </summary>
        public List<Note> Notes { get; set; }
    }
}
