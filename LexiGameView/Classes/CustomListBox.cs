using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LexiGame.View
{
    internal abstract class CustomListBox : ListBox, ICustomListBox
    {
        public virtual void ClearAllItems()
        {
            this.Items.Clear();
        }

        public virtual void RemoveSelectedItem()
        {
            if (this.SelectedItem != null)
            {
                int index = this.SelectedIndex;
                this.Items.RemoveAt(this.SelectedIndex);
                this.SelectedIndex = index > this.Items.Count - 1 ? this.Items.Count - 1 : index;
            }
        }

        public virtual void RemoveSelection()
        {
            if (this.SelectedItem != null)
            {
                ((ListBoxItem)this.SelectedItem).IsSelected = false;
            }
        }

        public virtual void SetSelectionAt(int index)
        {

            if (this.Items.Count > index)
            {
                this.SelectedIndex = index;
            }
        }

        public int ItemsCount
        {
            get
            {
                return this.Items.Count;
            }
        }
    }
}
