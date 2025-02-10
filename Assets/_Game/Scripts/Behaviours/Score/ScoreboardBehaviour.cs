using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class ScoreEntry
    {
        public int rank;
        public string name;
        public int score;
    }

    public List<ScoreEntry> entries = new List<ScoreEntry>();

    public GameObject scoreEntryPrefab;

    public void AddScoreEntry(string playerName, int score)
    {
        ScoreEntry newEntry = new ScoreEntry
        {
            name = playerName,
            score = score
        };

        entries.Add(newEntry);
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        entries = entries.OrderByDescending(entry => entry.score).ToList();

        // Asign ranks
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].rank = i + 1;
        }

        // Clear the scoreboard
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ScoreEntry entry in entries)
        {
            GameObject newEntry = Instantiate(scoreEntryPrefab, transform);
            newEntry.GetComponent<ScoreEntryUI>().SetScoreEntry(entry);
        }
    }
}
