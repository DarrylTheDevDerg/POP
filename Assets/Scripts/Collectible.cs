using UnityEngine;

public class Collectible : MonoBehaviour
{
    public PlayerManager player;
    public int value;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void Update()
    {
        if (player.GetCurrentPowerUp() == PowerUp.Magnet && Time.timeScale > 0)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), 15f * Time.deltaTime + 1f + Time.deltaTime); 
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && player.GetCurrentPowerUp() != PowerUp.Magnet)
        {
            Destroy(gameObject);
        }
    }
}
