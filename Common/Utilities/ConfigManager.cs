using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public sealed class ConfigManager
    {

        #region Fields

        private static readonly object _lockObject = new object();
        private static ConfigManager _configManager = null;
        private IConfiguration _configuration;

        #endregion

        #region Properties

        public static ConfigManager Instance
        {
            get
            {
                lock (_lockObject)
                {
                    return _configManager ?? new ConfigManager();
                }
            }
        }

        #endregion

        #region Constructors

        public ConfigManager()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                            .Build();
        }

        #endregion

        #region Methods

        public string GetConnectionString(string connectionName = "AppConnection")
        {
            return _configuration.GetConnectionString(connectionName);
        }

        public string GetKeyValue(params string[] items)
        {
            string key = string.Join(':', items);
            return _configuration.GetSection($"{key}").Value;
            

        }

        public async Task<string> GetKeyValueAsync(params string[] items)
        {
            return await Task.Run(() =>
            {
                string key = string.Join(':', items);
                return _configuration.GetSection($"{key}").Value;
            });
        }

        #endregion
    }
}
