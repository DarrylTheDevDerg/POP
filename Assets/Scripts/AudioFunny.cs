using UnityEngine;
using UnityEngine.Serialization;

public class AudioFunny : MonoBehaviour
{
    public float slowPitch = 0.5f;
    public float quickPitch = 1.5f;

    [FormerlySerializedAs("isSFX")] public bool isSfx;
    public bool letItDrop;
    
    private bool _slowDown = false;
    private bool _speedUp = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!letItDrop)
        {
            FrequencyManager();
        }
        else
        {
            AudioManager.instance.SetMusicPitch(AudioManager.instance.GetPitch("music") - 0.001f);
        }
    }

    public void FrequencyManager()
    {
        if (AudioManager.instance.GetPitch("music") <= 1f)
        {
            AudioManager.instance.SetMusicPitch(AudioManager.instance.GetPitch("music") + 0.005f);
        }
        else
        {
            AudioManager.instance.SetMusicPitch(AudioManager.instance.GetPitch("music") - 0.005f);
        }
        
        if (_slowDown)
        {
            if (isSfx)
            {
                AudioManager.instance.SetSfxPitch(slowPitch);
            }
            else
            {
                AudioManager.instance.SetMusicPitch(slowPitch);
            }
        }
        else if (_speedUp)
        {
            if (isSfx)
            {
                AudioManager.instance.SetSfxPitch(quickPitch);
            }
            else
            {
                AudioManager.instance.SetMusicPitch(quickPitch);
            }
        }
    }

    public void ChangePitch(int value)
    {
        switch (value)
        {
            case 0:
                _slowDown = true;
                _speedUp = false;
                break;
            
            case 1:
                _speedUp = true;
                _slowDown = false;
                break;
            
            default:
                _speedUp = false;
                _slowDown = false;
                break;
        }
    }
}
