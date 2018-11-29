using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace LexiGameDB_Access
{
    public abstract class BaseGateway
    {
        private string _conString;
        public string ConString
        {
            get
            {
                return _conString;
            }
            set
            {
                _conString = value;
            }

        }
        public BaseGateway(string conString)
        {
            this._conString = conString;
        }
        private OleDbConnection con;
        public void Connect(OleDbCommand cmd)
        {
            string s=Environment.CurrentDirectory;
            con = new OleDbConnection(ConString);
            try
            {
                cmd.Connection = con;
                con.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to open connection to DB", ex);
            }
        }

        public void Close()
        {
            try
            {
                con.Close();
            }
            catch
            {
            }
        }
        protected virtual void ExecuteNonQuery(OleDbCommand com, bool close)
        {
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execure non-Query", ex);
            }
            finally
            {
                if (close)
                {
                    this.Close();
                }
            }

        }
        protected virtual IDataReader ExecuteReader(OleDbCommand com, bool close)
        {
            try
            {
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute Reader", ex);
            }
            finally
            {
                if (close)
                {
                    this.Close();
                }
            }
        }
        protected virtual object ExecuteScalar(OleDbCommand com, bool close)
        {
            try
            {
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execure Reader", ex);
            }
            finally
            {
                if (close)
                {
                    this.Close();
                }
            }
        }
    }
}
