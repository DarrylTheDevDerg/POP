using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Sprite[] numberSprites;
    public Image[] numberDisplays;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int score)
    {
        string scoreString = score.ToString();

        foreach (Image img in numberDisplays)
        {
            img.gameObject.SetActive(false);
        }

        if (scoreString.Length > numberDisplays.Length)
        {
            return;
        }

        for (int i = 0; i < scoreString.Length; i++)
        {
            int digit = scoreString[i] - '0';
            
            numberDisplays[i].sprite = numberSprites[digit];
            numberDisplays[i].gameObject.SetActive(true);
        }
    }
}
