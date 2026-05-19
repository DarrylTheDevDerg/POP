using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float amplitude = 0.25f;
    
    private float _frequency = 0.25f;
    private Vector2 _startPosition;
    private MainGame _mg;
    private float _s;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPosition = transform.position;
        _mg = FindFirstObjectByType<MainGame>();
        _s = Random.Range(0.01f, 0.045f);
        _frequency = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (_s / 1.75f) * (_mg.speed / 1.75f);
        
        float y = Mathf.Sin(Time.time / _frequency) * amplitude / 3;
        
        transform.position = new Vector2(transform.position.x, _startPosition.y + y);
        //transform.position = new Vector2(x, _startPosition.y);
    }
}
