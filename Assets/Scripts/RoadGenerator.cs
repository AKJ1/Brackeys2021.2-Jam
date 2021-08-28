using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadGenerator : MonoBehaviour
{
    private const int MaxChunkCount = 30;
    public Transform transformPlayer;
    private RoadChunk previousChunk;
    private RoadChunk currentChunk;
    private List<RoadChunk> chunks = new List<RoadChunk>();
    private MeshCollider roadMeshCollider;
    [Tooltip("Maximal high of the surface.")]
    public float surfaceHigh = 1f; //
    [Tooltip("Number of random points to generate on the surface.")]
    public int numPoints = 100;
    [Tooltip("Maximal number of iterations to find the points.")]
    public int maxIterations = 1000;
    [Tooltip("Size of the generated sphere primitives.")]
    public Vector3 scalePrimitives = new Vector3(2f, 2f, 2f);
    [Tooltip("Color of the generated sphere primitives.")]
    public Color colorPrimitives = Color.red;

    private bool isUnityTerrain = true;
    //private Collider m_collider;
    private float bboxScale = 1f;

    private void Start ()
    {
        InitializeChunk();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    UpdateChunk();
        //}
        
        if (transformPlayer==null || previousChunk == null)
        {
            return;
        }

        if (Vector3.Distance(transformPlayer.transform.position, previousChunk.end.position) < 25)
        {
            UpdateChunk();
            
            Debug.Log("You've passed" + (100 * chunks.Count) + "meters!");
        }

        //not working
        if (MaxChunkCount <= chunks.Count && (Vector3.Distance(transformPlayer.transform.position, previousChunk.end.position) <= 5))
        {
            Debug.Log("You won!");
        }

    }

    private void InitializeChunk()
    {
        currentChunk = Instantiate(Resources.Load<RoadChunk>("Road/TestRoad/4"));
        chunks.Add(currentChunk);
        var relativeMovement = transformPlayer.position - currentChunk.start.position;
        Bounds b = new Bounds();
        var allbounds = transformPlayer.GetComponentsInChildren<Renderer>().Select(r => r.bounds.size);
        foreach (var bound in allbounds)
        {
            b.Encapsulate(bound);
        }
        var playerOffset = new Vector3(0,b.size.y*1.5f,0);
        
        currentChunk.transform.position += relativeMovement - playerOffset; //new Vector3(absoluteMovement.x, 0, absoluteMovement.z);
        currentChunk.gameObject.SetActive(true);
        previousChunk = currentChunk;
        previousChunk.gameObject.SetActive(true);
    }

    // private void PositionChunk(RoadChunk first, RoadChunk second)
    // {
    //     var offset = first.start.position - second.end.position;
    //     second.transform.Translate(offset);
    // }

    private void UpdateChunk()
    {
        currentChunk = Instantiate(Resources.Load<RoadChunk>("Road/TestRoad/4"));
        var absoluteMovement = previousChunk.end.position -  currentChunk.start.position;
        // if (previousChunk != null)
        // {
        //     Debug.DrawLine(previousChunk.end.transform.position, previousChunk.start.transform.position, Color.cyan,
        //         100);
        //     Debug.DrawLine(currentChunk.end.transform.position, currentChunk.start.transform.position, Color.cyan, 100);
        //     Debug.DrawLine(previousChunk.end.transform.position, currentChunk.start.transform.position, Color.red, 100);
        // }
        
        currentChunk.transform.position += absoluteMovement;
        //GenerateObstacles();
        previousChunk = currentChunk;
        chunks.Add(previousChunk);

       
        previousChunk.gameObject.SetActive(true);

    }

    //public void GenerateObstacles()
    //{
    //    roadMeshCollider = currentChunk.prefabCollider.GetComponent<MeshCollider>();
    //    GenerateRandomPositions();
    //}

    //void GenerateRandomPositions()
    //{
    //    Vector3 pointRandom;
    //    Vector3 pointOnSurface = Vector3.zero;
    //    bool pointFound = false;
    //    int indexPoints = 0;
    //    int indexLoops = 0;
    //    do
    //    {
    //        indexLoops++;
    //        // Double the size of the bounding box here to get better results if not a Unity terrain
    //        if (isUnityTerrain) bboxScale = 1f;
    //        else bboxScale = 2f;

    //        if (isUnityTerrain) pointRandom = RandomPointInBounds(roadMeshCollider.bounds, bboxScale);
    //        else pointRandom = RandomPointInBounds(roadMeshCollider.bounds, bboxScale) - transform.position;

    //        pointFound = GetRandomPointOnColliderSurface(pointRandom, out pointOnSurface);

    //        if (pointFound)
    //        {
    //            indexPoints++;
    //            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //            sphere.transform.position = pointOnSurface;
    //            sphere.transform.localScale = scalePrimitives;
    //            sphere.GetComponent<Renderer>().material.color = colorPrimitives;
    //            //sphere.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    //        }
    //    } while ((indexPoints < numPoints) && (indexLoops < maxIterations));
    //}

    //private bool GetRandomPointOnColliderSurface(Vector3 point, out Vector3 pointSurface)
    //{

    //    Vector3 pointOnSurface = Vector3.zero;
    //    RaycastHit hit;
    //    bool pointFound = false;
    //    // Raycast against the surface of the transform
    //    Debug.DrawRay(point - surfaceHigh * transform.up, transform.up * surfaceHigh, Color.green, 5f);
    //    if (Physics.Raycast(point - surfaceHigh * transform.up, transform.up, out hit, Mathf.Infinity))
    //    {
    //        //Debug.Log("Found point up");
    //        pointOnSurface = hit.point;
    //        pointFound = true;
    //    }
    //    else
    //    {
    //        Debug.DrawRay(point + surfaceHigh * transform.up, -transform.up * surfaceHigh, Color.red, 5f);
    //        if (Physics.Raycast(point + surfaceHigh * transform.up, -transform.up, out hit, Mathf.Infinity))
    //        {
    //            //Debug.Log("Found point -up");
    //            pointOnSurface = hit.point;
    //            pointFound = true;
    //        }
    //    }

    //    pointSurface = pointOnSurface;
    //    return pointFound;
    //}

    //private Vector3 RandomPointInBounds(Bounds bounds, float scale)
    //{
    //    return new Vector3(
    //        Random.Range(bounds.min.x * scale, bounds.max.x * scale),
    //        Random.Range(bounds.min.y * scale, bounds.max.y * scale),
    //        Random.Range(bounds.min.z * scale, bounds.max.z * scale)
    //    );
    //}
}
