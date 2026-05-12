using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public MainGame mainGame;
    public PlayerManager player;
    
    private Transform _transform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainGame = FindObjectOfType<MainGame>();
        player = FindObjectOfType<PlayerManager>();
        
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            switch (player.GetCurrentPowerUp())
            {
                case PowerUp.Slow:
                    _transform.localPosition -= new Vector3(0.0325f * (mainGame.speed / (6f * mainGame.speed)), 0, 0);
                    break;
                
                case PowerUp.Faster:
                    _transform.localPosition -= new Vector3(0.095f * (mainGame.speed / (2.5f * mainGame.speed)), 0, 0);
                    break;
                
                default:
                    _transform.localPosition -= new Vector3(0.065f * (mainGame.speed / (3 * mainGame.speed)), 0, 0);
                    break;
            }
            
        }
    }
}
