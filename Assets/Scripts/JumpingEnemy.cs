using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    public float speed;
    public float thld;
    public float minThld, maxThld;
    public AnimationCurve path;

    private float _elapsed, _currentTime;
    private Vector3 _startPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        thld = Random.Range(minThld, maxThld);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime >= thld)
        {
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
}
