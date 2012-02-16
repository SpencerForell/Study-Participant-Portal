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
    protected int user_id;    
    protected string user_name;
    protected string first_name;
    protected string last_name;
    protected string email;

    // public properties
    public UserType Type {
        get { return type; }
    }

    public int User_id {
        get { return user_id; }
    }

    public string User_Name {
        get { return user_name; }
    }

    public string First_name {
        get { return first_name; }
    }

    public string Last_Name {
        get { return last_name; }
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