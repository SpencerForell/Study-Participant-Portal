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
    protected int userID; //not sure if this should be userID or user_ID    
    protected string user;
    protected string email;

    // public properties
    public UserType Type {
        get { return type; }
        set { type = value; }
    }

    public int UserID {
        get { return userID; }
        set { userID = value; }
    }

    public string User {
        get { return user; }
        set { user = value; }
    }
         
    public string Email {
        get { return email; }
        set { email = value; }
    }

    public enum UserType {
        Researcher,
        Participant
    }

    public UserType getType() {
        return type;
    }
    
    public SuperUser() {
		//
		// TODO: Add constructor logic here
		//
	}
}