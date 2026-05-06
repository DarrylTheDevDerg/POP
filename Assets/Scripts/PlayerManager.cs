using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum PowerUp
{
    None,
    Magnet,
    Faster,
    Slow,
    Multiplier,
    Invincibility
}

public class PlayerManager : MonoBehaviour
{
    public KeyCode[] controls;
    public int score;
    public TextMeshProUGUI scoreText, powerText;
    
    public bool hasBeenHit = false;
    
    private Rigidbody2D _rb;
    private bool _isPowerActive;
    private PowerUp _currentPowerUp = PowerUp.None;
    private float _powahTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ModifyScore(1);
        scoreText.text = score.ToString("D7");
        
        foreach (KeyCode key in controls)
        {
            if (Input.GetKey(key))
            {
                MoveUpwards();
            }
        }
        
        if (_powahTimer >= 0f && _isPowerActive) _powahTimer -= Time.deltaTime;

        if (_powahTimer <= 0f)
        {
            _isPowerActive = false;
            _currentPowerUp = PowerUp.None;
            powerText.text = " ";
            powerText.gameObject.SetActive(false);
        }
    }

    public void MoveUpwards()
    {
        _rb.AddForce((transform.up + Vector3.up) * 2);
    }

    public int ModifyScore(int mod)
    {
        score += mod;
        return score;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Power":
                GetPowerUp(other.gameObject.GetComponent<PowerUpItem>().powerUp);
                powerText.gameObject.SetActive(true);
                powerText.text = other.gameObject.GetComponent<PowerUpItem>().powerUp.ToString();
                ModifyScore(100);
                Destroy(other.gameObject);
                break;
            
            case "Obstacle":
                if (_isPowerActive)
                {
                    _powahTimer = 0f;
                    _isPowerActive = false;
                }
                else
                {
                    CommitDie();
                }
                break;
            
            case "Collectible":
                ModifyScore(other.gameObject.GetComponent<Collectible>().value);
                Destroy(other.gameObject);
                break;
        }
    }

    public void GetPowerUp(PowerUp p)
    {
        _currentPowerUp = p;
        _isPowerActive = true;
        _powahTimer = 15f;
    }

    public void CommitDie()
    {
        hasBeenHit = true;
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("02_GameOver");
    }

    public void PowerEffect()
    {
        switch (_currentPowerUp)
        {
            case PowerUp.None:
                break;
            
            case PowerUp.Magnet:
                break;
        }
    }
}
