using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    public float speed;
    public float thld;
    public float minThld, maxThld, minSpd, maxSpd;
    public AnimationCurve path;
    public int points = 50;
    public float previewDuration;

    private float _elapsed, _currentTime;
    private Vector3 _startPos;
    private LineRenderer _lineRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _lineRenderer = GetComponent<LineRenderer>();
        thld = Random.Range(minThld, maxThld);
        speed = Random.Range(minSpd, maxSpd);
        
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
            
            _elapsed += Time.deltaTime;
        
            float x = _startPos.x - _elapsed * speed;
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
            
            float x = _startPos.x - t * speed;
            float y = _startPos.y + path.Evaluate(t);
            
            _lineRenderer.SetPosition(i, new Vector3(x, y, _startPos.z));
        }
    }
}
