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
        List<string> scrubbedInput = sanitizeInputs(study.Name, study.Description, study.Incentive, study.ResearcherID.ToString());
        string queryString = "insert into Study " +
                             "(Name, Description, Incentive, Creation_Date, Expired, Res_ID) " +
                             "values " +
                             "('" + scrubbedInput[0] + "','" + scrubbedInput[1] + "','" + scrubbedInput[2] + "', NOW(), 0, " + scrubbedInput[3] + ")";

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
                             "('" + scrubbedInput[0] + "','" + scrubbedInput[1] + "', " + qualifier.ResID +")";

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
        List<string> scrubbedInput = sanitizeInputs(answer.AnswerText, answer.Score.ToString());
        string queryString = "insert into Answers " +
                             "(Answer, Rank, Qual_ID) " +
                             "values " +
                             "('" + scrubbedInput[0] + "', " + Convert.ToInt32(scrubbedInput[1]) + ", " + qualID + ")";

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
                             "Res_ID = " + qualifier.ResID +
                             "where Qual_ID = " + qualifier.QualID;

        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Update);
    }

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