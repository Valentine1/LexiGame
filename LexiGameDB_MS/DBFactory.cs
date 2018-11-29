using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.DB;

namespace LexiGameDB_MS
{
    public class DBFactory :IDBFactory
    {

        public ILexemGateway MakeLexemGateway(string connectionStr)
        {
            return new LexemGateway(connectionStr);
        }

        public IThemeGateway MakeThemeGateway(string connectionStr)
        {
            throw new NotImplementedException();
        }

    }
}
