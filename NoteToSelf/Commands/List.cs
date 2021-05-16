using Discord;
using Discord.Commands;
using NoteToSelf.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("list")]
    public class List : NtsModuleBase
    {
        [Command]
        public async Task ExecuteAsync()
        {
            var user = await Context.Database.Users.FirstOrDefaultAsync(x => x.Id == Context.User.Id);

            if (user is null || user.Notes.Count < 1)
            {
                await ReplyAsync(embed: SimpleEmbed("You have no self notes!", false));
                return;
            }

            var builders = new List<EmbedBuilder>();
            var builder = new EmbedBuilder()
            {
                Title = "Your Self Notes!",
                Color = Color.Blue
            };
            var charCount = 0;
            var charLimit = 1750;

            foreach (var note in user.Notes)
            {
                if (charCount > charLimit)
                {
                    builders.Add(builder);
                    builder = new() { Color = Color.Blue };
                    charCount = 0;
                }

                var fieldName = $"ID: {note.Id}";
                charCount += fieldName.Length + note.Text.Length;
                builder.AddField(fieldName, note.Text);
            }

            builders.Add(builder);

            foreach (var embed in builders)
                await ReplyAsync(embed: embed.Build());
        }
    }
}
