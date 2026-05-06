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
        switch (mainGame.speed)
        {
            case < 1:
                _transform.localPosition -= new Vector3(0.045f, 0, 0);
                break;
            
            case >= 1:
                _transform.localPosition -= new Vector3(0.045f * mainGame.speed, 0, 0);
                break;
        }
    }
}
