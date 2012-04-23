using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Study
/// </summary>
public class Study {
    private string name;
    private string description;
    private string incentive;
    private DateTime dateCreated;
    private bool expired;
    private int studyID;
    private int researcherID;
    private List<Qualifier> qualifiers = new List<Qualifier>();

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public string Incentive {
        get { return incentive; }
    }

    public DateTime DateCreated {
        get { return dateCreated; }
    }

    public bool Expired {
        get { return expired; }
    }

    public int StudyID {
        get { return studyID; }
        set { studyID = value; }
    }

    public int ResearcherID {
        get { return researcherID; }
    }

    public List<Qualifier> Qualifiers {
        get { return qualifiers; }
        set { qualifiers = value; }
    }

   

    public Study(int studyID) {
        string queryString = "select name, Description, Incentive, Creation_Date, Expired, Res_ID from Study where Study_ID = " + studyID.ToString();
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        this.name = query.Results[0][0];
        this.description = query.Results[0][1];
        this.incentive = query.Results[0][2];
        this.dateCreated = Convert.ToDateTime(query.Results[0][3]);
        this.expired = Convert.ToBoolean(Convert.ToInt32(query.Results[0][4]));
        this.researcherID = Convert.ToInt32(query.Results[0][5]);
        this.studyID = studyID;

        queryString = "select Q.Qual_ID, Question, Description from Study_Qualifiers SQ, Qualifiers Q where Study_ID = " + studyID + " and Q.Qual_ID = SQ.Qual_ID";
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        foreach (List<String> result in query.Results) {
            int qualID = Convert.ToInt32(result[0]);
            string question = result[1];
            string description = result[2];
            Qualifier tempQualifiers = new Qualifier(qualID, question, description);
            this.qualifiers.Add(tempQualifiers);
        }
    }

    public Study(int studyID, string name, string description, string incentive, DateTime dateCreated, bool expired, int researcherID, List<Qualifier> qualifiers) {
        this.studyID = studyID;
        this.name = name;
        this.description = description;
        this.incentive = incentive;
        this.dateCreated = dateCreated;
        this.expired = expired;
        this.researcherID = researcherID;
        this.qualifiers = qualifiers;
    }

}