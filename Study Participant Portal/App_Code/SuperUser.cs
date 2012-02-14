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
    protected int user_ID; //not sure if this should be userID or user_ID    
    protected string user;
    protected string email;

    // public properties
    public UserType Type {
        get { return type; }
    }

    public int User_ID {
        get { return user_ID; }
    }

    public string User {
        get { return user; }
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