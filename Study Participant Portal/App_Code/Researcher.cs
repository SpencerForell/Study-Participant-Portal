using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Researcher
/// </summary>
public class Researcher: SuperUser {
    
    /// <summary>
    /// Constructor that builds a researcher object by passing in all of the attributes
    /// </summary>
    public Researcher(int user_id, string user_name, string first_name, string last_name, string email) {
        type = UserType.Researcher;
        this.userID = user_id;
        this.userName = user_name;
        this.firstName = first_name;
        this.lastName = last_name;
        this.email = email;
	}

    /// <summary>
    /// Constructor that runs a query to populate all fields of a Researcher object
    /// </summary>
    /// <param name="user_id">The id of the researcher to create an object for</param>
    public Researcher(int user_id) {
        string queryString = "select User_Name, First_Name, Last_Name, Email from Researcher where Res_ID = " + user_id.ToString();
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

        this.userID = user_id;
        this.userName = query.Results[0][0];
        this.firstName = query.Results[0][1];
        this.lastName = query.Results[0][2];
        this.email = query.Results[0][3];
    }
}