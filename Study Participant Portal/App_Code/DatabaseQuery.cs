using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Data connection class
/// The results of the query are stored in the variable "results"
/// </summary>
public class DatabaseQuery
{
    public string[] results;

    /// <summary>
    /// Constructor to run queries from the database
    /// </summary>
    /// <param name="queryString">This should be an actual sql query like "select * from table where id = 1"</param>
	public DatabaseQuery(String queryString)
	{
        string MyConString = "SERVER=mysql.eecs.oregonstate.edu;" +
                "DATABASE=cs462-team34;" +
                "UID=cs462-team34;" +
                "PASSWORD=u4tL9v4cEDjrMjnW;";
        MySqlConnection connection = new MySqlConnection(MyConString);
        MySqlCommand command = connection.CreateCommand();
        MySqlDataReader Reader;
        command.CommandText = queryString;
        connection.Open();
        Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            string thisrow = "";
            for (int i = 0; i < Reader.FieldCount; i++) {
                thisrow += Reader.GetValue(i).ToString() + ",";
                this.results[i] = Reader.GetValue(i).ToString();
                //listBox1.Items.Add(thisrow);
            }
            String o = thisrow;
            
            
        }
        connection.Close();
	}
}