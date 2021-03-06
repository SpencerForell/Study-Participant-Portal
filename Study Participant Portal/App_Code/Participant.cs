﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Participants are users that go in and answer qualifiers(requirements) that are setup by researchers
/// They can be contacted by researchers to come in and do additional work/testing/research with a particular study.
/// </summary>
public class Participant: SuperUser {

    private List<Answer> answers;

    public List<Answer> Answers {
        get { return answers; }
    }

    /// <summary>
    /// Constructor that builds a participant object by passing in all of the attributes
    /// </summary>
    public Participant(int userID, string user_name, string first_name, string last_name, string email, List<Answer> answers) {
        type = UserType.Participant;
        this.userID = userID;
        this.userName = user_name;
        this.firstName = first_name;
        this.lastName = last_name;
        this.email = email;
        this.answers = answers;
    }

     /// <summary>
    /// Constructor that runs a query to populate all fields of a Participant object
    /// </summary>
    /// <param name="user_id">The id of the researcher to create an object for</param>
    public Participant(int user_id) {
        string queryString = "select User_Name, First_Name, Last_Name, Email from Participant where Par_ID = " + user_id.ToString();
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

        this.userID = user_id;
        this.userName = query.Results[0][0];
        this.firstName = query.Results[0][1];
        this.lastName = query.Results[0][2];
        this.email = query.Results[0][3];

        queryString = "select Ans_ID from Participant_Answers where Par_ID = " + userID;
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

        int count = query.Results.Count;
        this.answers = new List<Answer>();
        for (int i = 0; i < count; i++) {
            int ansID = Convert.ToInt32(query.Results[i][0]);
            Answer answer = new Answer(ansID);
            this.answers.Add(answer);
        }
    }
}