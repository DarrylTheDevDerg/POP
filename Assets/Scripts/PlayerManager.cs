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
    public int score, combo;
    public TextMeshProUGUI scoreText, powerText, comboText, multiText;
    
    public bool hasBeenHit = false;
    
    private Rigidbody2D _rb;
    private bool _isPowerActive;
    private PowerUp _currentPowerUp = PowerUp.None;
    private float _powahTimer, _comboTimer;
    private int multiplier = 1;
    private PlayerControls _controls;

    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Enable();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString("D9");
        
        if (Time.timeScale > 0f)
        {
            ModifyScore(1 * (multiplier + (combo / 9)));
            
            if (_controls.Player.Jump.IsPressed() && !hasBeenHit)
            {
                MoveUpwards();
            }
        
            if (_powahTimer >= 0f && _isPowerActive) _powahTimer -= Time.deltaTime;

            if (_powahTimer < 3.5f)
            {
                Color change = new Color(1, 1, 1, 1);
                change.a = Mathf.PingPong(_powahTimer, 1f);
                powerText.color = change;
            }

            if (_powahTimer <= 0f)
            {
                _isPowerActive = false;
                _currentPowerUp = PowerUp.None;
                powerText.text = " ";
                powerText.gameObject.SetActive(false);
            }
        
            if (_comboTimer > 0f)
            {
                _comboTimer -= Time.deltaTime;
                
                if (combo > 1) ComboChain();
            }
        }
    }

    public void MoveUpwards()
    {
        _rb.AddForce((transform.up + Vector3.up) * 4.5f);
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
                Color change = new Color(1, 1, 1, 1);
                powerText.color = change;
                ModifyScore(100 * (multiplier + (combo / 6)));
                Destroy(other.gameObject);
                break;
            
            case "Obstacle":
                if (_isPowerActive && _currentPowerUp != PowerUp.Invincibility)
                {
                    _powahTimer = 0f;
                    _isPowerActive = false;
                }
                else if (_isPowerActive && _currentPowerUp == PowerUp.Invincibility)
                {
                    Destroy(other.gameObject);
                    ModifyScore(150 * (multiplier + (combo / 9)));
                }
                else
                {
                    _controls.Disable();
                    CommitDie();
                }
                break;
            
            case "Collectible":
                _comboTimer = 7.5f;
                Color org = new Color(1, 1, 1, 1);
                ModifyScore(other.gameObject.GetComponent<Collectible>().value * (multiplier + (combo / 9)));
                
                if (_comboTimer > 0f)
                {
                    comboText.color = org;
                    combo++;
                }
                
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
                multiplier = 1;
                break;
            
            case PowerUp.Multiplier:
                multiplier = 2;
                break;
            
            case PowerUp.Faster:
                multiplier = 4;
                break;
        }
    }

    public void ComboChain()
    {
        comboText.gameObject.SetActive(true);
        comboText.text = $"¡COMBO de {combo}!";

        if (_comboTimer <= 2f)
        {
            Color change = new Color(1, 1, 1, 1);
            change.a = Mathf.PingPong(_comboTimer, 1f);
            comboText.color = change;
        }

        if (_comboTimer <= 0)
        {
            combo = 0;
            comboText.gameObject.SetActive(false);
        }
    }

    public PowerUp GetCurrentPowerUp()
    {
        return _currentPowerUp;
    }
}
