using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Study
/// </summary>
public class Study
{
    private string studyName;
    private string studyDescript;
    private bool expired;
    private string dateCreated;

    public string StudyName {
        get { return studyName; }
    }

    public string StudyDescript {
        get { return studyDescript; }
    }

    public bool Expired {
        get { return expired; }
    }

    public string DateCreated {
        get { return dateCreated; }
    }

	public Study()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}