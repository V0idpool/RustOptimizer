using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustOptimizer.Core
{
    /// <summary>
    /// This class handles reading and writing the game's client.cfg file,
    /// making it easier to load and save settings without messing up the file format.
    /// </summary>
    public class RustConfig
    {
        private Dictionary<string, string> settings = new Dictionary<string, string>();
        /// <summary>
        /// Loads the settings from the client.cfg file into the app's memory.
        /// It's smart enough to skip blank lines and comments.
        /// </summary>
        public void LoadSettings(string filePath)
        {
            if (File.Exists(filePath))
            {
                foreach (string line in File.ReadAllLines(filePath))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    {
                        continue;
                    }

                    string[] parts = line.Split(new char[] { ' ' }, 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim().Trim('"');
                        settings[key] = value;
                    }
                }
            }
        }
        /// <summary>
        /// Saves all the settings currently in the app's memory back to the client.cfg file.
        /// It also makes a quick backup of the old file just in case.
        /// </summary>
        public void SaveSettings(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Copy(filePath, filePath + ".bak", true);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in settings)
                {
                    writer.WriteLine($"{kvp.Key} \"{kvp.Value}\"");
                }
            }
        }
        /// <summary>
        /// Tries to get a specific setting from the loaded settings.
        /// </summary>
        public bool GetSetting(string key, out string value)
        {
            return settings.TryGetValue(key, out value);
        }
        /// <summary>
        /// Updates a setting with a new value. If the setting doesn't exist, it adds it.
        /// </summary>
        public void SetSetting(string key, string value)
        {
            settings[key] = value;
        }
    }
}
