using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardBehaviour : MonoBehaviour
{
    public List<ScoreEntry> entries = new List<ScoreEntry>();

    public int maxEntries = 8;

    // Prefabs
    public GameObject scoreEntryPrefab;
    public GameObject scoreEntryInputPrefab;

    // References to UI elements
    public GameObject submitScoreButton;
    private ScoreEntryInputUI playerScoreEntryUI;


    public bool IsScoreQualifying(int newScore)
    {
        if (entries.Count < maxEntries)
            return true;

        var sortedEntries = entries.OrderByDescending(e => e.score).ToList();
        int lowestQualifyingScore = sortedEntries[maxEntries - 1].score;

        if (newScore > lowestQualifyingScore)
            return true;

        // New score is equal to the lowest qualifying score and there's space
        if (newScore == lowestQualifyingScore && entries.Count < maxEntries)
            return true;

        return false;
    }

    public void AddScoreEntry(string playerName, int score, bool isPlayerEntry = false)
    {
        ScoreEntry newEntry = new ScoreEntry
        {
            name = playerName,
            score = score,
            isInputEntry = isPlayerEntry
        };

        entries.Add(newEntry);
        UpdateScoreboard();
    }

    public void OnSubmitScoreButtonPressed()
    {
        if (playerScoreEntryUI != null)
        {
            string playerName = playerScoreEntryUI.GetInputName();
            int score = playerScoreEntryUI.GetScore();

            // Remove the entry (or entries) that are marked as input entries.
            entries.RemoveAll(e => e.isInputEntry);

            AddScoreEntry(playerName, score);

            UpdateScoreboard();

            submitScoreButton.SetActive(false);

            SaveScoreboardData();
        }
    }

    public void UpdateScoreboard()
    {
        entries = entries.OrderByDescending(entry => entry.score).ToList();

        // Assign ranks
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].rank = i + 1;
        }

        // Clear the current scoreboard.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < entries.Count && i < maxEntries; i++)
        {
            GameObject prefabToInstantiate = entries[i].isInputEntry ? scoreEntryInputPrefab : scoreEntryPrefab;
            GameObject newEntryObject = Instantiate(prefabToInstantiate, transform);
            newEntryObject.GetComponent<ScoreEntryUI>().SetScoreEntry(entries[i]);

            if (entries[i].isInputEntry)
            {
                playerScoreEntryUI = newEntryObject.GetComponent<ScoreEntryInputUI>();
            }
        }
    }

    public void SaveScoreboardData()
    {
        Data.Game.ScoreboardEntries = entries.Select(e => new ScoreEntryData { name = e.name, score = e.score }).ToList();
        Data.Save();
    }

    public void LoadScoreboardData()
    {
        entries = Data.Game.ScoreboardEntries.Select(e => new ScoreEntry { name = e.name, score = e.score }).ToList();
        UpdateScoreboard();
    }

    [ContextMenu("Delete Scoreboard Data")]
    public void DeleteScoreboardData()
    {
        entries.Clear();
        SaveScoreboardData();
    }
}
