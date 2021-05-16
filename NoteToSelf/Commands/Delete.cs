using Discord;
using Discord.Commands;
using NoteToSelf.Core;
using System.Linq;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("delete")]
    public class Delete : NtsModuleBase
    {
        [Command]
        public async Task ExecuteAsync(int id)
        {
            var user = await Context.Database.Users.FirstOrDefaultAsync(x => x.Id == Context.User.Id);

            if (user is null || user.Notes.Count < 1)
            {
                await ReplyAsync(embed: SimpleEmbed("You have no self notes!", false));
                return;
            }

            if (!user.Notes.Any(x => x.Id == id))
            {
                await ReplyAsync(embed: SimpleEmbed("The note Id you specified does not exist! Please double-check & try again.", false));
                return;
            }

            user.Notes.RemoveAll(x => x.Id == id);

            await ReplyAsync(embed: SimpleEmbed(string.Format("Note (ID: {0}) has been deleted!{1}", Format.Bold(id.ToString()), user.Notes.Count < 1 ? " You no longer have any notes!" : "")));
        }
    }
}
