using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public enum PowerUp
{
    None,
    Magnet,
    Faster,
    Slow,
    Multiplier,
    Invincibility
}

[System.Serializable]
public class SFXEffect
{
    public string effectName;
    public AudioClip clip;
}

public class PlayerManager : MonoBehaviour
{
    public int score, combo;
    public TextMeshProUGUI scoreText, comboText, multiText;
    
    public bool hasBeenHit = false;
    
    [NonReorderable]
    [Header("Do NOT modify unless it's really needed to.")]
    public List<SFXEffect> effects =  new()
    {
        new SFXEffect
        {
            effectName = "Flap",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Hit",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Power-Up",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Obstacle Break",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Hit (Power-Up)",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Collect",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Charge",
            clip = null
        },
        new SFXEffect
        {
            effectName = "Flare",
            clip = null
        }
    };
    
    private Rigidbody2D _rb;
    private bool _isPowerActive, _isBoost, _inv;
    private PowerUp _currentPowerUp = PowerUp.None;
    private float _powahTimer, _comboTimer, _invTimer, _invF = 0.1f;
    private float _cRotation, _holdR;
    
    public int multiplier = 1;
    public PlayerControls _controls;
    public UnityEvent onDeath;
    public RectTransform ui;

    public GameObject invParticles;
    public GameObject fastParticles;
    public GameObject magnetParticles;
    public GameObject speedFlare;

    private Collider2D _c;
    private TutorialIdle _t;
    private AudioFunny _fun;
    private SpriteRenderer _r;

    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Enable();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _c = GetComponent<Collider2D>();
        _r = GetComponent<SpriteRenderer>();
        
        _t = FindFirstObjectByType<TutorialIdle>();
        _fun = FindFirstObjectByType<AudioFunny>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        PowerEffect();
        WhichParticles();
        
