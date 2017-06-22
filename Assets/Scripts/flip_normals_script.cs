using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class flip_normals_script : MonoBehaviour
{
    private bool isFlipped = false;

    public bool renderOnStart = true;
    public bool flipNormals = true;

    private Mesh originalMesh;

    void Start()
    {
        if (renderOnStart)
        {
            GetComponent<Renderer>().enabled = true;
        }
    }

    private void Update()
    {
        if (flipNormals)
        {
            if (!isFlipped)
            {
                FlipNormals();
                isFlipped = true;
            }
        }
        else
        {
            if (isFlipped)
            {
                FlipNormals();
                isFlipped = false;
            }
        }
    }

    public void FlipNormals()
    {
        MeshFilter filter = GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (filter != null)
        {
            Mesh mesh = filter.mesh;

            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            for (int m = 0; m < mesh.subMeshCount; m++)
            {
                int[] triangles = mesh.GetTriangles(m);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;

                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }
}