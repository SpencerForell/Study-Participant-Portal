using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Qualifiers (Also known as requrements) are questions that Researchers setup and Participants
/// answer to filter and figure out participants to find the best matches.
/// </summary>
public class Qualifier {
    private int qualID;
    private string question;
    private string description;
    private int resID;
    private List<Answer> answers = new List<Answer>();

    public int QualID {
        get { return qualID; }
        set { qualID = value; }
    }

    public string Question {
        get { return question; }
    }

    public string Description {
        get { return description; }
    }

    /// <summary>
    /// ID of the researcher that created the qualifier
    /// </summary>
    public int ResID {
        get { return resID; }
    }

    /// <summary>
    /// All of the answers associated with this qualifier
    /// </summary>
    public List<Answer> Answers {
        get { return answers; }
        set { answers = value; }
    }

    /// <summary>
    /// Constructor that queries the database to build the object. The ID must exist in the database.
    /// </summary>
    /// <param name="qualID"></param>
    public Qualifier(int qualID) {
        string queryString = "select Question, Description, Res_ID from Qualifiers where Qual_ID = " + qualID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        this.question = query.Results[0][0];
        this.description = query.Results[0][1];
        this.resID = Convert.ToInt32(query.Results[0][2]);
        this.qualID = qualID;

        queryString = "select Ans_ID, Answer, Rank from Answers where Qual_ID = " + qualID;
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        foreach (List<String> result in query.Results) {
            int ansID = Convert.ToInt32(result[0]);
            string answer = result[1];
            int score = Convert.ToInt32(result[2]);
            Answer tempAnswer = new Answer(ansID, answer, score, this);
            this.answers.Add(tempAnswer);
        }
	}

    /// <summary>
    /// Constructor used to manually build a qualifier, however it will still try to query the database to get the answers.
    /// In general, the other two constructors are more practical to use - this should only be used in specific cases.
    /// </summary>
    /// <param name="qualID"></param>
    /// <param name="question"></param>
    /// <param name="description"></param>
    /// <param name="resID"></param>
    public Qualifier(int qualID, string question, string description, int resID) {
        this.qualID = qualID;
        this.question = question;
        this.description = description;
        this.resID = resID;

        string queryString = "select Ans_ID, Answer, Rank from Answers where Qual_ID = " + qualID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        foreach (List<String> result in query.Results) {
            int ansID = Convert.ToInt32(result[0]);
            string answer = result[1];
            int score = Convert.ToInt32(result[2]);
            Answer tempAnswer = new Answer(ansID, answer, score, this);
            this.answers.Add(tempAnswer);
        }
    }

    /// <summary>
    /// Constructor to completely build a Qualifier manually. The list of answers can be assigned to null.
    /// </summary>
    /// <param name="qualID"></param>
    /// <param name="question"></param>
    /// <param name="description"></param>
    /// <param name="resID"></param>
    /// <param name="answers"></param>
    public Qualifier(int qualID, string question, string description, int resID, List<Answer> answers) {
        this.qualID = qualID;
        this.question = question;
        this.description = description;
        this.resID = resID;
        this.answers = answers;
    }
}