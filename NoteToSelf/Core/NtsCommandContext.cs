using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using NoteToSelf.Database;
using System;

namespace NoteToSelf.Core
{
    public class NtsCommandContext : CommandContext
    {
        private readonly IServiceProvider _services;

        private DatabaseContext _database = null;

        public DatabaseContext Database
        {
            get
            {
                if (_database is null)
                    _database = _services.GetService<DatabaseContext>();

                return _database;
            }
        }

        public NtsCommandContext(IDiscordClient client, IUserMessage msg, IServiceProvider services) : base(client, msg)
            => _services = services;
    }
}
