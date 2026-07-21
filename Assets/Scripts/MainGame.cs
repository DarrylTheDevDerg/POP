using UnityEngine;

public class MainGame : MonoBehaviour
{
    public float speed;
    public AnimationCurve accel;

    private TutorialIdle _t;

    void Start()
    {
        _t = FindFirstObjectByType<TutorialIdle>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_t.inTutoriel)
        {
            float div = Mathf.Max(accel.Evaluate(speed), 0.001f);
            speed += Time.deltaTime / div;
        }
    }
}
