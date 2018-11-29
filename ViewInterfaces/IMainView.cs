using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public interface IMainView:IView
    {
        event GameStarted OnGameStarted;
        event ThemesShown OnThemesShown;
        event SettingsShown OnSettingsShown;
        void ClearOwnedWindows();
    }

    public delegate void GameStarted();
    public delegate void ThemesShown();
    public delegate void SettingsShown();
}
