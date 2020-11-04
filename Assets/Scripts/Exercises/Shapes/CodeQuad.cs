using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeQuad : MonoBehaviour
{
    public float width;
    public float height;
    private Mesh customMesh;
    private void Start()
    {
        var mesh = new Mesh();

        var verts = new Vector3[4];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(width, 0, 0);
        verts[2] = new Vector3(0, height, 0);
        verts[3] = new Vector3(width, height, 0);
        mesh.vertices = verts;

        var indices = new int[6];
        indices[0] = 0;
        indices[1] = 2;
        indices[2] = 1;
        indices[3] = 2;
        indices[4] = 3;
        indices[5] = 1;
        mesh.triangles = indices;

        var norms = new Vector3[4];
        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;
        mesh.normals = norms;

        var UVs = new Vector2[4];
        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(1, 0);
        UVs[2] = new Vector2(0, 1);
        UVs[3] = new Vector2(1, 1);
        mesh.uv = UVs;
        var filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
        customMesh = mesh;

    }
    void OnDestroy()
    {
        if (customMesh != null)
        {
            Destroy(customMesh);
        }
    }
}
