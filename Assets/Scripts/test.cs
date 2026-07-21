using UnityEngine;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class BoardEntry
{
    public string playerName;
    public float savedScore;
}

[System.Serializable]
public class BoardData
{
    public List<BoardEntry> entries = new();
}

public class test : MonoBehaviour
{
    public static test instance;

    public float relayedScore;
    
    public TMPro.TMP_InputField inputField;
    [SerializeField] public List<BoardEntry> boardEntries = new List<BoardEntry>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("Leaderboard"))
            {
                CreateDummyData();
            }
            
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    void Update()
    {
        inputField = FindFirstObjectByType<TMPro.TMP_InputField>();
    }

    public void KeepScore(int value)
    {
        relayedScore = value;
    }

    public void RegisterEntry()
    {
        if (inputField.text != null || inputField.text != " ")
        {
            boardEntries.Add(new BoardEntry
            {
                playerName = inputField.text.ToUpper(),
                savedScore = relayedScore
            });

            inputField.interactable = false;
        
            SaveData();
            LoadData();
        }
    }

    public void SaveData()
    {
        BoardData bD =  new BoardData
        {
            entries = boardEntries
        };
        
        string json = JsonUtility.ToJson(bD);
        
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (!PlayerPrefs.HasKey("Leaderboard")) return;
        
        string json = PlayerPrefs.GetString("Leaderboard");
        BoardData bD = JsonUtility.FromJson<BoardData>(json);
        
        boardEntries = bD.entries ?? new List<BoardEntry>();
        
        boardEntries.Sort((a, b) => b.savedScore.CompareTo(a.savedScore));
        
        if (boardEntries.Count > 5) boardEntries.RemoveRange(5, boardEntries.Count - 5);
    }

    public void CreateDummyData()
    {
        boardEntries = new List<BoardEntry>
        {
            new BoardEntry{playerName = "AAA", savedScore = 5000},
            new BoardEntry{playerName = "DEV", savedScore = 4500},
            new BoardEntry{playerName = "TEST", savedScore = 4000},
            new BoardEntry{playerName = "HELO", savedScore = 3500},
            new BoardEntry{playerName = "WRLD", savedScore = 3000}
        };
        
        SaveData();
    }

    public void WipeBoardData()
    {
        PlayerPrefs.DeleteKey("Leaderboard");
        
        CreateDummyData();
    }

    public void WipeTutorialData()
    {
        PlayerPrefs.DeleteKey("Seen Tutorial");
    }
}
