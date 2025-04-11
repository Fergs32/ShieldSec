using ShieldSec.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShieldSec.Core.Managers
{
    /// <summary>
    ///  This class is responsible for managing the app settings.
    /// </summary>
    public static class SettingsManager
    {
        private static readonly string SettingsPath = "appsettings.json";
        private static AppSettings _settings;
        /// <summary>
        ///  App settings property.
        /// </summary>
        public static AppSettings Settings
        {
            get
            {
                if (_settings == null) LoadSettings();
                return _settings;
            }
        }
        /// <summary>
        ///  Loads the settings from the appsettings.json file.
        /// </summary>
        public static void LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    var json = File.ReadAllText(SettingsPath);
                    // we need to deserialize the json string into an object so we can use it
                    _settings = JsonSerializer.Deserialize<AppSettings>(json);
                }
                else
                {
                    _settings = new AppSettings();
                    SaveSettings();
                }
            }
            catch
            {
                _settings = new AppSettings();
            }
        }
        /// <summary>
        ///  Saves the settings to the appsettings.json file.
        /// </summary>
        public static void SaveSettings()
        {
            // if we dont write indented, the json will be minified and hard to read
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Settings, options);
            File.WriteAllText(SettingsPath, json);
        }
    }
}
