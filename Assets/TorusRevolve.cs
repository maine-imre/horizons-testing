using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusRevolve : MonoBehaviour
{
    public float radius1 = 1f;
    public float radius2 = 1f;
    public Vector3 center = Vector3.zero;
    public int n = 5;
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = new Vector3[(int) Mathf.Pow(n - 1, 2)];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int idx = i + n * j;
                verts[idx] = torusPosition(i*(1/n)*2*Mathf.PI, j*(1/n)*2*Mathf.PI);
            }
        }

        int[] triangles = new int[6 * n * n];
        for (int k = 0; k < 2 * n * n; k++)
        {
            for (int m = 0; m < 2; m++)
                triangles[3 * k + m] = triangle(k, m);
        }

        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }

    /// <summary>
    /// Take alpha and beta to be angles describing turns around the primary and secondary revolutions of a torus.
    /// Take r1 and r2 to be the radii of those revolutions
    /// Find the position on the surface of the torus
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="beta"></param>
    /// <returns></returns>
    private Vector3 torusPosition(float alpha, float beta)
    {
        torusPosition = new Vector3(((radius1 * Math.Cos(alpha), 0, Math.Sin(alpha)));
    }
/// <summary>
/// Finds the vertex indicies for the kth triangle
/// returns the mth vertex index of the kth triangle
/// </summary>
/// <param name="k"></param>
/// <param name="m"></param>
/// <returns></returns>
    private int triangle(int k, int m)
    {
        
    }
}
