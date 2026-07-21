using UnityEngine;

public class AfterimageSpawner : MonoBehaviour
{
    [SerializeField] private Afterimage prefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float interval = 0.05f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Spawn();
            timer = interval;
        }
    }

    void Spawn()
    {
        var ghost = Instantiate(prefab, transform.position, transform.rotation);

        ghost.Initialize(
            spriteRenderer.sprite,
            spriteRenderer.color,
            transform.localScale,
            spriteRenderer.flipX,
            spriteRenderer.flipY
        );
    }
}