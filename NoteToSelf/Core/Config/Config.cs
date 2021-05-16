using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace NoteToSelf.Core.Config
{
    public class Config
    {
        [JsonIgnore]
        public const string Filename = "config.json";

        [JsonProperty]
        public string Token { get; private set; } = "DISCORD TOKEN";

        [JsonProperty]
        public string Prefix { get; set; } = "nts!";

        [JsonConstructor]
        private Config() { }

        /// <summary>
        /// Load Bot Configuration
        /// </summary>
        /// <returns>Config On Success. Null if not found.</returns>
        public static async Task<Config> LoadAsync()
        {
            // Check config exists
            if (!File.Exists(Filename))
            {
                var config = new Config();

                await config.SaveAsync();

                return null;
            }

            // Load config
            var json = await File.ReadAllTextAsync(Filename);

            return JsonConvert.DeserializeObject<Config>(json);
        }

        public Task SaveAsync()
            => File.WriteAllTextAsync(Filename, JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}
