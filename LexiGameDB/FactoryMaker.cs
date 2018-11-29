using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using LexiGame.Utility;
namespace LexiGame.DB
{
    internal static class FactoryMaker
    {
        private static IDBFactory dbFactory;
        public static IDBFactory MakeDBFactory()
        {
            if (dbFactory == null)
            {
                for (int i = 0; i < Settings.UserSettings.DBProviders.Count; i++)
                {
                    if (Settings.UserSettings.DBProviders[i].Name == Settings.UserSettings.ProviderName)
                    {
                        Assembly asm = Assembly.Load(Settings.UserSettings.DBProviders[i].Assembly);
                        dbFactory = (IDBFactory)asm.CreateInstance(Settings.UserSettings.DBProviders[i].FactoryClassName);
                    }
                }
            }
            if (dbFactory == null)
            {
                throw new Exception("dbProviderName " + Settings.UserSettings.ProviderName + "is misspelled or has not been added");
            }
            return dbFactory;
        }
    }
}
