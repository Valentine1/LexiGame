using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LexiGame.Utility
{
    public static class Settings
    {
        private static Configuration conf;
        private static UserSection userSettings;
        public static UserSection UserSettings
        {
            get
            {
                if (userSettings == null)
                {
                    //conf = ConfigurationManager.OpenExeConfiguration("TestUnit.dll");
                    conf = ConfigurationManager.OpenExeConfiguration("LexiGame.exe");
                    userSettings = (UserSection)conf.GetSection("userSection");
                }
                if (userSettings == null)
                    throw new Exception("Failed to load UserSection.");
                return userSettings;
            }
        }
       static public void Save()
        {
            conf.Save();
        }

    }
}
