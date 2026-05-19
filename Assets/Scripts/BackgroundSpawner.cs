using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject bg;
    
    private BoxCollider2D _collider;
    private float _th = 5;
    private float _t, _ch;
    private int _qt = 2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _ch = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;

        if (_t >= _th)
        {
            _t = 0;
            _ch = Random.Range(0, 100);
        }

        if (_ch >= 55)
        {
            _ch = 0;
            _th = Random.Range(1, 10);

            for (int i = 0; i < _qt; i++)
            {
                Spawn(_collider);
            }
            
            _qt = Random.Range(1, 5);
        }
    }
    
    void Spawn(Collider2D spawn)
    {
        Vector2 position = GetRandomSpawnPosition(spawn);
        Instantiate(bg, position, Quaternion.identity);
    }
    
    Vector2 GetRandomSpawnPosition(Collider2D coll)
    {
        Vector2 center = (Vector2)coll.transform.position + coll.offset;
        Vector2 size = coll.bounds.size;

        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float y = Random.Range(center.y  - size.y / 2, center.y + size.y / 2);
        
        return new Vector2(x, y);
    }
}
