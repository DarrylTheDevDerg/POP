using UnityEngine;

public class Afterimage : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float lifetime = 0.3f;

    private float timer;

    private Color startColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite sprite, Color color, Vector3 scale, bool flipX, bool flipY)
    {
        sr.sprite = sprite;
        sr.color = color;
        transform.localScale = scale;

        sr.flipX = flipX;
        sr.flipY = flipY;

        startColor = color;
        timer = lifetime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        Color c = startColor;
        c.a = Mathf.Clamp01(timer / lifetime);

        sr.color = c;

        if (timer <= 0)
            Destroy(gameObject);
    }
}