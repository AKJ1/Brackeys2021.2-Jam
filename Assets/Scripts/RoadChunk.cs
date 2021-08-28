using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public Transform roadChunk;
    public Transform transformRoad;
    private MeshCollider roadMeshCollider;

    public float startDistance = 10;
    public float yDistance = 100;
    public float minSpread = 5;
    public float maxSpread = 10;
    public int numPoints = 100;
    private float boundingBoxScale = 1f;
    private Vector3 pointRandom;
    private Vector3 pointOnRoad = Vector3.zero;
    private bool pointFound = false;
    private int indexPoints = 0;
    private RaycastHit hit;

    void OnEnable()
    {
        roadMeshCollider = transformRoad.GetComponent<MeshCollider>();
        GenerateRandomPositions();
    }

    void GenerateRandomPositions()
    {
        for (int i = 0; i < 200; i++)
        {
            if (indexPoints >= numPoints)
            {
                break;
            }

            pointRandom = new Vector3(
            Random.Range(roadMeshCollider.bounds.min.x * boundingBoxScale, roadMeshCollider.bounds.max.x * boundingBoxScale),
            Random.Range(roadMeshCollider.bounds.min.y * boundingBoxScale, roadMeshCollider.bounds.max.y * boundingBoxScale),
            Random.Range(roadMeshCollider.bounds.min.z * boundingBoxScale, roadMeshCollider.bounds.max.z * boundingBoxScale));
         
            GetRandomPointOnColliderSurface(pointRandom, pointOnRoad);

            if (pointFound)
            {
                indexPoints++;
                //Transform cubeTransform = Instantiate(Resources.Load<Transform>("Obstacles/Cube"));
                //cubeTransform.position = pointOnRoad;
                //cubeTransform.localScale = new Vector3(2f, 2f, 2f);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = pointOnRoad;
                sphere.transform.localScale = new Vector3(2f, 2f, 2f);
                sphere.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }

    private void GetRandomPointOnColliderSurface(Vector3 point, Vector3 pointSurface)
    {
        pointOnRoad = Vector3.zero;
        pointFound = false;
      
        if (Physics.Raycast(point - transformRoad.up, transformRoad.up, out hit, Mathf.Infinity))
        {
            pointOnRoad = hit.point;
            pointFound = true;
        }
        else
        {
            if (Physics.Raycast(point + transformRoad.up, -transformRoad.up, out hit, Mathf.Infinity))
            {
                pointOnRoad = hit.point;
                pointFound = true;
            }
        }

        pointSurface = pointOnRoad;
    }
}
