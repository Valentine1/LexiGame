using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class Themes
    {
        private static Dictionary<int, Theme> _themeDictionary;
        public static Dictionary<int, Theme> ThemeDictionary
        {
            get
            {
                if (_themeDictionary == null)
                {
                    _themeDictionary = new Dictionary<int, Theme>();
                }
                return _themeDictionary;
            }
            set
            {
                _themeDictionary = value;
            }

        }
        private static List<Theme> _themeCollection;
        public static List<Theme> ThemeCollection
        {
            get
            {
                if (_themeCollection == null)
                {
                    _themeCollection = new List<Theme>(Themes.ThemeDictionary.Values);
                }
                return _themeCollection;
            }
        }

        public static void Clear()
        {
            Themes.ThemeDictionary.Clear();
            Themes.ThemeCollection.Clear();
            Themes._themeDictionary = null;
            Themes._themeCollection = null;
        }
        public static void ClearCollection()
        {
            Themes.ThemeCollection.Clear();
            Themes._themeCollection = null;
        }
        public static Theme MapThemeById(int id)
        {
            Theme theme = (Theme)ThemeDictionary[id];
            if (theme == null)
                throw new Exception("No Theme with requested id exists.");
            return theme;
        }
    }
}
