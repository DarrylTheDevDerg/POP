using UnityEngine;

public class AudioManagerOnDemand : MonoBehaviour
{
    public AudioClip clip;
    
    public bool onStart;
    public bool isBGM;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (onStart)
        {
            OnDemand();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDemand()
    {
        if (isBGM)
        {
            AudioManager.instance.PlayMusic(clip);
        }
        else
        {
            AudioManager.instance.PlaySFX(clip);
        }
    }
}
