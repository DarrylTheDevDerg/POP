using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public Leaderboard leaderboard;
    public TMP_Text leaderboardText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        leaderboardText.text = "";

        for (int i = 0; i < leaderboard.entries.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {leaderboard.entries[i].name} - {leaderboard.entries[i].score.ToString("D7")}\n";
        }
    }
}
