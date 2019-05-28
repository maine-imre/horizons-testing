using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TorusRevolve : MonoBehaviour
{
    //floats for radius
    public float radius1 = 1f;
    public float radius2 = 1f;

    //origin
    public Vector3 center = Vector3.zero;


    public int n = 5;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        //Get mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = new Vector3[(n + 1) * (n - 1)+1];

        //Array of 2D vectors for UV map of vertices
        Vector2[] uvs = new Vector2[verts.Length];
        float oneNth = 1f / ((float)n);

        //loop through n-1 times, since edges wrap around
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                //index value computation
                int idx = i + n * j;

                //find radian value of length of curve
                float alpha = i*oneNth*2*Mathf.PI;
                float beta = j*oneNth*2*Mathf.PI;

                //map vertices from 2 dimensions to 3
                verts[idx] = torusPosition(alpha,beta);
                
                //uv mapping 
                uvs[idx] = new Vector2(j*oneNth, i*oneNth);
            }
        }

        //integer array for number of triangle vertices-3 verts x 2 triangles per square x n^2
        int[] triangles = new int[6 * n * n];

        //for each triangle
        for (int k = 0; k < 2 * n * n; k++)
        {
            //for each vertex on each triangle
            for (int m = 0; m < 3; m++)
                triangles[3 * k + m] = triangle(k, m);
        }


        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }

    /// <summary>
    /// Take alpha and beta to be angles describing turns around the primary and secondary revolutions of a torus.
    /// Take r1 and r2 to be the radii of those revolutions
    /// Find the position on the surface of the torus
    /// </summary>
    /// <param name="alpha">x</param>
    /// <param name="beta">y</param>
    /// <returns></returns>
    private Vector3 torusPosition(float alpha, float beta)
    {
        //3D vectors for describing positions on the circle
        Vector3 firstPosition = new Vector3(radius1 * Mathf.Cos(alpha), 0, radius1 * Mathf.Sin(alpha));
        Vector3 secondPosition = new Vector3(radius2 * Mathf.Cos(beta), radius2 * Mathf.Sin(beta), 0)+Vector3.right*radius1;

        //mapping of 
        Vector3 result = firstPosition + Quaternion.FromToRotation(Vector3.right, firstPosition)*secondPosition+center;
        return result;
    }
    /// <summary>
    /// Finds the vertex indicies for the kth triangle
    /// returns the mth vertex index of the kth triangle
    /// Triangles indexed for clockwise front face
    /// </summary>
    /// <param name="k"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    private int triangle(int k, int m)
    {
        if(k < n * n)
        {
            //lower half triangle
            switch (m)
            {
                case 0:
                    return k;
                case 2:
                    return Mathf.FloorToInt((k) / n) * n + ((k + 1) % n);
                case 1:
                    return ((Mathf.FloorToInt((k) / n) + 1) % n) * n + ((k + 1) % n);
            }
        }
        else
        {
            k = k - n * n;

            switch (m)
            {

                case 0:
                    return k;
                case 2:
                    return ((Mathf.FloorToInt((k) / n)+1)%n) * n + ((k + 1) % n);
                case 1:
                    return ((Mathf.FloorToInt((k) / n) + 1) % n) * n + (k % n);



            }
        }

        Debug.LogError("Invalid parameter.");



        return 0;
    }

}