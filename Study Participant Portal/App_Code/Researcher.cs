using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Researcher
/// </summary>
public class Researcher: SuperUser {
    
    public Researcher() {
        type = UserType.Researcher;
        //
		// TODO: Add constructor logic here
		//
	}
}