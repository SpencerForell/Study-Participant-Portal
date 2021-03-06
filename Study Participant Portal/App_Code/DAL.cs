﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// This class contains all the queries used in the entire program. The queries are
/// categorized by select, insert, update, and delete statements and are alphabetical.
/// </summary>
public static class DAL {

    /// <summary>
    /// A method to sanitize the inputs to allow for special characters
    /// and make it difficult for malicous users to sabotage the database.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static List<string> sanitizeInputs(params string[] inputs) {
        List<string> cleanInputs = new List<string>();
        string clean;
        if (inputs == null) {
            return null;
        }
        foreach (string input in inputs) {
            clean = Regex.Replace(input, @"[\r\n\x00\x1a\\'""]", @"\$0");
            cleanInputs.Add(clean);
        }
        return cleanInputs;
    }

    //********************************************************************
    //                       SELECT QUERIES
    //********************************************************************

    /// <summary>
    /// Returns all of the qualifiers Dictionary object
    /// </summary>
    /// <returns></returns>
    public static Dictionary<int, List<List<string>>> GetAllQualifiers() {
        List<string> answer = null;
        List<List<string>> temp = null;
        Dictionary<int, List<List<string>>> qualifiers = new Dictionary<int, List<List<string>>>();
        string queryString = "select Q.Qual_ID, Q.Question, Q.Description, Q.Res_ID, A.Ans_ID, " +
                             "A.Answer, A.Rank from Qualifiers As Q inner join Answers As A " +
                             "on Q.Qual_ID = A.Qual_ID";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        for (int i = 0; i < query.Results.Count; i++) {
            answer = new List<string>();
            for (int j = 0; j < query.Results[i].Count; j++) {
                answer.Add(query.Results[i][j]);
            }
            if (qualifiers.ContainsKey(Convert.ToInt32(query.Results[i][0]))) {
                qualifiers[Convert.ToInt32(query.Results[i][0])].Add(answer);
            }
            else {
                temp = new List<List<string>>();
                temp.Add(answer);
                qualifiers.Add(Convert.ToInt32(query.Results[i][0]), temp);
            }
        }
        return qualifiers;
    }

    /// <summary>
    /// Query to return the most recently created study to use in the main panel on the front page
    /// </summary>
    /// <returns></returns>
    public static Study GetLatestStudy() {
        string queryString = "select Study_ID, Name, Description, Incentive, Creation_Date, Expired, Res_ID from Study order by Creation_Date desc limit 1";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        List<string> result = query.Results[0];
        Study study = new Study(Convert.ToInt32(result[0]), result[1], result[2], result[3], Convert.ToDateTime(result[4]), Convert.ToBoolean(Convert.ToInt32(result[5])), Convert.ToInt32(result[6]), null);
        return study;
    }

    /// <summary>
    /// Gets all of the answers that a participant has answered so far
    /// </summary>
    /// <param name="partID"></param>
    /// <returns></returns>
    public static List<int> GetParticipantAnswers(int partID) {
        List<int> answerIDs = new List<int>();
        string queryString = "select Ans_ID " +
                             "from Participant_Answers " +
                             "where Par_ID = " + partID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        for (int i = 0; i < query.Results.Count; i++) {
            answerIDs.Add(Convert.ToInt32(query.Results[i][0]));
        }

        return answerIDs;
    }

    /// <summary>
    /// Gets all of the participant IDs in the database. 
    /// </summary>
    /// <returns></returns>
    public static List<int> GetParticipants() {
        List<int> ids = new List<int>();
        string queryString = "select Par_ID from Participant";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        for (int i = 0; i < query.Results.Count; i++) {
            ids.Add(Convert.ToInt32(query.Results[i][0]));
        }

        return ids;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Dictionary<int, List<List<string>>> GetParticipantsOptimized() {
        List<string> participantAnswer = null;
        List<List<string>> temp = null;
        Dictionary<int, List<List<string>>> participants = new Dictionary<int, List<List<string>>>();
        string queryString = "select P.Par_ID, P.User_Name, P.First_Name, P.Last_Name, P.Email, " +
                             "A.Ans_ID, A.Qual_ID, A.Answer, A.Rank, Q.Qual_ID, Q.Question, Q.Description, Q.Res_ID " +
                             "from Participant as P left outer join Participant_Answers as PA " +
                             "on P.Par_ID = PA.Par_ID left outer join Answers as A on PA.Ans_ID = A.Ans_ID " +
                             "left outer join Qualifiers as Q on A.Qual_ID = Q.Qual_ID";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        for (int i = 0; i < query.Results.Count; i++) {
            participantAnswer = new List<string>();
            for (int j = 0; j < query.Results[i].Count; j++) {
                participantAnswer.Add(query.Results[i][j]);
            }
            if (participants.ContainsKey(Convert.ToInt32(query.Results[i][0]))) {
                participants[Convert.ToInt32(query.Results[i][0])].Add(participantAnswer);
            }
            else {
                temp = new List<List<string>>();
                temp.Add(participantAnswer);
                participants.Add(Convert.ToInt32(query.Results[i][0]), temp);
            }
        }

        return participants;
    }

    /// <summary>
    /// Returns all of the qualifiers in the database.
    /// </summary>
    /// <param name="parID"></param>
    /// <returns></returns>
    public static List<Qualifier> GetQualifiers(int parID) {
        string queryString = "select distinct Qual_ID from Answers " +
                             "where Qual_ID not in(select Qual_ID from Answers As A " +
                             "inner join Participant_Answers As PA on A.Ans_ID = PA.Ans_ID " +
                             "where PA.Par_ID = " + parID.ToString() + ")";
        Qualifier qual = null;
        List<Qualifier> quals = new List<Qualifier>();

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

        for (int i = 0; i < query.Results.Count; i++) {
            qual = new Qualifier(Convert.ToInt32(query.Results[i][0]));
            quals.Add(qual);
        }

        return quals;
    }

    public static List<string> GetRecipientEmails() {
        List<string> recipients = new List<string>();
        string queryString = "select Email from Participant";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

        for (int i = 0; i < query.Results.Count; i++) {
            recipients.Add(query.Results[i][0]);
        }

        return recipients;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ResID"></param>
    /// <returns></returns>
    public static List<string> GetResearcher(int ResID) {
        List<string> resNameEmail = new List<string>();
        string queryString = "select User_Name, Email " +
                             "from Researcher " +
                             "where Res_ID = " + ResID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        resNameEmail.Add(query.Results[0][0]);
        resNameEmail.Add(query.Results[0][1]);
        return resNameEmail;
    }

    /// <summary>
    /// Gets all the studies in the database and returns them in a list
    /// </summary>
    /// <returns></returns>
    public static List<Study> GetStudies() {
        List<Study> studies = new List<Study>();
        string queryString = "select study_id, name, Description, Incentive, Creation_Date, Expired, Res_ID from Study";

        DatabaseQuery query1 = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

        for (int i = 0; i < query1.Results.Count; i++) {
            int studyID = Convert.ToInt32(query1.Results[i][0]);
            string name = query1.Results[i][1];
            string description = query1.Results[i][2];
            string incentive = query1.Results[i][3];
            DateTime dateCreated = Convert.ToDateTime(query1.Results[i][4]);
            bool expired = Convert.ToBoolean(Convert.ToInt32(query1.Results[i][5]));
            int researcherID = Convert.ToInt32(query1.Results[i][6]);

            List<Qualifier> qualifiers = new List<Qualifier>();
            queryString = "select Q.Qual_ID, Question, Description, Res_ID from Study_Qualifiers SQ, Qualifiers Q where Study_ID = " + studyID + " and Q.Qual_ID = SQ.Qual_ID";
            DatabaseQuery query2 = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
            foreach (List<String> result in query2.Results) {
                int qualID = Convert.ToInt32(result[0]);
                string question = result[1];
                string qualDescription = result[2];
                int resID = Convert.ToInt32(result[3]);
                Qualifier tempQualifiers = new Qualifier(qualID, question, description, resID);
                qualifiers.Add(tempQualifiers);
            }

            Study temp = new Study(studyID, name, description, incentive, dateCreated, expired, researcherID, qualifiers);
            studies.Add(temp);
        }
        return studies;
    }

    /// <summary>
    /// Checks to see if a qualifier exists in a particular study.
    /// </summary>
    /// <param name="studyID">The study being checked.</param>
    /// <param name="qualID">The qualifier being checked.</param>
    /// <returns>True if qualifier belongs to the study. False if it belongs to another study.</returns>
    public static bool QualifierExistsInStudy(int studyID, int qualID) {
        string queryString = "select 'True' " +
                             "from Study_Qualifiers " +
                             "where Study_ID = " + studyID + " and Qual_ID =  " + qualID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        try {
            return Convert.ToBoolean(query.Results[0][0]);
        }
        catch {
            return false;
        }
    }

    //********************************************************************
    //                       IINSERT QUERIES
    //********************************************************************

    /// <summary>
    /// Insert an answer into the database and link it to its associated qualifier.
    /// </summary>
    /// <param name="answer"></param>
    /// <param name="qualID"></param>
    /// <returns></returns>
    public static int InsertAnswer(Answer answer, int qualID) {
        List<string> scrubbedInput = sanitizeInputs(answer.AnswerText, answer.Score.ToString());
        string queryString = "insert into Answers " +
                             "(Answer, Rank, Qual_ID) " +
                             "values " +
                             "('" + scrubbedInput[0] + "', " + Convert.ToInt32(scrubbedInput[1]) + ", " + qualID + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }

    /// <summary>
    /// Inserts a pre-existing qualifier into the Study_Qualifier table.
    /// Doesn't insert into the Qualifier table because its pre-existing.
    /// </summary>
    /// <param name="qualifier"></param>
    /// <param name="studyID"></param>
    /// <returns></returns>
    public static int InsertExisingQualifier(Qualifier qualifier, int studyID) {
        string queryString = "insert into Study_Qualifiers " +
                             "(Qual_ID, Study_ID) " +
                             "values " +
                             "(" + qualifier.QualID + ", " + studyID + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }

    /// <summary>
    /// Insert a Participant into the database
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public static void InsertParticipant(string userName, string firstName, string lastName, string email, string password) {
        List<string> scrubbedInput = sanitizeInputs(userName, firstName, lastName, email, password);

        string queryString = "insert into Participant" +
                              " (User_Name, First_Name, Last_Name, Email, Password, Num_Ratings)" +
                              " values " +
                              " ('" + scrubbedInput[0] + "', '" + scrubbedInput[1] + "','" + scrubbedInput[2] + "','" + scrubbedInput[3] + "', '" + scrubbedInput[4] + "',0)";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
    }
    /// <summary>
    /// Insert that a specific participant has selected a specific answer to a qualifier.
    /// </summary>
    /// <param name="partID"></param>
    /// <param name="answerID"></param>
    /// <returns></returns>
    public static int InsertParticipantAnswer(int partID, int answerID) {
        string queryString = "insert into Participant_Answers " +
                             "(Par_ID, Ans_ID) " +
                             "values " +
                             "(" + partID + ", " + answerID + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }

    /// <summary>
    /// Inserts a new qualifier into the database
    /// </summary>
    /// <param name="qualifier"></param>
    /// <returns>Returns the qualifierID</returns>
    public static int InsertQualifier(Qualifier qualifier, int studyID) {
        List<string> scrubbedInput = sanitizeInputs(qualifier.Question, qualifier.Description);
        string queryString = "insert into Qualifiers " +
                             "(Question, Description, Res_ID) " +
                             "values " +
                             "('" + scrubbedInput[0] + "','" + scrubbedInput[1] + "', " + qualifier.ResID + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        int qualID = query.LastInsertID;

        queryString = "insert into Study_Qualifiers " +
                      "(Qual_ID, Study_ID) " +
                      "values " +
                      "(" + qualID + ", " + studyID + ")";
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return qualID;
    }

    /// <summary>
    /// Inserts a researcher into the database.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public static void InsertResearcher(string userName, string firstName, string lastName, string email, string password) {
        List<string> scrubbedInput = sanitizeInputs(userName, firstName, lastName, email, password);

        string queryString = "insert into Researcher" +
                            " (User_Name, First_Name, Last_Name, Email, Password, Num_Ratings)" +
                            " values " +
                            " ('" + scrubbedInput[0] + "', '" + scrubbedInput[1] + "','" + scrubbedInput[2] + "', '" + scrubbedInput[3] + "', '" + scrubbedInput[4] + "',0)";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
    }

    /// <summary>
    /// Inserts a new study into the database
    /// </summary>
    /// <param name="study"></param>
    /// <returns>Returns the studyID of the newly created study</returns>
    public static int InsertStudy(Study study) {
        List<string> scrubbedInput = sanitizeInputs(study.Name, study.Description, study.Incentive, study.ResearcherID.ToString());
        string queryString = "insert into Study " +
                             "(Name, Description, Incentive, Creation_Date, Expired, Res_ID) " +
                             "values " +
                             "('" + scrubbedInput[0] + "','" + scrubbedInput[1] + "','" + scrubbedInput[2] + "', NOW(), 0, " + scrubbedInput[3] + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }

    //********************************************************************
    //                       UPDATE QUERIES
    //********************************************************************

    /// <summary>
    /// Update an answer. Note: AnsID must be set and qualID must be set for this to update properly.
    /// </summary>
    /// <param name="answer"></param>
    public static void UpdateAnswer(Answer answer) {
        List<string> scrubbedInput = sanitizeInputs(answer.AnswerText, answer.Score.ToString());
        if (answer.AnsID <= 0) {
            throw new Exception("Invalid answer to update, the AnsID = " + answer.AnsID);
        }
        string queryString = "update Answers set " +
                             "Answer = '" + scrubbedInput[0] + "', " +
                             "Rank = '" + Convert.ToInt32(scrubbedInput[1]) + "' " +
                             "where Ans_ID = " + answer.AnsID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    /// <summary>
    /// Update a participant in the database. Participant IDs must match.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="parID"></param>
    public static void UpdateParticipant(string userName, string firstName, string lastName, string email, string password, int parID) {
        List<string> scrubbedInput = sanitizeInputs(userName, firstName, lastName, email, password);

        string queryString = "update Participant " +
                              "set User_Name = '" + scrubbedInput[0] + "'," +
                              "First_Name = '" + scrubbedInput[1] + "'," +
                              "Last_Name = '" + scrubbedInput[2] + "'," +
                              "Email = '" + scrubbedInput[3] + "'," +
                              "Password = '" + scrubbedInput[4] + "' " +
                              "where Par_ID = " + parID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    /// <summary>
    /// Updates a qualifier. NOTE: the qualifier must have its qualID field properly set or it will throw an exception.
    /// </summary>
    /// <param name="qualifier"></param>
    public static void UpdateQualifier(Qualifier qualifier) {
        List<string> scrubbedInput = sanitizeInputs(qualifier.Question, qualifier.Description);
        if (qualifier.QualID <= 0) {
            throw new Exception("Invalid qualifier to update, the QualID= " + qualifier.QualID);
        }
        string queryString = "update Qualifiers set " +
                             "Question = '" + scrubbedInput[0] + "'," +
                             "Description = '" + scrubbedInput[1] + "'," +
                             "Res_ID = " + qualifier.ResID + " " +
                             "where Qual_ID = " + qualifier.QualID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    /// <summary>
    /// Updates a Researcher in the database. Researcher IDs must match.
    /// </summary>
    /// <param name="resID"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public static void UpdateResearcher(int resID, string firstName, string lastName, string userName, string email, string password) {
        List<string> scrubbedInput = sanitizeInputs(firstName, lastName, userName, email, password);

        string queryString = "update Researcher " +
                              "set User_Name = '" + scrubbedInput[2] + "'," +
                              "First_Name = '" + scrubbedInput[0] + "'," +
                              "Last_Name = '" + scrubbedInput[1] + "'," +
                              "Email = '" + scrubbedInput[3] + "'," +
                              "Password = '" + scrubbedInput[4] + "' " +
                              "where Res_ID = " + resID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    /// <summary>
    /// Updates a specific study. NOTE: the study must have its studyID field properly set or it will throw an exception.
    /// </summary>
    /// <param name="study"></param>
    public static void UpdateStudy(Study study) {
        List<string> scrubbedInput = sanitizeInputs(study.Name, study.Description, study.Incentive);
        if (study.StudyID <= 0) {
            throw new Exception("Invalid study to update, the studyID = " + study.StudyID);
        }
        string queryString = "update Study set " +
                             "Name = '" + scrubbedInput[0] + "', " +
                             "Description = '" + scrubbedInput[1] + "', " +
                             "Incentive = '" + scrubbedInput[2] + "', " +
                             "Expired = " + Convert.ToInt32(study.Expired) + " " +
                             "where Study_ID = " + study.StudyID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    //********************************************************************
    //                       DELETE QUERIES
    //********************************************************************

    /// <summary>
    /// Permanently deletes an answer from the database.
    /// </summary>
    /// <param name="answer"></param>
    public static void DeleteAnswer(Answer answer) {
        if (answer.AnsID <= 0) {
            throw new Exception("Invalid answer to delete, the AnsID = " + answer.AnsID);
        }
        string queryString = "delete from Answers where Ans_ID = " + answer.AnsID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Delete);
    }

    /// <summary>
    /// Permanently deletes a qualifier from the database and from the study it was being used in.
    /// </summary>
    /// <param name="qualifier"></param>
    /// <param name="study"></param>
    public static void DeleteQualifier(Qualifier qualifier, Study study) {
        if (qualifier.QualID <= 0) {
            throw new Exception("Invalid qualifier to delete, the qualID = " + qualifier.QualID);
        }
        if (study.StudyID <= 0) {
            throw new Exception("Invalid study to delete, the studyID = " + study.StudyID);
        }
        string queryString = "delete from Study_Qualifiers where Qual_ID = " + qualifier.QualID + " and Study_ID = " + study.StudyID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Delete);
    }
}