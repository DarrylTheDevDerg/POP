using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [Range(0.001f, 0.75f)]
    public float rotationAmt;
    private Transform _transform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, _transform.eulerAngles.z + rotationAmt);
    }
}
