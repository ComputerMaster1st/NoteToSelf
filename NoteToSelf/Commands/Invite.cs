using Discord.Commands;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("invite")]
    public class Invite : ModuleBase
    {
        [Command]
        public Task ExecuteAsync()
            => ReplyAsync("https://discordapp.com/oauth2/authorize?client_id=350385352986591235&scope=bot");
    }
}
