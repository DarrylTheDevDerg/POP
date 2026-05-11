using UnityEngine;

public class MainGame : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        switch (speed)
        {
            case < 2:
                speed += Time.deltaTime / 50;
                break;
            
            case < 4:
                speed += Time.deltaTime / 100;
                break;
            
            case < 6:
                speed += Time.deltaTime / 150;
                break;
        }
        
    }
}
