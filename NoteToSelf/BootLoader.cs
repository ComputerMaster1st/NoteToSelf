using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoteToSelf.Core.Attributes;
using NoteToSelf.Core.Config;
using NoteToSelf.Database;
using NoteToSelf.Events;
using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;

namespace NoteToSelf
{
    public class BootLoader
    {
        private readonly Config _config;
        private readonly DiscordShardedClient _client;

        private readonly CommandService _commands = new(
                new()
                {
                    CaseSensitiveCommands = false,
                    DefaultRunMode = RunMode.Sync
                }
            );

        private IServiceProvider _services = null;

        public BootLoader(Config config, DiscordShardedClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task StartAsync()
        {
            #region Core Dependencies

            var collection = new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton(_client)
                .AddSingleton<IDiscordClient>(_client)
                .AddSingleton(_commands)
                .AddDbContext<DatabaseContext>(options => {
                    options.UseSqlite(new SqliteConnectionStringBuilder("Data Source=database.db")
                    {
                        Mode = SqliteOpenMode.ReadWriteCreate
                    }.ToString())
                    .EnableSensitiveDataLogging()
                    .UseLazyLoadingProxies();
                }, ServiceLifetime.Transient);

            #endregion

            #region Event Dependencies

            collection.AddSingleton<ConsoleLogEvent>()
                .AddSingleton<CommandEvent>();

            #endregion

            _services = collection.BuildServiceProvider();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            PreInitialize(collection);
        }

        private void PreInitialize(IEnumerable collection)
        {
            foreach (ServiceDescriptor service in collection)
            {
                if (service.ServiceType.GetCustomAttribute<PreInitialize>() == null)
                    continue;

                if (service.ImplementationType == null)
                    continue;

                _services.GetService(service.ImplementationType);
            }
        }
    }
}