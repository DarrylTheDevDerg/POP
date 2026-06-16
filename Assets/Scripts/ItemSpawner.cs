using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public MainGame mgmt;
    public BoxCollider2D itemSpawner, obsSpawner, fishSpawner;
    
    public GameObject[] itemPrefabs;
    public GameObject[] obsPrefabs;
    public GameObject fishPrefab;

    private float _objTimer, _obsTimer, _fishT, chance;
    private PauseMenu _pauseMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mgmt = FindObjectOfType<MainGame>();
        
        _objTimer = Random.Range(15, 35);
        _obsTimer = Random.Range(20, 35);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            chance += Time.deltaTime / 2;

            if (chance >= 17)
            {
                chance = 17;
            }
            
            _objTimer += Time.deltaTime;
            _obsTimer += Time.deltaTime;
            _fishT += Time.deltaTime;

            if (_objTimer >= 12 / (chance / 7f))
            {
                SpawnItems(itemSpawner, Random.Range(1, 7));
                _objTimer = 0;
            }

            if (_obsTimer >= 10 / (chance / 3f))
            {
                SpawnObs(obsSpawner);
                _obsTimer = 0;
            }
            
            if (_fishT >= 7.45f / (chance / 9.45f))
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
        Vector2 position = GetRandomSpawnPosition(obsSpawn);
        Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], position, Quaternion.identity);
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
