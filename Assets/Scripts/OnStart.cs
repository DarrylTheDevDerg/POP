using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    public UnityEvent doInStart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doInStart.Invoke();   
    }
}
