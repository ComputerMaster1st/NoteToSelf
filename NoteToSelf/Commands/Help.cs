using Discord;
using Discord.Commands;
using NoteToSelf.Core.Config;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("help")]
    public class Help : ModuleBase
    {
        private readonly Config _config;

        public Help(Config config)
            => _config = config;

        [Command]
        public Task ExecuteAsync()
        {
            var builder = new EmbedBuilder
            {
                Title = "Note to Self Help!",
                Color = Color.Blue,
                Description = Format.Bold("Note to You... Remember to memorize all these commands! This is all I do! You can contact the developer at https://discord.gg/Czw5ffx")
            };

            builder.AddField($"{_config.Prefix} add <note>", "Enter your note into the note parameter to create it.")
                .AddField($"{_config.Prefix} list", "List all notes you have. Warning, this might spam many embeds if you have so many notes!")
                .AddField($"{_config.Prefix} edit <note id> <note>", "Edit the specified note by ID with the new note.")
                .AddField($"{_config.Prefix} delete <note id>", "Delete this note by ID.")
                .AddField($"{_config.Prefix} info", "Get bot statistics. Nothing more, nothing less.")
                .AddField($"{_config.Prefix} invite", "Get bot invite.")
                .AddField($"{_config.Prefix} help", "You are dumb if you want to know what this does.");

            return ReplyAsync(embed: builder.Build());
        }
    }
}
