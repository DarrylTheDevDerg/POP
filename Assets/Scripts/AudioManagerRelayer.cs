using UnityEngine;

public class AudioManagerRelayer : MonoBehaviour
{
    public void RelayBGMFadeOut()
    {
        AudioManager.instance.BGMFadeOut();
    }
}
