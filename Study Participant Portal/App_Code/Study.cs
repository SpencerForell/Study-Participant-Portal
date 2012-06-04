using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A Study contains all the general information for the study the Researcher wants to perform,
/// as well as the qualifiers(requirements) that are needed to be eligible to participate in the
/// study. 
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

    /// <summary>
    /// Name of the study
    /// </summary>
    public string Name {
        get { return name; }
    }

    /// <summary>
    /// Description of the study
    /// </summary>
    public string Description {
        get { return description; }
    }

    /// <summary>
    /// The incentive to get Participants to want to take part in the study.
    /// </summary>
    public string Incentive {
        get { return incentive; }
    }

    /// <summary>
    /// The datetime that the study was created.
    /// </summary>
    public DateTime DateCreated {
        get { return dateCreated; }
    }

    /// <summary>
    /// If the study is completed or not
    /// </summary>
    public bool Expired {
        get { return expired; }
    }

    /// <summary>
    /// The primary key study ID.
    /// </summary>
    public int StudyID {
        get { return studyID; }
        set { studyID = value; }
    }

    /// <summary>
    /// Researcher that created the study's ID
    /// </summary>
    public int ResearcherID {
        get { return researcherID; }
    }

    /// <summary>
    /// Qualifiers associated with this study
    /// </summary>
    public List<Qualifier> Qualifiers {
        get { return qualifiers; }
        set { qualifiers = value; }
    }

    /// <summary>
    /// Constructor that queries the database to get all relevant information about a study
    /// and create an object out of it. this constructor uses significantly more overhead than
    /// the following one so it should only be used if it is not feasible to use the other one.
    /// </summary>
    /// <param name="studyID"></param>
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

        queryString = "select Q.Qual_ID, Question, Description, Res_ID from Study_Qualifiers SQ, Qualifiers Q where Study_ID = " + studyID + " and Q.Qual_ID = SQ.Qual_ID";
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        foreach (List<String> result in query.Results) {
            int qualID = Convert.ToInt32(result[0]);
            string question = result[1];
            string description = result[2];
            int resID = Convert.ToInt32(result[3]);
            Qualifier tempQualifiers = new Qualifier(qualID, question, description, resID);
            this.qualifiers.Add(tempQualifiers);
        }
    }

    /// <summary>
    /// Constructor that manually builds a Study based on input parameters
    /// </summary>
    /// <param name="studyID"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="incentive"></param>
    /// <param name="dateCreated"></param>
    /// <param name="expired"></param>
    /// <param name="researcherID"></param>
    /// <param name="qualifiers"></param>
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