using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Parent of both child and researcher so we can reuse code from here
/// </summary>
public abstract class SuperUser {
    public enum Type {
        Researcher,
        Participant
    }   
    
    public SuperUser() {
		//
		// TODO: Add constructor logic here
		//
	}
}