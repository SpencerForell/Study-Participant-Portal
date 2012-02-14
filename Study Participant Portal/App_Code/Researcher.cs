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
    public Researcher(string user_name, string first_name, string last_name, string email) {
        type = UserType.Researcher;
        this.user_name = user_name;
        this.first_name = first_name;
        this.last_name = last_name;
        this.email = email;
	}

    /// <summary>
    /// Constructor that runs a query to populate all fields of a Researcher object
    /// </summary>
    /// <param name="user_id">The id of the researcher to create an object for</param>
    public Researcher(int user_id) {
        //do query and set all fields to query results
        //note*** need to make sure the user_id exists
    }
}