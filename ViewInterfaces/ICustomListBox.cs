using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public interface ICustomListBox
    {
        void ClearAllItems();
        void RemoveSelection();
        void SetSelectionAt(int index);
        void RemoveSelectedItem();
        int ItemsCount
        {
            get;
        }
    }
}