        if (Time.timeScale > 0f && !_t.inTutoriel)
        {
            if (!hasBeenHit && !_t.inTutoriel) if (!_isBoost) ModifyScore(1 * multiplier); else ModifyScore(5 * multiplier);
            
            if (_controls.Player.Flap.WasPressedThisFrame() && !hasBeenHit && !_t.inTutoriel && _currentPowerUp != PowerUp.Faster) MoveUpwards();
        
            if (_powahTimer >= 0f && _isPowerActive) _powahTimer -= Time.deltaTime;

            if (_powahTimer <= 0f && _isPowerActive)
            {
                _fun.ChangePitch(2);
                _isPowerActive = false;
                _currentPowerUp = PowerUp.None;
                ApplyInvulnerability();
            }
        
            if (_comboTimer > 0f)
            {
                _comboTimer -= Time.deltaTime;
                
                if (combo > 4) ComboChain();
            }
            
            transform.rotation = Quaternion.Euler(0f, 0f, _cRotation);

            if (_cRotation > -65f && _holdR <= 0f) _cRotation -= 0.45f;
            else if (_currentPowerUp == PowerUp.Faster)
            {
                _holdR = 0.1f;
                _cRotation = Mathf.MoveTowards(_cRotation, 0f, 0.45f);
                multiplier = 7;
            }

            if (_currentPowerUp == PowerUp.Slow)
            {
                AfterimageSpawner[] images = FindObjectsOfType<AfterimageSpawner>();
                foreach (AfterimageSpawner i in images)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    i.GetComponent<AfterimageSpawner>().enabled = true;
                }
            }
            else
            {
                AfterimageSpawner[] images = FindObjectsOfType<AfterimageSpawner>();
                
                foreach (AfterimageSpawner i in images)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    i.GetComponent<AfterimageSpawner>().enabled = false;
                }
            }

            if (_holdR > 0f) _holdR -= Time.deltaTime;
            
            if (_invTimer > 0f) _invTimer -= Time.deltaTime;
        }
    }

    public void MoveUpwards()
    {
        _holdR = 0.375f;
        _cRotation = 38.5f;
        DoSFX("Flap");
        _rb.linearVelocity = Vector2.up * 6.75f;
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

                switch (other.gameObject.GetComponent<PowerUpItem>().powerUp)
                {
                    case PowerUp.Magnet:
                        _fun.ChangePitch(2);
                        break;
                    
                    case PowerUp.Faster:
                        _fun.ChangePitch(1);
                        break;
                    
                    case PowerUp.Invincibility:
                        _fun.ChangePitch(2);
                        break;
                    
                    case PowerUp.Multiplier:
                        _fun.ChangePitch(2);
                        break;
                    
                    case PowerUp.Slow:
                        _fun.ChangePitch(0);
                        break;
                }
                
                // powerText.text = other.gameObject.GetComponent<PowerUpItem>().powerUp.ToString();
                
                ModifyScore(100 * multiplier);
                
                if (other.gameObject.GetComponent<PowerUpItem>().powerUp == PowerUp.Faster) StartCoroutine(SpeedPower());
                
                Destroy(other.gameObject);
                DoSFX("Power-Up");
                break;
            
            case "Obstacle":
                if (_isPowerActive && _currentPowerUp != PowerUp.Invincibility)
                {
                    DoSFX("Hit (Power-Up)");
                    _fun.ChangePitch(2);
                    _powahTimer = 0f;
                    _isPowerActive = false;
                    _currentPowerUp = PowerUp.None;
                    ApplyInvulnerability();
                }
                else if (_isPowerActive && _currentPowerUp == PowerUp.Invincibility)
                {
                    DoSFX("Obstacle Break");
                    Destroy(other.gameObject);
                    ModifyScore(150 * multiplier);
                }
                else if (_invTimer <= 0f)
                {
                    _controls.Disable();
                    CommitDie();
                }
                break;
            
            case "Collectible":
                DoSFX("Collect");
                
                _comboTimer = 3.125f;
                Color org = new Color(1, 1, 1, 1);
                ModifyScore(other.gameObject.GetComponent<Collectible>().value * multiplier);
                
                if (_comboTimer > 0f)
                {
                    comboText.color = org;
                    combo += 1 * multiplier;
                }
                
                Destroy(other.gameObject);
                break;
        }
    }

    public void GetPowerUp(PowerUp p)
    {
        _currentPowerUp = p;
        _isPowerActive = true;
        _powahTimer = 19.5f;
    }

    public void CommitDie()
    {
        _c.enabled = false;
        hasBeenHit = true;
        test.instance.KeepScore(score);
        DoSFX("Hit");
        _fun.letItDrop = true;
        
        StartCoroutine(TurnRed(0.1f));
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2.05f);
        onDeath.Invoke();
    }

    public IEnumerator SpeedPower()
    {
        _rb.Sleep();
        _c.enabled = false;
        
        DoSFX("Charge");
        
        yield return new WaitForSeconds(1.5f);
        
        _isBoost = true;
        speedFlare.gameObject.SetActive(true);
        fastParticles.gameObject.SetActive(true);
        DoSFX("Flare");
        StartCoroutine(ShakeCam(0.1f, 0.15f, true, false));
        StartCoroutine(ShakeUI(0.1f, 0.15f, true, false));
        
        yield return new WaitForSecondsRealtime(4.25f);
        
        speedFlare.gameObject.SetActive(false);
        fastParticles.gameObject.SetActive(false);
        
        _isBoost = false;
        multiplier = 1;
        
        _rb.WakeUp();
        _c.enabled = true;
        
        _fun.ChangePitch(2);
        _powahTimer = 0f;
        _isPowerActive = false;
        _currentPowerUp = PowerUp.None;
        _invTimer = 3.5f;
    }

    public void PowerEffect()
    {
        switch (_currentPowerUp)
        {
            case PowerUp.Multiplier:
                multiplier = 2;
                MultiplierDisplay(2);
                break;
            
            case PowerUp.Slow:
                multiplier = 4;
                MultiplierDisplay(4);
                break;
            
            default:
                multiplier = 1;
                MultiplierDisplay(1);
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
            if (combo >= 5) ModifyScore((50 * combo) * multiplier);
            
            combo = 0;
            comboText.gameObject.SetActive(false);
        }
    }

    public void MultiplierDisplay(int value)
    {
        multiText.gameObject.SetActive(true);
        multiText.text = $"¡MULTIPLICADOR DE: {value}x!";

        if (value == 1)
        {
            multiText.gameObject.SetActive(false);
        }
    }

    public PowerUp GetCurrentPowerUp()
    {
        return _currentPowerUp;
    }

    public void WhichParticles()
    {
        switch (_currentPowerUp)
        {
            case PowerUp.Magnet:
                magnetParticles.gameObject.SetActive(true);
                invParticles.gameObject.SetActive(false);
                break;
            
            case PowerUp.Invincibility:
                magnetParticles.gameObject.SetActive(false);
                invParticles.gameObject.SetActive(true);
                break;
            
            case PowerUp.None:
                magnetParticles.gameObject.SetActive(false);
                invParticles.gameObject.SetActive(false);
                break;
            
            default:
                magnetParticles.gameObject.SetActive(false);
                invParticles.gameObject.SetActive(false);
                break;
        }
    }

    public void RelayAudioPitchFunction()
    {
        AudioManager.instance.ResetPitch();
    }

    public void RelayAudioStop()
    {
        AudioManager.instance.StopMusic();
    }

    public bool RelayBoost()
    {
        return _isBoost;
    }

    public void DoSFX(string name)
    {
        foreach (SFXEffect e in effects)
        {
            if (name == e.effectName) AudioManager.instance.PlaySFX(e.clip);
        }
    }

    public void ApplyInvulnerability()
    {
        _invTimer = 3.5f;

        StartCoroutine(InvulFrames());
    }
    
    IEnumerator InvulFrames()
    {
        _inv = true;

        Color color = _r.color;
        float elapsed = 0f;

        while (elapsed < _invTimer)
        {
            color.a = Mathf.Approximately(color.a, 1f) ? 0.3f : 1f;
            
            _r.color = color;

            yield return new WaitForSeconds(_invF);
            elapsed += _invF;
        }

        color.a = 1f;
        _r.color = color;
        _inv = false;
    }

    IEnumerator TurnRed(float fade)
    {
        Color orig = _r.color;
        Color target = Color.red;

        float elapsed = 0f;

        while (elapsed < fade)
        {
            _r.color = Color.Lerp(orig, target, elapsed / fade);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        _r.color = target;
    }

    IEnumerator ShakeCam(float duration, float strength, bool shakeX, bool shakeY)
    {
        Camera cam = FindFirstObjectByType<Camera>();
        Vector3 origPos = cam.transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = shakeX ? Random.Range(-strength, strength) : 0f;
            float y = shakeY ? Random.Range(-strength, strength) : 0f;

            cam.transform.position = origPos + new Vector3(x, y, 0f);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        cam.transform.position = origPos;
    }

    IEnumerator ShakeUI(float duration, float strength, bool shakeX, bool shakeY)
    {
        RectTransform t = ui;
        Vector2 orig = t.anchoredPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = shakeX ? Random.Range(-strength, strength) : 0f;
            float y = shakeY ? Random.Range(-strength, strength) : 0f;

            t.anchoredPosition = orig + new Vector2(x, y);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        t.anchoredPosition = orig;
    }
}
