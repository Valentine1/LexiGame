using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;
using LexiGame.View;

namespace LexiGame.Presenter
{
    public class MainWindowPresenter
    {
        private WindowViewLoader _loader;
        private WindowViewLoader Loader
        {
            get
            {
                if (_loader == null)
                {
                    _loader = new WindowViewLoader();
                }

                return _loader;
            }
            set
            {
                _loader = value;
            }
        }
        private IMainView mainView;
        public IMainView MainView
        {
            get
            {
                return mainView;
            }
            set
            {
                mainView = value;
            }
        }
        public MainWindowPresenter()
        {
            this.mainView = Loader.LoadMainWindow();
            Initialize();
        }
        private void Initialize()
        {
            MainView.OnGameStarted += MainView_OnGameStarted;
            MainView.OnThemesShown +=MainView_OnThemesShown;
            this.MainView.OnSettingsShown += MainView_OnSettingsShown;
        }

        void MainView_OnSettingsShown()
        {
            ISettingView settingView= this.Loader.LoadSettingWindow();
            settingView.ViewOwner = this.MainView;
            SettingWindowPresenter settingWinPresener = new SettingWindowPresenter(settingView);
            settingWinPresener.OnResourceChanged += new ResourceChanged(settingWinPresener_OnResourceChanged);
            settingWinPresener.ShowDialog();
        }

        void settingWinPresener_OnResourceChanged()
        {
            this.MainView.SetDynamicResources();
        }

        void MainView_OnThemesShown()
        {
            this.MainView.ClearOwnedWindows();
            IThemeView themeView = this.Loader.LoadThemeWindow();
            themeView.ViewOwner = this.MainView;
            themeView.Show();
            ThemeWindowPresenter themeWindowPresenter = new ThemeWindowPresenter(themeView);
            themeWindowPresenter.DisplayThemes();
        }

        void MainView_OnGameStarted()
        {
            System.GC.Collect();
            this.MainView.ClearOwnedWindows();
            IGameView gameView = Loader.LoadGameWindow();
            gameView.ViewOwner = this.MainView;
            gameView.Show();
            GameWindowPresenter gameWindowPresenter = new GameWindowPresenter(gameView);
            gameWindowPresenter.StartGame();
        }

    }
}
