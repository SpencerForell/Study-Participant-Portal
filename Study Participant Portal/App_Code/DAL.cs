using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for DAL
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

    /// <summary>
    /// Inserts a new study into the database
    /// </summary>
    /// <param name="study"></param>
    /// <returns>Returns the studyID of the newly created study</returns>
    public static int InsertStudy(Study study) {
        List<string> allClean = sanitizeInputs(study.Name, study.Description, study.ResearcherID.ToString());
        string queryString = "insert into Study " +
                             "(Name, Description, Creation_Date, Expired, Res_ID) " +
                             "values " +
                             "('" + allClean[0] + "','" + allClean[1] + "', NOW(), 0, " + allClean[2] + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }

    

    /// <summary>
    /// Inserts a new qualifier into the database
    /// </summary>
    /// <param name="qualifier"></param>
    /// <returns>Returns the qualifierID</returns>
    public static int InsertQualifier(Qualifier qualifier, int studyID) {
        string queryString = "insert into Qualifiers " +
                             "(Question, Description) " +
                             "values " +
                             "('" + qualifier.Question + "','" + qualifier.Description + "')";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        int qualID = query.LastInsertID;

        queryString = "insert into Study_Qualifiers " +
                      "(Qual_ID, Study_ID) " +
                      "values " +
                      "(" + qualID + ", " + studyID + ")";
        query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return qualID;
    }

    public static int InsertAnswer(Answer answer, int qualID) {
        string queryString = "insert into Answers " +
                             "(Answer, Rank, Qual_ID) " +
                             "values " +
                             "('" + answer.AnswerText + "', " + answer.Score + ", " + qualID + ")";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }

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

    public static List<int> GetParticipants() {
        List<int> ids = new List<int>();
        string queryString = "select Par_ID from Participant";

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        for (int i = 0; i < query.Results.Count; i++) {
            ids.Add(Convert.ToInt32(query.Results[i][0]));
        }

        return ids;
    }

    public static int InsertParticipantAnswer(int partID, int answerID) {
        string queryString = "insert into Participant_Answers " +
                             "(Par_ID, Ans_ID) " +
                             "values " +
                             "(" + partID + ", " + answerID + ")";
 
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
        return query.LastInsertID;
    }
    /// <summary>
    /// Updates a specific study. NOTE: the study must have its studyID field properly set or it will throw an exception.
    /// </summary>
    /// <param name="study"></param>
    public static void UpdateStudy(Study study) {
        if (study.StudyID <= 0) {
            throw new Exception("Invalid study to update, the studyID = " + study.StudyID);
        }
        string queryString = "update Study set " +
                             "Name = '" + study.Name + "', " +
                             "Description = '" + study.Description + "', " +
                             "Expired = " + Convert.ToInt32(study.Expired) + " " +
                             "where Study_ID = " + study.StudyID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    /// <summary>
    /// Updates a qualifier. NOTE: the qualifier must have its qualID field properly set or it will throw an exception.
    /// </summary>
    /// <param name="qualifier"></param>
    public static void UpdateQualifier(Qualifier qualifier) {
        if (qualifier.QualID <= 0) {
            throw new Exception("Invalid qualifier to update, the QualID= " + qualifier.QualID);
        }
        string queryString = "update Qualifiers set " +
                             "Question = '" + qualifier.Question + "'," +
                             "Description = '" + qualifier.Description + "' " +
                             "where Qual_ID = " + qualifier.QualID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    /// <summary>
    /// Update an answer. Note: AnsID must be set and qualID must be set for this to update properly.
    /// </summary>
    /// <param name="answer"></param>
    public static void UpdateAnswer(Answer answer) {
        if (answer.AnsID <= 0) {
            throw new Exception("Invalid answer to update, the AnsID = " + answer.AnsID);
        }
        string queryString = "update Answers set " +
                             "Answer = '" + answer.AnswerText + "', " +
                             "Rank = '" + answer.Score + "' " +
                             "where Ans_ID = " + answer.AnsID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

    public static void DeleteAnswer(Answer answer) {
        if (answer.AnsID <= 0) {
            throw new Exception("Invalid answer to delete, the AnsID = " + answer.AnsID);
        }
        string queryString = "delete from Answers where Ans_ID = " + answer.AnsID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Delete);
    }


    public static void DeleteQualifier(Qualifier qualifier) {
        if (qualifier.QualID <= 0) {
            throw new Exception("Invalid qualifier to delete, the AnsID = " + qualifier.QualID);
        }
        string queryString = "delete from Qualifiers where Qual_ID = " + qualifier.QualID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Delete);
    }
}