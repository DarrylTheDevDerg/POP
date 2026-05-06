using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class LeaderboardEntry
{
    public string name;
    public int score;
}

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

public class Leaderboard : MonoBehaviour
{
    public int maxEntries;
    [SerializeField] public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    public void AddScore(string player, int score)
    {
        LeaderboardEntry entry = new LeaderboardEntry();
        {
            entry.name = player;
            entry.score = score;
        }
        
        entries.Add(entry);
        
        entries.Sort((x, y) => y.score.CompareTo(x.score));

        if (entries.Count > maxEntries)
        {
            entries.RemoveRange(maxEntries, entries.Count - maxEntries);
        }
    }

    public void SaveScores()
    {
        LeaderboardData data = new LeaderboardData();
        data.entries = entries;
        
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        if (!PlayerPrefs.HasKey("Leaderboard"))
        {
            return;
        }
        
        string json = PlayerPrefs.GetString("Leaderboard");
        LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
        
        entries = data.entries ?? new List<LeaderboardEntry>();
    }
}
