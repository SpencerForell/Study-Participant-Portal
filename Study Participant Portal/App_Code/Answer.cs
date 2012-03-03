using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Answer
/// </summary>
public class Answer {
    private int ansID;
    private string answerText;
    private int score;

    public int AnsID {
        get { return ansID; }
    }

    public string AnswerText {
        get { return answerText; }
    }

    public int Score {
        get { return score; }
    }

    public Answer(int ansID) {
        string queryString = "select Answer, Rank from Answers where Ans_ID = " + ansID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        this.answerText = query.Results[0][0];
        this.score = Convert.ToInt32(query.Results[0][1]);
        this.ansID = ansID;
	}

    public Answer(int ansID, string answer, int score) {
        this.ansID = ansID;
        this.answerText = answer;
        this.score = score;
    }
}