using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Light))]
public class VolumetricLightMesh : MonoBehaviour
{

    public float maximumOpacity = 0.25f;
    public int SubDivisions = 16;

    private MeshFilter filter;
    private Light light;

    private Mesh mesh;

    // Use this for initialization
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        light = GetComponent<Light>();
        if (light.type != LightType.Spot)
        {
            Debug.LogError("Attached Volumetric Light Mesh to a non-supported Light Type. Please use Spotlight lights.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        mesh = BuildMesh();

        filter.mesh = mesh;
    }

    private Mesh BuildMesh()
    {
        mesh = new Mesh();

        float coneAngle = light.innerSpotAngle * 0.5f * Mathf.Deg2Rad;
        float horizFac = -Mathf.Cos(coneAngle + Mathf.PI);

        float farPosition = Mathf.Tan(coneAngle) * light.range;
        var vertices = new Vector3[SubDivisions * 2];
        var uv = new Vector2[SubDivisions * 2];
        var normals = new Vector3[SubDivisions * 2];
        var triangles = new int[SubDivisions * 3];

        for (int i = 0; i < SubDivisions; i++)
        {
            float angle = (Mathf.PI * 2 / SubDivisions) * i;
            float halfangle = (Mathf.PI * 2 / SubDivisions) * (i + 0.5f);

            vertices[i] = new Vector3(0, 0, 0);
            uv[i] = new Vector2(0, 0);
            normals[i] = new Vector3(Mathf.Cos(halfangle) * horizFac, Mathf.Sin(halfangle) * horizFac, -horizFac).normalized;

            vertices[i + SubDivisions] = new Vector3(farPosition * Mathf.Cos(angle), farPosition * Mathf.Sin(angle), light.range);
            uv[i + SubDivisions] = new Vector2(0, 1);
            normals[i + SubDivisions] = new Vector3(Mathf.Cos(angle) * horizFac, Mathf.Sin(angle) * horizFac, -horizFac).normalized;
            triangles[i * 3] = i;
            triangles[i * 3 + 1] = (i + 1) % SubDivisions + SubDivisions;
            triangles[i * 3 + 2] = i + SubDivisions;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.normals = normals;
        //mesh.RecalculateNormals();

        return mesh;
    }
}
