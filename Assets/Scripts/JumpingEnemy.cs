using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    public float speed;
    public float thld;
    public float minThld, maxThld, minSpd, maxSpd;
    public AnimationCurve path;
    public int points = 50;
    public float previewDuration;
    public ParticleSystem rise, splash;

    private float _elapsed, _currentTime;
    private Vector3 _startPos;
    private LineRenderer _lineRenderer;
    private MainGame _g;
    private PlayerManager _p;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _lineRenderer = GetComponent<LineRenderer>();
        thld = Random.Range(minThld, maxThld);
        speed = Random.Range(minSpd, maxSpd);
        
        _g = FindFirstObjectByType<MainGame>();
        _p = FindFirstObjectByType<PlayerManager>();
        
        _lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > thld / 3.75)
        {
            _lineRenderer.enabled = true;
            DrawPath();
        }
        
        if (_currentTime >= thld)
        {
            _lineRenderer.enabled = false;
            rise.gameObject.SetActive(true);
            
            _elapsed += Time.deltaTime;

            float x = 0;

            switch (_p.GetCurrentPowerUp())
            {
                case PowerUp.Faster:
                    if (_p.RelayBoost()) x = _startPos.x - _elapsed * (speed * _g.speed / (0.125f * _g.speed));
                    else x = _startPos.x - _elapsed * (speed * _g.speed / 12.75f);
                    break;
                
                case PowerUp.Slow:
                    x = _startPos.x - _elapsed * (speed * _g.speed / (6.75f * _g.speed));
                    break;
                
                case PowerUp.None:
                    x = _startPos.x - _elapsed * (speed * _g.speed / (3.75f * _g.speed));
                    break;
                
                default:
                    x = _startPos.x - _elapsed * (speed * _g.speed / (3.75f * _g.speed));
                    break;
            }
            
            float y = _startPos.y + path.Evaluate(_elapsed);
        
            transform.position = new Vector3(x, y, _startPos.z);
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    void DrawPath()
    {
        _lineRenderer.positionCount = points;

        for (int i = 0; i < points; i++)
        {
            float t =  i / (float)(points - 1) * previewDuration;

            float x = 0;
            
            switch (_p.GetCurrentPowerUp())
            {
                case PowerUp.Faster:
                    if (_p.RelayBoost()) x = _startPos.x - t * (speed * _g.speed / (0.125f * _g.speed));
                    else x = _startPos.x - t * (speed * _g.speed / 12.75f);
                    break;
                
                case PowerUp.Slow:
                    x = _startPos.x - t * (speed * _g.speed / (6.75f * _g.speed));
                    break;
                
                case PowerUp.None:
                    x = _startPos.x - t * (speed * _g.speed / (3.75f * _g.speed));
                    break;
                
                default:
                    x = _startPos.x - t * (speed * _g.speed / (3.75f * _g.speed));
                    break;
            }
            
            float y = _startPos.y + path.Evaluate(t);
            
            _lineRenderer.SetPosition(i, new Vector3(x, y, _startPos.z));
        }
    }
}
