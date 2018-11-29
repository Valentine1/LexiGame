using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.DB
{
    public interface IDBFactory
    {
        IThemeGateway MakeThemeGateway(string connectionStr);
        ILexemGateway MakeLexemGateway(string connectionStr);
    }
}
