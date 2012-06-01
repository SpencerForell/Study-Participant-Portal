using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

/// <summary>
/// Data connection class
/// The results of the query are stored in the variable "results"
/// </summary>
public class DatabaseQuery {

    private string item;
    private int lastInsertID; //Used for insert queries to get back the auto incremement value
    private List<string> record = null;
    private List<List<string>> results = new List<List<string>>();

    //Possible types of queries
    public enum Type {
        Select,
        Insert,
        Update,
        Delete
    }

    public List<List<string>> Results {
        get { return results; }
    }

    public int LastInsertID {
        get { return lastInsertID; }
    }

    /// <summary>
    /// Constructor that sets up and runs queries from the database.
    /// </summary>
    /// <param name="queryString">This should be an actual sql query like "select * from table where id = 1"</param>
	public DatabaseQuery(String queryString, Type type) {

        System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Study Participant Portal");
        System.Configuration.ConnectionStringSettings connString;
        connString = config.ConnectionStrings.ConnectionStrings["ApplicationServices"];
        System.Configuration.KeyValueConfigurationElement setting = config.AppSettings.Settings["userName"];

        MySqlConnection connection = new MySqlConnection(connString.ToString());
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
                while (Reader.Read()) {
                    record = new List<string>();
                    for (int i = 0; i < Reader.FieldCount; i++) {
                        item = Reader.GetValue(i).ToString();
                        record.Add(item);                
                    }
                    results.Add(record);
                }
                break;
            case Type.Insert:
                command.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                command.CommandText = "select Last_Insert_ID()";
                MySqlDataReader Reader2 = command.ExecuteReader();
                if (Reader2.Read()) {
                    lastInsertID = Convert.ToInt32(Reader2.GetValue(0));
                }
                break;
            case Type.Update:
                command.ExecuteNonQuery();
                break;
            case Type.Delete:
                command.ExecuteNonQuery();
                break;
        }

        //test code
        
        connection.Close();
        
	}

    
}