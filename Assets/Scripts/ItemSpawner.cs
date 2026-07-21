using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public MainGame mgmt;
    public BoxCollider2D itemSpawner, obsSpawner, fishSpawner;
    
    public GameObject[] itemPrefabs;
    public GameObject[] obsPrefabs;
    public GameObject fishPrefab;
    public float maxSpeed = 20f;

    private float _objTimer, _obsTimer, _fishT, difficulty;
    private PauseMenu _pauseMenu;
    private TutorialIdle _t;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mgmt = FindFirstObjectByType<MainGame>();
        _t = FindFirstObjectByType<TutorialIdle>();
        
        _objTimer = Random.Range(15, 35);
        _obsTimer = Random.Range(30, 85);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0 && !_t.inTutoriel)
        {
            difficulty = Mathf.Clamp01(mgmt.speed / maxSpeed);
            
            float itemInterval = Mathf.Lerp(35.5f, 1.5f, difficulty / 2);
            float obsInterval = Mathf.Lerp(12.75f, 0.8f, difficulty / 2);
            float fishInterval = Mathf.Lerp(12.5f, 2.5f, difficulty / 2);
            
            _objTimer += Time.deltaTime;
            _obsTimer += Time.deltaTime;
            _fishT += Time.deltaTime;

            if (_objTimer >= itemInterval)
            {
                SpawnItems(itemSpawner, Random.Range(1, 7));
                _objTimer = 0;
            }

            if (_obsTimer >= obsInterval)
            {
                SpawnObs(obsSpawner);
                _obsTimer = 0;
            }
            
            if (_fishT >= fishInterval)
            {
                SpawnFish(fishSpawner);
                _fishT = 0;
            }
        }
    }

    void SpawnItems(Collider2D itemSpawn, int quantity)
    {
        for (int j = 0; j < quantity; j++)
        {
            Vector2 position = GetRandomSpawnPosition(itemSpawn);
            Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], position, Quaternion.identity);
        }
    }

    void SpawnObs(Collider2D obsSpawn)
    {
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Vector2 position = GetRandomSpawnPosition(obsSpawn);
            Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], position, Quaternion.identity);
        }
    }
    
    void SpawnFish(Collider2D fishSpawn)
    {
        Vector2 position = GetRandomSpawnPosition(fishSpawn);
        Instantiate(fishPrefab, position, Quaternion.identity);
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
