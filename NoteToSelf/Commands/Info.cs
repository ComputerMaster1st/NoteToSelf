using Discord;
using Discord.Commands;
using NoteToSelf.Core;
using System.Linq;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("info")]
    public class Info : ModuleBase<NtsCommandContext>
    {
        [Command]
        public async Task ExecuteAsync()
        {
            var builder = new EmbedBuilder()
            {
                Title = "Note to Self Info!",
                Color = Color.Blue
            };
            var guilds = await Context.Client.GetGuildsAsync();

            var usersWithNotes = Context.Database.Users.Count();
            var noteCount = 0;

            foreach (var user in Context.Database.Users)
                noteCount += user.Notes.Count;

            builder.AddField("Bot Statistics:", "Number of Guilds, Channels & Users...")
                .AddField("Guilds: ", guilds.Count, true)
                .AddField("Note Statistics:", "Number of Notes...")
                .AddField("Users With Notes: ", usersWithNotes, true)
                .AddField("Total Notes: ", noteCount, true);

            await ReplyAsync(embed: builder.Build());
        }
    }
}
