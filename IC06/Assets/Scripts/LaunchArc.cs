using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArc : MonoBehaviour
{

    Mesh mesh;
    public float meshWidth;

    public float velocity;
    public float angle;
    public int resolution = 10;

    float g;
    float radianAngle;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    public Vector3[] Render()
    {
        Vector3[] array = CalculateArcArray();
        MakeArcMesh(array);
        return array;

    }

    public void Disable()
    {
        mesh.Clear();
    }
    // Update is called once per frame
    void MakeArcMesh(Vector3[] arcVerts)
    {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution +1)*2];
        int[] triangles = new int[resolution *12];

        for( int i=0;i<= resolution; i++)
        {
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 +1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

            if (i != resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;

            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    Vector3[] CalculateArcArray()
    {

        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;

        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }

    public Vector3[] Calculate3dArcArray(Vector3 origine,Vector3 arrivee)
    {

        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float xDistance = arrivee.x - origine.x;
        float zDistance = arrivee.z - origine.z;

        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;
        float maxDistance2 = Mathf.Sqrt((xDistance * xDistance) + (zDistance * zDistance));

        Debug.Log(maxDistance);
        Debug.Log(maxDistance2);
        for (int i = 0; i < resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = Calculate3dArcPoint(origine, arrivee, t, maxDistance2);
        }
        arcArray[resolution] = arrivee;

        return arcArray;
    }
    Vector3 Calculate3dArcPoint(Vector3 origin, Vector3 arrivee, float t, float maxDistance){
        float xDistance = arrivee.x - origin.x;
        float zDistance = arrivee.z - origin.z;
        float x = (t * xDistance) + origin.x;
        float z = (t * zDistance) + origin.z;
        float height_x = t * maxDistance;
        float y = height_x * Mathf.Tan(radianAngle) - ((g * height_x * height_x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        
        
        return new Vector3(x, y,z);
    }
}