using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public MainGame mainGame;
    public PlayerManager player;
    
    private Transform _transform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainGame = FindFirstObjectByType<MainGame>();
        player = FindFirstObjectByType<PlayerManager>();

        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            switch (player.GetCurrentPowerUp())
            {
                case PowerUp.Faster:
                    if (player.RelayBoost()) _transform.position += Vector3.left * (0.095f * (mainGame.speed * 12.5f) * Time.deltaTime);
                    else _transform.position += Vector3.left * (0.005f * (mainGame.speed * 1.5f) * Time.deltaTime);
                    break;
                    
                case PowerUp.Slow:
                    _transform.position += Vector3.left * (0.015f * (mainGame.speed * 1.25f) * Time.deltaTime);
                    break;
                
                case PowerUp.None:
                    _transform.position += Vector3.left * (0.075f * (mainGame.speed * 1.75f) * Time.deltaTime);
                    break;
                
                default:
                    _transform.position += Vector3.left * (0.075f * (mainGame.speed * 1.75f) * Time.deltaTime);
                    break;
            }
        }
    }
}
