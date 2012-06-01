using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class represents the answers that a qualifier has. Answers are related directly to one qualifier.
/// Scores are associated with an answer as well, which go into the matchmaking algorithm. A -1 means the
/// participant is ineligible, a 0 has no affect, and a score of 1 or higher will rank the participant higher
/// if they selected the answer.
/// </summary>
public class Answer {
    private int ansID;
    private string answerText;
    private int score;
    private Qualifier qualifier;

    public int AnsID {
        get { return ansID; }
        set { ansID = value; }
    }

    public string AnswerText {
        get { return answerText; }
    }

    public int Score {
        get { return score; }
    }

    public Qualifier Qualifier {
        get { return qualifier; }
    }

    /// <summary>
    /// Constructor used to query the database and create an answer object based on the id
    /// passed in. The ID must exist in the database or this will fail.
    /// </summary>
    /// <param name="ansID"></param>
    public Answer(int ansID) {
        string queryString = "select Answer, Rank from Answers where Ans_ID = " + ansID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        this.answerText = query.Results[0][0];
        this.score = Convert.ToInt32(query.Results[0][1]);
        this.ansID = ansID;

        queryString = "select Qual_ID from Answers where Ans_ID = " + ansID;
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        int qualID = Convert.ToInt32(query.Results[0][0]);
        this.qualifier = new Qualifier(qualID);
	}

    /// <summary>
    /// Constructor to build an answer based only on the parameters given. Use this to create an answer
    /// based on text fields provided on a web form or when using a large query of answers to be more time 
    /// efficient. 
    /// </summary>
    /// <param name="ansID"></param>
    /// <param name="answer"></param>
    /// <param name="score"></param>
    /// <param name="qualifier"></param>
    public Answer(int ansID, string answer, int score, Qualifier qualifier) {
        this.ansID = ansID;
        this.answerText = answer;
        this.score = score;
        this.qualifier = qualifier;
    }
}