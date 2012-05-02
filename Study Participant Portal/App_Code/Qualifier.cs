using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Qualifier
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

    public int ResID {
        get { return resID; }
    }

    public List<Answer> Answers {
        get { return answers; }
        set { answers = value; }
    }

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

    public Qualifier(int qualID, string question, string description, int resID, List<Answer> answers) {
        this.qualID = qualID;
        this.question = question;
        this.description = description;
        this.resID = resID;
        this.answers = answers;
    }
}