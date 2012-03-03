﻿using System;
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
    private List<Answer> answers = new List<Answer>();

    public int QualID {
        get { return qualID; }
    }

    public string Question {
        get { return question; }
    }

    public string Description {
        get { return description; }
    }

    public List<Answer> Answers {
        get { return answers; }
    }

    public Qualifier(int qualID) {
        string queryString = "select Question, Description from Qualifiers where Quali_ID = " + qualID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        this.question = query.Results[0][0];
        this.description = query.Results[0][1];
        this.qualID = qualID;

        queryString = "select Ans_ID, Answer, Rank from Answers where Qual_ID = " + qualID;
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        foreach (List<String> result in query.Results) {
            int ansID = Convert.ToInt32(result[0]);
            string answer = result[1];
            int score = Convert.ToInt32(result[2]);
            Answer tempAnswer = new Answer(ansID, answer, score);
            this.answers.Add(tempAnswer);
        }
	}

    public Qualifier(int qualID, string question, string description) {
        this.qualID = qualID;
        this.question = question;
        this.description = description;

        string queryString = "select Ans_ID, Answer, Rank from Answers where Qual_ID = " + qualID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        foreach (List<String> result in query.Results) {
            int ansID = Convert.ToInt32(result[0]);
            string answer = result[1];
            int score = Convert.ToInt32(result[2]);
            Answer tempAnswer = new Answer(ansID, answer, score);
            this.answers.Add(tempAnswer);
        }
    }
}