using Discord;
using Discord.WebSocket;
using NoteToSelf.Core.Attributes;
using System;
using System.Threading.Tasks;

namespace NoteToSelf.Events
{
    [PreInitialize]
    public class ConsoleLogEvent
    {
        public ConsoleLogEvent(DiscordShardedClient client)
            => client.Log += Client_Log;

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}
