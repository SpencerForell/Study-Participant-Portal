using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Matchmaker
/// </summary>
public class Matchmaker {

    private Dictionary<Participant, int> results = new Dictionary<Participant,int>();

    public Dictionary<Participant, int> Results {
        get { return results; }
    }

	public Matchmaker(Study study) {
        results = makeMatch(study);
	}

    /// <summary>
    /// This method gets all of the participants out of the database and stores them in a Participant list
    /// This is optimized because we are only accessing the database once to get all the information.
    /// </summary>
    /// <returns></returns>
    private List<Participant> GetAllParticipants() {
        bool exists = false;
        int index = 0;
        Dictionary<int, List<List<string>>> participantsRaw = DAL.GetParticipantsOptimized();
        List<Participant> participants = new List<Participant>();
        List<Qualifier> qualifiers = new List<Qualifier>();
        List<Answer> answers = null;
        Answer answer = null;
        Qualifier qualifier = null;
        Participant participant = null;

        //iterate through each participant in the raw dictionary
        foreach (KeyValuePair<int, List<List<string>>> kvp in participantsRaw) {
            answers = new List<Answer>();
            //iterate through each record in the participant
            foreach (List<string> record in kvp.Value) {
                //first check the qualifier list to make sure it doesn't already exist.
                for (int i = 0; i < qualifiers.Count; i++) {
                    exists = false;
                    if (qualifiers[i].QualID == Convert.ToInt32(record[9])) {
                        //if qualifier already exists than break out of loop
                        exists = true;
                        //we will use this index to access the Qualifier list later
                        index = i;
                        break;
                    }
                }
                //if the qualifier doesn't exist in the list 
                if (exists == false) {
                    //then create it and add it to the list.
                    qualifier = new Qualifier(Convert.ToInt32(record[9]), record[10], record[11], new List<Answer>());                  
                    //then create the answer object
                    answer = new Answer(Convert.ToInt32(record[5]), record[7], Convert.ToInt32(record[8]), qualifier);
                    //Add it to the list of answers
                    answers.Add(answer);
                    //now that we have our answer object, add it to the qualifier answer list
                    qualifier.Answers.Add(answer);
                    //finally, add the new qualifier to the qualifier list
                    qualifiers.Add(qualifier);
                }
                //if the qualifier already exists in the qualifier list
                else {
                    //then create the answer object
                    answer = new Answer(Convert.ToInt32(record[5]), record[7], Convert.ToInt32(record[8]), qualifiers[index]);
                    //add it to the list of answers
                    answers.Add(answer);
                    //add the new answer to the qualifier answer list
                    qualifiers[index].Answers.Add(answer);
                }
            }
            //finally create a new participant with all the data as well as the constructed answer list
            participant = new Participant(Convert.ToInt32(kvp.Value[0][0]), kvp.Value[0][1], kvp.Value[0][2], kvp.Value[0][3], kvp.Value[0][4], answers);
            //add it to the participant list
            participants.Add(participant);
        }
        return participants;
    }

    public Dictionary<Participant, int> makeMatch(Study study) {
        // create necassary variables
        //Participant participant = null;
        //List<int> participantIDs = DAL.GetParticipants();
        List<Participant> removeList = new List<Participant>();

        //We are now using the new optimized code. Still needs throurough testing.
        List<Participant> participants = GetAllParticipants();

        // Populate our Participant list based on the Participant IDs we got.
        //THIS NEEDS TO BE OPTIMIZED IT IS WAY TOO SLOW. WE CAN DO THIS BY USING THE OTHER CONSTRUCTOR AND JUST DOING ONE BIG QUERY TO RETURN ALL THE PARTICIPANTS AND MAKE THE OBJECTS FROM THAT
        
        //foreach (int id in participantIDs) {
        //    participant = new Participant(id);
        //    participants.Add(participant);
        //}

        // Go through each Participant in the Participant List.
        foreach (Participant p in participants) {
            // Go through each Answer in each participant Answer List.
            foreach (Answer ans in p.Answers) {
                // Go through each Qualifier in the provided Study.
                foreach (Qualifier qual in study.Qualifiers) {
                    // Compare the Qualifier id of the Participant answer to the Study Qual ID.
                    if (ans.Qualifier.QualID == qual.QualID) {
                        //Populate the dictionary
                        if (results.ContainsKey(p)) {
                            if (results[p] == -1) {
                                continue;
                            }
                            else if (ans.Score == -1) {
                                results[p] = -1;
                            }
                            else {
                                results[p] = results[p] += ans.Score;
                            }
                        }
                        else {
                            results.Add(p, ans.Score);
                        }
                    }
                }
            }
        }

        foreach (KeyValuePair<Participant, int> kvp in results) {
            if (kvp.Value == -1) {
                removeList.Add(kvp.Key);
            }
        }

        foreach (Participant p in removeList) {
            results.Remove(p);
        }

        
        return results;
    }
}