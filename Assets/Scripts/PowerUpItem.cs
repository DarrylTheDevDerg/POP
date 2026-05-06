using System;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public PowerUp powerUp;
    public TMPro.TextMeshPro text;

    void Start()
    {
        powerUp = (PowerUp)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PowerUp)).Length);
        text.text = $"{powerUp.ToString()}";
    }
}
