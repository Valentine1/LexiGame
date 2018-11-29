using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public delegate void ThemeSelectionChanged(ThemeDTView themeDT);
    public delegate void LeximSelectionChanged(LeximDTView leximDT);
    public delegate void NewTheme();
    public delegate void SaveTheme(ThemeDTView themeDT);
    public delegate void DeleteTheme(int id);
    public delegate void NewLexim();
    public delegate void SaveLexim(LeximDTView leximDT);
    public delegate void DeleteLexim(int id);

    public interface IThemeView : IView
    {
        IThemeListBox ThemeListBox
        {
            get;
        }
        ILeximListBox LeximListBox
        {
            get;
        }
        event ThemeSelectionChanged OnThemeSelectionChanged;
        event LeximSelectionChanged OnLeximSelectionChanged;
        event NewTheme OnNewTheme;
        event SaveTheme OnSaveTheme;
        event DeleteTheme OnDeleteTheme;
        event NewLexim OnNewLexim;
        event SaveLexim OnSaveLexim;
        event DeleteLexim OnDeleteLexim;
        void DisplayTheme(ThemeDTView themeDT);
        void DisplayLexim(LeximDTView lexim, PlayAudio play);
    }
}
