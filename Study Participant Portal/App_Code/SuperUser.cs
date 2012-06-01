using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Parent of both participant and researcher. These are the attribtues
/// that are shared by both types of users
/// </summary>
public abstract class SuperUser {

    //*********************
    //Protected variables
    //*********************

    protected UserType type;
    protected int userID;    
    protected string userName;
    protected string firstName;
    protected string lastName;
    protected string email;

    //*********************
    //Public Properties
    //*********************

    //The type of user (either researcher or participant)
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
}