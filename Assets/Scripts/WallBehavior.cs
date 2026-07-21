using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public float[] angles;

    private RotatingObject _speen;
    private MainGame _m;
    
    private bool _doRotate;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speen = GetComponent<RotatingObject>();
        _m = FindFirstObjectByType<MainGame>();

        if (_speen != null)
        {
            int r = Random.Range(0, 100);

            _doRotate = r > 50;
            
            _speen.enabled = _doRotate;

            if (_doRotate)
            {
                if (_m != null)
                {
                    _speen.rotationAmt = 0.075f / _m.speed;
                }
                else
                {
                    _speen.rotationAmt = Random.Range(0.001f, 0.01f);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, angles[Random.Range(0, angles.Length)]);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angles[Random.Range(0, angles.Length - 1)]);
        }
        
    }
}
