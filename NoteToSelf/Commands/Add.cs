using Discord;
using Discord.Commands;
using NoteToSelf.Core;
using NoteToSelf.Database.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("add")]
    public class Add : NtsModuleBase
    {
        [Command]
        public async Task ExecuteAsync([Remainder] string note)
        {
            if (note.Length > 1800)
            {
                await ReplyAsync(embed: SimpleEmbed("Notes can only be 1800 characters maximum.", false));
                return;
            }

            var user = await Context.Database.Users.FirstOrDefaultAsync(x => x.Id == Context.User.Id);

            if (user is null)
            {
                user = new UserProfile(Context.User.Id);
                Context.Database.Users.Add(user);
            }

            user.Notes.Add(new Note(note));

            await ReplyAsync(embed: SimpleEmbed(string.Format("New Self Note Created: {0}", Format.BlockQuote(note))));
        }
    }
}
