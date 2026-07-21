using UnityEngine;

public class RelayLeaderboardData : MonoBehaviour
{
    public TMPro.TextMeshProUGUI title, names, scores;
    public TMPro.TextMeshProUGUI fname, fscore;
    
    private bool _isDone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GatherLeaderboardOnce();
        GatherLeaderboard(1);
    }

    public void RegisterProxy()
    {
        if (test.instance != null)
        {
            test.instance.RegisterEntry();
            test.instance.LoadData();
            
            GatherLeaderboardOnce();
            GatherLeaderboard(1);
        }
        else Debug.LogError("Error!");
    }
    
    void GatherLeaderboard(int startFrom)
    {
        title.text = "Mejores 5:";
        
        if (test.instance != null)
        {
            if (names.text != "")
            {
                names.text = "";
            }

            if (scores.text != "")
            {
                scores.text = "";
            }
            
            for (int i = startFrom; i < test.instance.boardEntries.Count; i++)
            {
                names.text += $"{test.instance.boardEntries[i].playerName},\n";
                scores.text += $"{test.instance.boardEntries[i].savedScore}\n";
            }
        }
    }
    
    void GatherLeaderboardOnce()
    {
        title.text = "Mejores 5:";
        
        if (test.instance != null)
        {
            fname.text = $"{test.instance.boardEntries[0].playerName},";
            fscore.text = $"{test.instance.boardEntries[0].savedScore}";
        }
    }
}
