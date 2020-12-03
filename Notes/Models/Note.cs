/** Note.cs
 * 
 * TODO
 * 
 * Author: Haran
 * Date: December 2nd, 2020
 */
// needed to add metadata, such as whether an attribute is required
using System.ComponentModel.DataAnnotations;

// needed to be able to add the foreign key annotation
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    public class Note
    {
        /// <summary>
        /// The note's identifier.
        /// Also the primary key in the table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The note's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The contents of the note.
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// An optional, brief description of this note.
        /// </summary>
        public string Description { get; set; }

        /* Having a separate property for the group ID (instead of a 
         * "shadow foreign key", or one that is automatically generated)
         * means that, when the controller for this model is generated, it 
         * will automatically generate a dropdown list with available groups.
         * This means that a user may select a group to associate the note 
         * with. */
        [ForeignKey("Group")]
        public int? GroupId { get; set; }

        /// <summary>
        /// The group associated with this note, if any.
        /// </summary>
        public Group Group { get; set; }
    }
}
