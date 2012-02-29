using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TODO: complete Class summary
/// </summary>
public class Participant: SuperUser {
    
    /// <summary>
    /// Constructor that builds a researcher object by passing in all of the attributes
    /// </summary>
    public Participant(string user_name, string first_name, string last_name, string email) {
        type = UserType.Participant;
        this.userName = user_name;
        this.firstName = first_name;
        this.lastName = last_name;
        this.email = email;
    }

     /// <summary>
    /// Constructor that runs a query to populate all fields of a Participant object
    /// </summary>
    /// <param name="user_id">The id of the researcher to create an object for</param>
    public Participant(int user_id) {
        //do query and set all fields to query results
        //note*** need to make sure the user_id exists
    }
}