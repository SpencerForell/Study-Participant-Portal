using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Study
/// </summary>
public class Study {
    private string studyName;
    private string studyDescription;
    private DateTime dateCreated;
    private bool expired;
    private int study_id;
    private int researcher_id;

    public string StudyName {
        get { return studyName; }
    }

    public string StudyDescription {
        get { return studyDescription; }
    }
    
    public DateTime DateCreated {
        get { return dateCreated; }
    }

    public bool Expired {
        get { return expired; }
    }

    public int Study_ID {
        get { return study_id; }
    }

    public int Researcher_ID {
        get { return researcher_id; }
    }

    public Study(int study_id) {
        string queryString = "select name, Description, Creation_Date, Expired, Res_ID from Study where Study_ID = " + study_id.ToString();
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        this.studyName = query.Results[0][0];
        this.studyDescription = query.Results[0][1];
        this.dateCreated = Convert.ToDateTime(query.Results[0][2]);
        this.expired = Convert.ToBoolean(Convert.ToInt32(query.Results[0][3]));
        this.researcher_id = Convert.ToInt32(query.Results[0][4]);
        this.study_id = study_id;
    }
}