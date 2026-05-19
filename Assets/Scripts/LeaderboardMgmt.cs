using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardMgmt : MonoBehaviour
{
    public TMP_Text leaderboardText;
    public PlayerManager pM;
    
    public int maxEntries;
    [SerializeField] public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    public bool doStart = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pM = FindFirstObjectByType(typeof(PlayerManager)) as PlayerManager;
        
        if (doStart) UpdateUI();
    }
    
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

    public void UpdateUI()
    {
        leaderboardText.text = "";

        for (int i = 0; i < entries.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {entries[i].name} - {entries[i].score.ToString("D7")}\n";
        }
    }
}
