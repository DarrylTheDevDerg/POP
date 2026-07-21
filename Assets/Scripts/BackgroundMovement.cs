using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float amplitude = 0.25f;
    
    private float _frequency = 0.25f;
    private Vector2 _startPosition;
    private MainGame _mg;
    private float _s;
    private PlayerManager _p;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPosition = transform.position;
        _mg = FindFirstObjectByType<MainGame>();
        _p = FindFirstObjectByType<PlayerManager>();
        
        _s = Random.Range(0.01f, 0.045f);
        _frequency = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            switch (_p.GetCurrentPowerUp())
            {
                case PowerUp.Faster:
                    if (_p.RelayBoost()) transform.position += Vector3.left * (_s / 1.75f * (_mg.speed * 100.25f) * Time.deltaTime);
                    else transform.position += Vector3.left * (_s / 2.75f * (_mg.speed * 0.15f) * Time.deltaTime);
                    break;
                
                case PowerUp.Slow:
                    transform.position += Vector3.left * (_s / 2.75f * (_mg.speed * 0.25f) * Time.deltaTime);
                    break;
                
                case PowerUp.None:
                    transform.position += Vector3.left * (_s / 1.75f * (_mg.speed * 1.25f) * Time.deltaTime);
                    break;
                    
                default:
                    transform.position += Vector3.left * (_s / 1.75f * (_mg.speed * 1.25f) * Time.deltaTime);
                    break;
            }
            
            float y = Mathf.Sin(Time.time / _frequency) * amplitude / 3;
        
            transform.position = new Vector3(transform.position.x, _startPosition.y + y, -0.7f);
            //transform.position = new Vector2(x, _startPosition.y);
        }
    }
}
