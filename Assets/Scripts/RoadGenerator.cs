using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public Transform transformPlayer;
    private RoadChunk previousChunk;
    private RoadChunk currentChunk;

    private void Start ()
    {
        InitializeChunk();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            UpdateChunk();
        }
        
        if (transformPlayer==null || previousChunk == null)
        {
            return;
        }

        if (Vector3.Distance(transformPlayer.transform.position, previousChunk.end.position) < 25)
        {
            UpdateChunk();
        }
    }

    private void InitializeChunk()
    {
        currentChunk = Instantiate(Resources.Load<RoadChunk>("Road/TestRoad/2"));
        var absoluteMovement = transformPlayer.position - currentChunk.start.position;
        currentChunk.transform.position = absoluteMovement; //new Vector3(absoluteMovement.x, 0, absoluteMovement.z);
        previousChunk = currentChunk;
    }

    // private void PositionChunk(RoadChunk first, RoadChunk second)
    // {
    //     var offset = first.start.position - second.end.position;
    //     second.transform.Translate(offset);
    // }

    private void UpdateChunk()
    {
        currentChunk = Instantiate(Resources.Load<RoadChunk>("Road/TestRoad/2"));
        var absoluteMovement = previousChunk.end.position -  currentChunk.start.position;
        if (previousChunk != null)
        {
            Debug.DrawLine(previousChunk.end.transform.position, previousChunk.start.transform.position, Color.cyan,
                100);
            Debug.DrawLine(currentChunk.end.transform.position, currentChunk.start.transform.position, Color.cyan, 100);
            Debug.DrawLine(previousChunk.end.transform.position, currentChunk.start.transform.position, Color.red, 100);
        }
        currentChunk.transform.position += absoluteMovement;
        previousChunk = currentChunk;
    }
}
