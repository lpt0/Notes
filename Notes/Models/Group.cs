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
        public string Name { get; set; }

        /// <summary>
        /// An optional field, to describe this group and the notes within it.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Notes associated with this group.
        /// </summary>
        public List<Note> Notes { get; set; }
    }
}
