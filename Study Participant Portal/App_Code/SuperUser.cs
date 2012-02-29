    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Parent of both child and researcher so we can reuse code from here
/// </summary>
public abstract class SuperUser {

    // Protected variables
    protected UserType type;
    protected int userID;    
    protected string userName;
    protected string firstName;
    protected string lastName;
    protected string email;

    // public properties
    public UserType Type {
        get { return type; }
    }

    public int UserID {
        get { return userID; }
    }

    public string UserName {
        get { return userName; }
    }

    public string FirstName {
        get { return firstName; }
    }

    public string LastName {
        get { return lastName; }
    }
    public string Email {
        get { return email; }
    }

    public enum UserType {
        Researcher,
        Participant
    }
        
    public SuperUser() {
		//
		// TODO: Add constructor logic here
		//
	}
}