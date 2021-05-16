using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteToSelf.Database.Models
{
    public class UserProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong Id { get; private set; }

        public virtual List<Note> Notes { get; private set; } = new List<Note>();

        public UserProfile(ulong id)
            => Id = id;
    }
}
