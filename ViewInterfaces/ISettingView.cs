using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public delegate void OKclick(ThemeDTView themeDT, int range, int step, string skin, string language);
    public delegate void ResourceChanged();
    public interface ISettingView : IView
    {
        void AddTheme(ThemeDTView themeDT);
        void SetSelectedThemeIndex(int i);
        void InitRange(int rangeBegin,int rangeEnd, int index);
        void InitStep(int range, int index);
        void InitSkins(List<string> skins, int index);
        void InitLanguages(List<string> languages, int index);
        event OKclick OnOKclick;
        event ResourceChanged OnResourceChanged;
    }
}
