using MainApp.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;

namespace MainApp.Common
{
    public class AppConfigManager
    {

        private readonly ILogger<AppConfigManager> _logger;
        public AppConfigManager(ILogger<AppConfigManager> logger)
        {
            _logger = logger;
        }
        const string CONFIG_FILE = "config.json";

        public AppConfig Config { get; private set; } = new AppConfig();

        public void Load()
        {

            if (File.Exists(CONFIG_FILE))
            {
                _logger.LogDebug("找到了配置文件");
                Config = JsonConvert.DeserializeObject<Model.AppConfig>(File.ReadAllText(CONFIG_FILE)) ?? new Model.AppConfig();
            }
            else
            {
                _logger.LogWarning("无配置文件");

                Config = new AppConfig();
            }
        }

        public void Save()
        {
            File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(Config));
        }
    }
}
