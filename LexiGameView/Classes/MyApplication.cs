using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace LexiGame.View
{
    public class MyApplication : Application, IApplication
    {
        private Dictionary<string, ResourceDictionary> _resources;
        public Dictionary<string, ResourceDictionary> MyResources
        {
            get
            {
                if (_resources == null)
                {
                    _resources = new Dictionary<string, ResourceDictionary>();
                }
                return _resources;
            }
        }
        public MyApplication()
        {
            SetResources();
        }
        public int RunApplication(IView view)
        {
            return this.Run((Window)view);
        }
        public void SetResources()
        {
            ResourceDictionary resLang = new ResourceDictionary();
            resLang.Source = new Uri("Resources;component/" + Utility.Settings.UserSettings.Profile.Language + ".xaml", UriKind.Relative);
            string Skin = Utility.Settings.UserSettings.Profile.Appearance;
            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary sliderStyle = new ResourceDictionary();
            sliderStyle.Source = new Uri("Resources;component/Themes/" + Skin + "/Slider.xaml", UriKind.Relative);
            ResourceDictionary menu = new ResourceDictionary();
            menu.Source = new Uri("Resources;component/Themes/" + Skin + "/Menu.xaml", UriKind.Relative);
            ResourceDictionary toolBar = new ResourceDictionary();
            toolBar.Source = new Uri("Resources;component/Themes/" + Skin + "/ToolBar.xaml", UriKind.Relative);
            ResourceDictionary main = new ResourceDictionary();
            main.Source = new Uri("Resources;component/Themes/" + Skin + "/Main.xaml", UriKind.Relative);

            ResourceDictionary listBox = new ResourceDictionary();
            listBox.Source = new Uri("Resources;component/Themes/" + Skin + "/ListBox.xaml", UriKind.Relative);

            ResourceDictionary padAnBall = new ResourceDictionary();
            padAnBall.Source = new Uri("Resources;component/Themes/" + Skin + "/PadAndBall.xaml", UriKind.Relative);

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(sliderStyle);
            Application.Current.Resources.MergedDictionaries.Add(resLang);
            Application.Current.Resources.MergedDictionaries.Add(menu);
            Application.Current.Resources.MergedDictionaries.Add(toolBar);
            Application.Current.Resources.MergedDictionaries.Add(main);
            Application.Current.Resources.MergedDictionaries.Add(listBox);
            Application.Current.Resources.MergedDictionaries.Add(padAnBall);
            this.MyResources.Clear();
            this.MyResources.Add("resPadAnBall", padAnBall);
            this.MyResources.Add("resLeximListBox", listBox);
            this.MyResources.Add("resToolBar", toolBar);
            this.MyResources.Add("resSliderStyle", sliderStyle);
        }

    }
}
