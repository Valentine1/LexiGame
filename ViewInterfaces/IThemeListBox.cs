using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public interface IThemeListBox : ICustomListBox
    {
        void AddDisplayTheme(ThemeDTView themeDT);
        void UpdateTheme(ThemeDTView themeDT);
        void InsertTheme(int index, ThemeDTView themeDT);
        ThemeDTView SelectedTheme
        {
            get;
        }
    }
}
