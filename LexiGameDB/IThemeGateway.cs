using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;

namespace LexiGame.DB
{
    public interface IThemeGateway
    {
        int AddTheme(Theme theme);
        void DeleteTheme(int id);
        void DeleteAllThemes();
        void UpdateTheme(Theme theme);
        Dictionary<int, Theme> GetThemes();
        List<Theme> GetThemesList();
    }
}
