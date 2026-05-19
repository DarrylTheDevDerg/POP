using UnityEngine;
using UnityEngine.Events;

public class EventMgmt : MonoBehaviour
{
    public UnityEvent events;
    public bool doInStart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doInStart)
        {
            events.Invoke();
        }
    }

    public void DoEvents()
    {
        events.Invoke();
    }
}
