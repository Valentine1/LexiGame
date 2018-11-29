using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace LexiGame.View
{
    
    public interface ILeximListBox : ICustomListBox
    {
        void AddDisplayLexim( LeximDTView lexim, PlayAudio play);
        void UpdateLexim(LeximDTView leximDT);
        void InsertLexim(int index, LeximDTView leximDT, PlayAudio play);
        LeximDTView SelectedLexim
        {
            get;
        }

    }
}
