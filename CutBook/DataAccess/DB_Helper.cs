using System.Data;
using System.Data.OleDb;

namespace CutBook.DataAccess
{
    public class DB_Helper
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter dataAdapter;
        OleDbTransaction transaction; //פיקוח על יותר משינוי אחד במוסד נתונים 
        public DB_Helper()
        {
            this.connection = new OleDbConnection();
            this.command = new OleDbCommand();
            this.command.Connection = this.connection;
            this.connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{Directory.GetCurrentDirectory()}\CutBook1.accdb'";
        }
        public void OpenConnection()
        {
            if(this.connection.State != ConnectionState.Open)
            {
                this.connection.Open();
            }
        }
        public void CloseConnection()
        {
            if(this.connection.State != ConnectionState.Closed)
            {
                this.connection.Close();
            }
        }
        public int ChangeDb(string sql)//insert, update, delete
        {
            this.command.CommandText = sql;
            return this.command.ExecuteNonQuery();
        }
        public DataTable GetDataTable(string sql, string tableName)
        {
            if(this.dataAdapter == null)
            {
                this.dataAdapter = new OleDbDataAdapter();
            }
            this.command.CommandText = sql;
            this.dataAdapter.SelectCommand = this.command;
            DataTable dataTable = new DataTable(tableName);
            this.dataAdapter.Fill(dataTable);
            return dataTable;
        }
        public void OpenTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
            this.command.Transaction = this.transaction;
        }
        public void CommitTransaction()
        {
            this.transaction.Commit();
        }
        public void RollbackTransaction()
        {
            this.transaction.Rollback();
        }
    }
}
