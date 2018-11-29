using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.Presenter
{
    public abstract class BasePresenter
    {
        protected virtual void HandleException(string message,Exception ex)
        {
            string mes = message+" "+ex.Message;
            if (ex.InnerException != null)
            {
                mes += ex.InnerException.Message;
            }
            System.Windows.MessageBox.Show(mes);
        }
    }
}
