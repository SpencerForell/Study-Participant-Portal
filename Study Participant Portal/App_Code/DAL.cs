using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DAL
/// </summary>
public static class DAL {

    /// <summary>
    /// Inserts a new study into the database
    /// </summary>
    /// <param name="study"></param>
    /// <returns>Returns the studyID of the newly created study</returns>
    public static int InsertStudy(Study study) {
        string queryString = "insert into Study " +
                             "(Name, Description, Creation_Date, Expired, Res_ID) " +
                             "values " +
                             "('" + study.Name + "','" + study.Description + "', NOW(), 0, " + study.ResearcherID.ToString() + ")";

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