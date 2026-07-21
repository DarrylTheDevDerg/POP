using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialIdle : MonoBehaviour
{
    public bool inTutoriel = true;
    public InputActionReference action;
    public UnityEvent afterTap;
    
    private int _hasSeenTutorial;
    private bool _noTap;

    void Start()
    {
        _hasSeenTutorial = PlayerPrefs.GetInt("Seen Tutorial", 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (action.action.WasPressedThisFrame() && inTutoriel && _hasSeenTutorial != 1 && !_noTap)
        {
            afterTap.Invoke();
            PlayerPrefs.SetInt("Seen Tutorial", 1);
            PlayerPrefs.Save();
            _noTap = true;
        }
        
        if (_hasSeenTutorial == 1)
        {
            Unfreeze();
            gameObject.SetActive(false);
        }
    }
    
    public void Unfreeze()
    {
        inTutoriel = false;
    }
}
