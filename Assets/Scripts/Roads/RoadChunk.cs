using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public Transform roadChunk;
    public Transform transformRoad;
    public List<SwitchDestructable> destructables = new List<SwitchDestructable>();

    private MeshCollider roadMeshCollider;
    public int numPoints = 20;
    private float boundingBoxScale = 1f;
    private Vector3 randomPoint;
    private Vector3 pointOnRoad = Vector3.zero;
    private bool pointFound = false;
    private int indexPoints = 0;
    private RaycastHit hit;


    //private void Start()
    //{
    //    UpdateBuildings();
    //}

    void OnEnable()
    {
        roadMeshCollider = transformRoad.GetComponent<MeshCollider>();
        GenerateRandomPositions();
        //UpdateBuildings();
    }

    void GenerateRandomPositions()
    {
        for (int i = 0; i < 10; i++)
        {
            if (indexPoints >= numPoints)
            {
                break;
            }

            randomPoint = new Vector3(
            Random.Range(roadMeshCollider.bounds.min.x * boundingBoxScale, roadMeshCollider.bounds.max.x * boundingBoxScale),
            Random.Range(roadMeshCollider.bounds.min.y * boundingBoxScale, roadMeshCollider.bounds.max.y * boundingBoxScale),
            Random.Range(roadMeshCollider.bounds.min.z * boundingBoxScale, roadMeshCollider.bounds.max.z * boundingBoxScale));
         
            GetRandomPointOnColliderSurface(randomPoint, pointOnRoad);

            if (pointFound)
            {
                indexPoints++;
                Transform cubeTransform = Instantiate(Resources.Load<Transform>("Obstacles/Sphere"));
                cubeTransform.position = pointOnRoad;
                cubeTransform.localScale = new Vector3(2f, 2f, 2f);
                cubeTransform.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
                //GameObject sphere = GameObject.CreatePrimitive((PrimitiveType)Random.Range(0, 3));
                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //sphere.transform.position = pointOnRoad;
                //sphere.transform.localScale = new Vector3(2f, 2f, 2f);
                //sphere.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
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

    public void UpdateBuildings()
    {
        Debug.Log(destructables.Count);
        if (destructables.Count != 0)
        {
            for (int i = 0; i < destructables.Count; i++)
            {
                Debug.Log(i);
                destructables[i].onCollisionEnter = GameController.instance.DecreasePlayerSpeed;
            }
        }
    }

}
