using UnityEngine;

public class BackgroundSprite : MonoBehaviour
{
    public Sprite[] sprites;
    
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int flip = Random.Range(1, 100);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        if (flip > 50)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
