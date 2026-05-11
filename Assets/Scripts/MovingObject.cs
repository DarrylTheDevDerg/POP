using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public MainGame mainGame;
    
    private Transform _transform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainGame = FindObjectOfType<MainGame>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            _transform.localPosition -= new Vector3(0.065f * (mainGame.speed / (3 * mainGame.speed)), 0, 0);
        }
    }
}
