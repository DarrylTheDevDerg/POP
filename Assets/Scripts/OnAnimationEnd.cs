using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class ExclusiveStuff
{
    public string name;
    public UnityEvent whatDo;
}

public class OnAnimationEnd : MonoBehaviour
{
    private Animator _a;
    private AnimatorStateInfo _aState;

    public UnityEvent doInEnd;
    public string animName;

    private string _gOExclusive;
    public List<ExclusiveStuff> exclusive;

    void Start()
    {
        _a = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        _aState = _a.GetCurrentAnimatorStateInfo(0);

        if (_aState.IsName(animName) && _aState.normalizedTime >= 1)
        {
            if (_gOExclusive == null)
            {
                doInEnd.Invoke();
            }
            else
            {
                foreach (ExclusiveStuff exc in exclusive)
                {
                    if (exc.name == _gOExclusive)
                    {
                        exc.whatDo.Invoke();
                    }
                }
            }
        }
    }

    public void GameOverExclusive(string option)
    {
        _gOExclusive = option;
    }
}
