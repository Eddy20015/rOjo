using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int rayCount = 25;
    [SerializeField] private float fadeDuration = .01f;
    private float elapsedTime;

    private Mesh mesh;
    private Color BaseColor, NoiseColor, HealthBaseColor, HealthNoiseColor;
    private MeshRenderer meshRenderer;
    private Bounds bounds;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;
    private float sign = 1;
    private float flip = 0;

    // Creates the FOV for the enemy.
    private void Start()
    {   
        mesh = new Mesh();
        meshRenderer = GetComponent<MeshRenderer>();
        bounds = GetComponent<MeshFilter>().mesh.bounds;
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        HealthBaseColor = Color.black;
        HealthBaseColor = Color.black;
        BaseColor = meshRenderer.material.GetColor("_BaseColor");
        NoiseColor = meshRenderer.material.GetColor("_Noisecolor");
        meshRenderer.material.SetColor("_BaseColor", Color.black);
        meshRenderer.material.SetColor("_Noisecolor", Color.black);
        sign = Mathf.Sign(transform.localScale.x);

        if (sign < 0)
        {
            flip = 180.0f;
        }
    }

    private void Update()
    {
        float angle = startingAngle; 
        float angleIncrease = fov/rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        Debug.DrawRay(origin, Quaternion.Euler(0.0f, flip, 0.0f) * GetVectorFromAngle(angle), Color.red);
        for(int i = 0 ; i <= rayCount; i++){
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Quaternion.Euler(0.0f, flip, 0.0f) * GetVectorFromAngle(angle), viewDistance, layerMask);
            if(raycastHit2D.collider == null)
            {
                //no hit!
                vertex = origin + Quaternion.Euler(0.0f, flip, 0.0f) * GetVectorFromAngle(angle) * viewDistance;
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
        mesh.RecalculateBounds();
        
    }

    public void setReverse(bool isReversed)
    {
        if(isReversed)
        {
            flip = 180.0f;
        }
        else
        { 
            flip = 0.0f;
        }
    }

    public void resetElapsedTime()
    {
        this.elapsedTime = 0.0f;
    }

    public void SetFade(float percentFade)
    {
        meshRenderer.material.SetColor("_BaseColor", BaseColor * percentFade);
        meshRenderer.material.SetColor("_Noisecolor", NoiseColor * percentFade);
        HealthBaseColor = meshRenderer.material.GetColor("_BaseColor");
        HealthNoiseColor = meshRenderer.material.GetColor("_Noisecolor");
    }

    public void FadeIn()
    {
        meshRenderer.material.SetColor("_BaseColor", smoothFadeSpot(meshRenderer.material.GetColor("_BaseColor"), BaseColor));
        meshRenderer.material.SetColor("_Noisecolor", smoothFadeSpot(meshRenderer.material.GetColor("_Noisecolor"), NoiseColor));
    }

    public void FadeOut()
    {
        meshRenderer.material.SetColor("_BaseColor", smoothFadeSpot(meshRenderer.material.GetColor("_BaseColor"), Color.black));
        meshRenderer.material.SetColor("_Noisecolor", smoothFadeSpot(meshRenderer.material.GetColor("_Noisecolor"), Color.black));
    }

    private Color smoothFadeSpot(Color alphaStart, Color alphaEnd)
    {
        float percentageComplete = elapsedTime / fadeDuration;
        elapsedTime += Time.deltaTime;
        return Color.Lerp(alphaStart, alphaEnd, percentageComplete);
    }

    public LayerMask GetLayerMask()
    {
        return layerMask;
    }

    public void SetFadeDuration(float fadeDuration)
    {
        this.fadeDuration = fadeDuration;
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
        Debug.Log("Degree found: " + n);
        return n;
    }
}
