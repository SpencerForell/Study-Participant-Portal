using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Parent of both child and researcher so we can reuse code from here
/// </summary>
public abstract class SuperUser {
    protected Type type;
    protected int userID; //not sure if this should be userID or user_ID
    protected string user;
    protected string email;

    protected enum Type {
        Researcher,
        Participant
    }

    public Type getType() {
        return type;
    }
    
    public SuperUser() {
		//
		// TODO: Add constructor logic here
		//
	}
}