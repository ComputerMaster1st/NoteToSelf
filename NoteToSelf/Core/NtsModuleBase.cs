using Discord;
using Discord.Commands;

namespace NoteToSelf.Core
{
    public class NtsModuleBase : ModuleBase<NtsCommandContext>
    {
        public Embed SimpleEmbed(string message, bool success = true)
        {
            var builder = new EmbedBuilder()
            {
                Title = "Note to Self!",
                Color = success ? Color.Green : Color.Red,
                Description = message
            };

            return builder.Build();
        }
    }
}
