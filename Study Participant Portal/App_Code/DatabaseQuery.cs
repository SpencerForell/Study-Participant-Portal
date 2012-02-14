using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Data connection class
/// The results of the query are stored in the variable "results"
/// </summary>
public class DatabaseQuery {

    private string item;

    private List<string> record = new List<string>();

    private List<List<string>> results = new List<List<string>>();

    public enum Type {
        Select,
        Insert,
        Delete
    }

    public List<List<string>> Results {
        get { return results; }
    }

    /// <summary>
    /// Constructor to run queries from the database
    /// </summary>
    /// <param name="queryString">This should be an actual sql query like "select * from table where id = 1"</param>
	public DatabaseQuery(String queryString, Type type) {
        string MyConString = @"SERVER=mysql.eecs.oregonstate.edu;
                                DATABASE=cs462-team34;
                                UID=cs462-team34;
                                PASSWORD=u4tL9v4cEDjrMjnW;";
        MySqlConnection connection = new MySqlConnection(MyConString);
        MySqlCommand command = connection.CreateCommand();
        MySqlDataReader Reader;
        command.CommandText = queryString;
        connection.Open();

        switch (type) {
            /*
             * The select statement populates a list of a list of strings.
             */
            case Type.Select:
                Reader = command.ExecuteReader();
                while (Reader.Read())         {
                    for (int i = 0; i < Reader.FieldCount; i++) {
                        item = Reader.GetValue(i).ToString();
                        record.Add(item);                
                    }
                    results.Add(record);
                }
                break;
            case Type.Insert:
                command.ExecuteNonQuery();
                break;

            case Type.Delete:
                command.ExecuteNonQuery();
                break;
        
        }
        
        connection.Close();
	}
}