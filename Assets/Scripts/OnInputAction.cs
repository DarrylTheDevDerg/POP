using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnInputAction : MonoBehaviour
{
    public InputActionReference input;
    public UnityEvent toDo;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.action.WasPressedThisFrame())
        {
            toDo.Invoke();
        }
    }
}
