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

    public Dictionary<Participant, int> makeMatch(Study study) {
        // create necassary variables
        Participant part = null;
        List<int> partIDs = DAL.GetParticipants();
        List<Participant> parts = new List<Participant>();

        // Populate our Participant list based on the Participant IDs we got.
        foreach (int id in partIDs) {
            part = new Participant(id);
            parts.Add(part);
        }

        // Go through each Participant in the Participant List.
        foreach (Participant p in parts) {
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
                results.Remove(kvp.Key);
            }
        }

        return results;
    }
}