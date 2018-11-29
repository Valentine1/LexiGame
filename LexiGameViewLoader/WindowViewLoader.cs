using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public class WindowViewLoader : IViewLoader
    {
        public IMainView LoadMainWindow()
        {
            MainWindow mainWin = new MainWindow();
            return (IMainView)mainWin;
        }
        private GameWin _gameWin;
        public IGameView LoadGameWindow()
        {
            //if(_gameWin==null)
            _gameWin = new GameWin();
            GameWin gameWin = _gameWin;
            return (IGameView)gameWin;
        }
        public IThemeView LoadThemeWindow()
        {
            ThemeWin themeWin = new ThemeWin();
            return (IThemeView)themeWin;
        }
        public ISettingView LoadSettingWindow()
        {
            SettingWin settingWin = new SettingWin();
            return (ISettingView)settingWin;
        }


    }
}
