using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ConeMesh : MonoBehaviour
{
    public Mesh mesh;
    //Minimum radius of the circle at the base of the cone
    private readonly float radiusMin = 0.5f;
    //Maximum radius of the circle at the base of the cone
    private readonly float radiusMax = 1.0f;
    //Current radius of the circle at the base of the cone
    private float radius;
    private MeshFilter mf;
    private MeshRenderer mr;
    private bool IsColorSet = false;
    public Material material;
    private Color matColor;
    // Start is called before the first frame update
    void Start()
    {
        radius = radiusMin;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
        // Distribution material
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsColorSet)
        {
            mr.material.SetColor("_BaseColor",matColor);
            IsColorSet = false;
        }

    }

    //Changes the radius of the circle at the base of the cone by the percentage in input, varying it between radiusMin
    //and radiusMax
    public void ChangeRadius(float percentage)
    {
        float newRadius = radiusMin + percentage * (radiusMax - radiusMin);
        //To change the radius rescale the vertices and the uvs on the X and Z axis
        float scale = newRadius / radius;
        Vector3[] newVertices = new Vector3[mesh.vertices.Length];
        Vector2[] newUv = new Vector2[mesh.uv.Length];
        for(int i = 0; i < mesh.vertices.Length; i++)
        {
            newVertices[i] = new Vector3 (mesh.vertices[i].x*scale, mesh.vertices[i].y, mesh.vertices[i].z*scale);
        }
        for(int i = 0; i < mesh.uv.Length; i++)
        {
            newUv[i] = new Vector2((mesh.uv[i].x - 0.5f) * scale + 0.5f, (mesh.uv[i].y - 0.5f) * scale + 0.5f);
        }
        radius = newRadius;
        mesh = new Mesh
        {
            name = mesh.name,
            vertices = newVertices,
            uv = newUv,
            triangles = (int[]) mesh.triangles.Clone(),
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mf.mesh = mesh;
    }

   

    public void SetColor(Color color)
    {
        matColor = color;
        IsColorSet = true;
    }

}
