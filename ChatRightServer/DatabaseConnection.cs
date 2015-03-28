using System;
using System.Data;
using System.Data.SqlClient;

namespace ChatRightServer
{
    internal class DatabaseConnection
    {
        private string sql_string;
        private string strCon;
        private SqlDataAdapter dataAdapter_1;

        public string Sql
        {
            set { sql_string = value; }
        }

        public string connection_string
        {
            set { strCon = value; }
        }

        public DataSet GetConnection
        {
            get { return MyDataSet(); }
        }

        private DataSet MyDataSet()
        {
            DataSet dataSet = new DataSet();

            SqlConnection con = new SqlConnection(strCon);
            con.Open();

            dataAdapter_1 = new SqlDataAdapter(sql_string, con);
            dataAdapter_1.Fill(dataSet, "Table_Data_1");

            con.Close();
            return dataSet;
        }

        public void UpdateDatabase(DataSet ds)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter_1);
            commandBuilder.DataAdapter.Update(ds.Tables[0]);
        }
    }
}