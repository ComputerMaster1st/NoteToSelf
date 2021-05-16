using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace NoteToSelf.Commands
{
    [Group("ping")]
    public class Ping : ModuleBase
    {
        private readonly DiscordShardedClient _client;

        public Ping(DiscordShardedClient client)
            => _client = client;

        [Command]
        public Task ExecuteAsync()
            => ReplyAsync(string.Format("Ping To Discord: {0}", _client.Latency));
    }
}
