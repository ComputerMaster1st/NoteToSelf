using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteToSelf.Database.Models
{
    public class Note
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public string Text { get; set; }

        public Note(string text)
            => Text = text;
    }
}
