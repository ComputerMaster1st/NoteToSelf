using Discord;
using Discord.WebSocket;
using NoteToSelf.Core.Config;
using System;
using System.Threading.Tasks;

namespace NoteToSelf
{
    class Program
    {
        private readonly DiscordShardedClient _client = new(
                new DiscordSocketConfig()
                {
                    DefaultRetryMode = RetryMode.AlwaysRetry,
                    LogLevel = LogSeverity.Info,
                    AlwaysDownloadUsers = true
                }
            );

        private Config _config = null;
        private BootLoader _bootLoader = null;

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {
            _config = await Config.LoadAsync();
            if (_config == null)
            {
                Console.WriteLine("Config file has been generated! Please fill in the gaps inside the \"{0}\"!", Config.Filename);
                return;
            }

            Console.WriteLine("Starting Note to Self Bot v2.0 ...");

            _bootLoader = new(_config, _client);

            await _bootLoader.StartAsync();

            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync($"{_config.Prefix}help");

            await Task.Delay(-1);
        }
    }
}
