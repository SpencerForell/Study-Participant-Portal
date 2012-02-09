using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Parent of both child and researcher so we can reuse code from here
/// </summary>
public abstract class SuperUser {

    protected UserType type;
    protected int user_ID; 
    protected string user;
    protected string email;

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