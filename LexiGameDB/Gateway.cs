using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.Utility;

namespace LexiGame.DB
{
    public static class Gateway
    {
        private static IDBFactory dbFactory
        {
            get
            {
                return FactoryMaker.MakeDBFactory();
            }
        }
        public static ILexemGateway LexemGateway
        {
            get
            {
                return dbFactory.MakeLexemGateway(Settings.UserSettings.ConnectionStringName);
            }
        }
        public static IThemeGateway ThemeGateway
        {
            get
            {
                return dbFactory.MakeThemeGateway(Settings.UserSettings.ConnectionStringName);
            }
        }
    }
}
