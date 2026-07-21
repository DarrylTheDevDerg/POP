using System;
using UnityEngine;

[Serializable]
public class PowerUpSprite
{
    public PowerUp Name;
    public Sprite SpriteToDisplay;
}

public class PowerUpItem : MonoBehaviour
{
    public PowerUp powerUp;
    public PowerUpSprite[] powerUpSprites;
    public SpriteRenderer renderer;
    

    void Start()
    {
        powerUp = (PowerUp)UnityEngine.Random.Range(1, Enum.GetValues(typeof(PowerUp)).Length);

        foreach (PowerUpSprite sprite in powerUpSprites)
        {
            if (sprite.Name == powerUp)
            {
                renderer.sprite = sprite.SpriteToDisplay;
            }
        }
    }
}
