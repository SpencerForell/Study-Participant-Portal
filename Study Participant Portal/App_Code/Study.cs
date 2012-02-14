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
        set { studyName = value; }
    }

    public string StudyDescript {
        get { return studyDescript; }
        set { studyDescript = value; }
    }

    public bool Expired {
        get { return expired; }
        set { expired = value; }
    }

    public string DateCreated {
        get { return dateCreated; }
        set { dateCreated = value; }
    }

	public Study()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}