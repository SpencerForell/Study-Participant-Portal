using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Matchmaker
/// </summary>
public class Matchmaker {

    private Dictionary<Participant, int> results;

    public Dictionary<Participant, int> Results {
        get { return results; }
    }

	public Matchmaker(Study study) {
		
	}
}