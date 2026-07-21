using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 scrollSpeed = new Vector2(0.1f, 0f);

    private Material material;
    private Vector2 offset;

    private MainGame _m;
    private PlayerManager _p;

    private void Awake()
    {
        // Creates an instance of the material so only this Quad scrolls.
        material = GetComponent<Renderer>().material;
        
        _m = FindFirstObjectByType<MainGame>();
        _p = FindFirstObjectByType<PlayerManager>();
    }

    private void Update()
    {
        switch (_p.GetCurrentPowerUp())
        {
            case PowerUp.Faster:
                if (_p.RelayBoost()) offset += scrollSpeed * (_m.accel.Evaluate(_m.speed) * Time.deltaTime * 100f);
                else offset += scrollSpeed * (_m.accel.Evaluate(_m.speed) * Time.deltaTime * 0.15f);
                break;
            
            case PowerUp.Slow:
                offset += scrollSpeed * (_m.accel.Evaluate(_m.speed) * Time.deltaTime * 0.25f);
                break;
            
            case PowerUp.None:
                offset += scrollSpeed * (_m.accel.Evaluate(_m.speed) * Time.deltaTime);
                break;
            
            default:
                offset += scrollSpeed * (_m.accel.Evaluate(_m.speed) * Time.deltaTime);
                break;
        }
        offset += scrollSpeed * (_m.accel.Evaluate(_m.speed) * Time.deltaTime);

        offset.x = Mathf.Repeat(offset.x, 1f);
        offset.y = Mathf.Repeat(offset.y, 1f);

        material.SetTextureOffset("_BaseMap", offset);
    }
}
