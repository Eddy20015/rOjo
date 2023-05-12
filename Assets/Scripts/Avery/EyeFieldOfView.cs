using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int rayCount = 5;
    private Mesh mesh;
    private Bounds bounds;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;
    // Creates the FOV for the enemy.
    private void Start()
    {   
        fov = 45f;
        viewDistance = 3f;
        mesh = new Mesh();

        bounds = GetComponent<MeshFilter>().mesh.bounds;
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;

    }

    private void LateUpdate()
    {
        rayCount = 5;
        float angle = startingAngle; 
        float angleIncrease = fov/rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0 ; i <= rayCount; i++){
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if(raycastHit2D.collider == null)
            {
                //no hit!
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Hit!
                vertex = raycastHit2D.point;
            }
            
            vertices[vertexIndex] = vertex;

            

            if (i > 0){
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x / bounds.size.x, vertices[i].y / bounds.size.y);
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        
    }

    public LayerMask GetLayerMask()
    {
        return layerMask;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetFOV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov/2f;
    }

    public void SetAimDirection(float angle)
    {
        if (angle < 0)
        {
            angle += 360;
        }
        startingAngle = angle + fov/2;
    }

    public static Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir){
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n <= 0){
            n += 360;
        }
        return n;
    }
}
