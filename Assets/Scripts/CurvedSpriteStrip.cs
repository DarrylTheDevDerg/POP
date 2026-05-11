using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CurvedSpriteStrip : MonoBehaviour
{
    [Header("Shape")]
    public int segments = 50;
    public float width = 20f;
    public float height = 2f;
    public float curveAmount = 2f;

    [Header("Texture")]
    public float textureTiling = 5f;
    public float scrollSpeed = 1f;

    private Mesh mesh;
    private Vector2[] uvs;
    private MainGame speed;

    void Start()
    {
        speed = FindObjectOfType<MainGame>();
        GenerateMesh();
    }

    void Update()
    {
        ScrollTexture();
    }

    void GenerateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int vertCount = (segments + 1) * 2;

        Vector3[] vertices = new Vector3[vertCount];
        uvs = new Vector2[vertCount];
        int[] triangles = new int[segments * 6];

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            float x = (t - 0.5f) * width;

            // Arc formula
            float y = -Mathf.Pow(x / (width * 0.5f), 2) * curveAmount;

            int index = i * 2;

            vertices[index] = new Vector3(x, y, 0);
            vertices[index + 1] = new Vector3(x, y + height, 0);

            float uvX = t * textureTiling;

            uvs[index] = new Vector2(uvX, 0);
            uvs[index + 1] = new Vector2(uvX, 1);

            if (i < segments)
            {
                int tri = i * 6;

                triangles[tri] = index;
                triangles[tri + 1] = index + 1;
                triangles[tri + 2] = index + 2;

                triangles[tri + 3] = index + 2;
                triangles[tri + 4] = index + 1;
                triangles[tri + 5] = index + 3;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    void ScrollTexture()
    {
        float offset = Time.time * scrollSpeed * (speed.speed / 2.5f);

        Vector2[] newUVs = new Vector2[uvs.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            newUVs[i] = new Vector2(uvs[i].x + offset, uvs[i].y);
        }

        mesh.uv = newUVs;
    }
}