using Discord;
using Discord.Commands;
using NoteToSelf.Core;
using System.Linq;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("edit")]
    public class Edit : NtsModuleBase
    {
        [Command]
        public async Task ExecuteAsync(int id, [Remainder] string edit)
        {
            var user = await Context.Database.Users.FirstOrDefaultAsync(x => x.Id == Context.User.Id);

            if (user is null || user.Notes.Count < 1)
            {
                await ReplyAsync(embed: SimpleEmbed("You have no self notes!", false));
                return;
            }

            var note = user.Notes.FirstOrDefault(x => x.Id == id);

            if (note is null)
            {
                await ReplyAsync(embed: SimpleEmbed("The note Id you specified does not exist! Please double-check & try again.", false));
                return;
            }

            if (edit.Length > 1800)
            {
                await ReplyAsync(embed: SimpleEmbed("Notes can only be 1800 characters maximum.", false));
                return;
            }

            note.Text = edit;

            await ReplyAsync(embed: SimpleEmbed(string.Format("Self Note (ID: {0}) Updated: {1}", Format.Bold(id.ToString()), Format.BlockQuote(edit))));
        }
    }
}
