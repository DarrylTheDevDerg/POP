using System;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public PowerUp powerUp;
    public TMPro.TextMeshPro text;

    void Start()
    {
        powerUp = (PowerUp)UnityEngine.Random.Range(1, Enum.GetValues(typeof(PowerUp)).Length);
        text.text = $"{powerUp.ToString()}";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
