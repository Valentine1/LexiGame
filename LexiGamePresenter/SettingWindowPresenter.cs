using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;
using LexiGame.View;
using LexiGame.DB;
using LexiGame.Utility;

namespace LexiGame.Presenter
{
   
    public class SettingWindowPresenter : BasePresenter
    {
        private ISettingView _settingView;
        public ISettingView SettingView
        {
            get
            {
                return _settingView;
            }
            set
            {
                _settingView = value;
            }
        }
        private IThemeGateway _themeGateway;
        private IThemeGateway ThemeGateway
        {
            get
            {
                if (_themeGateway == null)
                {
                    _themeGateway = Gateway.ThemeGateway;
                }
                return _themeGateway;
            }
        }
        public event ResourceChanged OnResourceChanged;
        public SettingWindowPresenter(ISettingView settingView)
        {
            _settingView = settingView;
            InitSettingView();
            this.SettingView.OnOKclick +=SettingView_OnOKclick;
            this.SettingView.OnResourceChanged += new ResourceChanged(SettingView_OnResourceChanged);
            
        }

        void SettingView_OnResourceChanged()
        {
            if (OnResourceChanged != null)
            {
                OnResourceChanged();
            }
        }
        public void ShowDialog()
        {
            SettingView.ShowDialog();
        }
        void SettingView_OnOKclick(ThemeDTView themeDT, int range, int step, string skin, string language)
        {
            try
            {
                string theme = themeDT == null ? string.Empty : themeDT.Name;
                Utility.Settings.UserSettings.Profile.ThemeSelected = theme;
                Utility.Settings.UserSettings.Profile.Range = range;
                Utility.Settings.UserSettings.Profile.Step = step;
                Utility.Settings.UserSettings.Profile.Appearance = skin;
                Utility.Settings.UserSettings.Profile.Language = language;
                Utility.Settings.Save();

              
            }
            catch (Exception ex)
            {
                this.HandleException("Could not to save settings", ex);
            }

        }
        private void InitSettingView()
        {
            this.InitThemes();
            this.InitRange();
            this.InitStep();
            this.InitSkins();
            this.InitLanguages();
        }
        private void InitThemes()
        {
            List<Theme> themeList = ThemeGateway.GetThemesList();
            int index = -1;
            for (int i = 0; i < themeList.Count; i++)
            {
                if (themeList[i].Name == Utility.Settings.UserSettings.Profile.ThemeSelected)
                {
                    index = i;
                }
                this.SettingView.AddTheme(new ThemeDTView(themeList[i].ID, themeList[i].Name));
            }
            this.SettingView.SetSelectedThemeIndex(index);
        }
        private void InitRange()
        {
            try
            {
                int range = Utility.Settings.UserSettings.Profile.Range;
                range = range > 5 ? 5 : range;
                range = range < 3 ? 3 : range;
                this.SettingView.InitRange(3,5, range);
            }
            catch (Exception ex)
            {
                this.HandleException("", ex);
                this.SettingView.InitRange(3,5, 5);
            }
        }
        private void InitStep()
        {
            try
            {
                int step = Utility.Settings.UserSettings.Profile.Step;
                step = step > 3 ? 3 : step;
                step = step < 1 ? 1 : step;
                this.SettingView.InitStep(3, step);
            }
            catch (Exception ex)
            {
                this.HandleException("", ex);
                this.SettingView.InitStep(3, 1);
            }
        }
        private void InitSkins()
        {
            SkinCollection skins = Utility.Settings.UserSettings.Skins;
            List<string> skinList=new List<string>();
            int index = -1;
            for (int i = 0; i < skins.Count; i++)
            {
                skinList.Add(skins[i].Name);
                if (skins[i].Name == Utility.Settings.UserSettings.Profile.Appearance)
                {
                    index = i;
                }
            }
            this.SettingView.InitSkins(skinList, index);
        }
        private void InitLanguages()
        {
            LanguageCollection languages = Utility.Settings.UserSettings.Languages;
            List<string> languageList = new List<string>();
            int index = -1;
            for (int i = 0; i < languages.Count; i++)
            {
                languageList.Add(languages[i].Name);
                if (languages[i].Name == Utility.Settings.UserSettings.Profile.Language)
                {
                    index = i;
                }
            }
            this.SettingView.InitLanguages(languageList, index);
        }
    }
}
