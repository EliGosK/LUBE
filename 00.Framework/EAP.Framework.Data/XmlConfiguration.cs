using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EAP.Framework.Data
{
    public class XmlConfiguration
    {
        public void SetValue(string configName, object configvalue)
        {
            if (configvalue == null)
            {
                return;
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings[configName] != null)
            {
                config.AppSettings.Settings.Remove(configName);
            }

            config.AppSettings.Settings.Add(configName, configvalue.ToString());
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string GetValue(string ConfigName)
        {
            return ConfigurationManager.AppSettings[ConfigName];
        }
    }
}
