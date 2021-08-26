using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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
        var relativeMovement = transformPlayer.position - currentChunk.start.position;
        Bounds b = new Bounds();
        var allbounds = transformPlayer.GetComponentsInChildren<Renderer>().Select(r => r.bounds.size);
        foreach (var bound in allbounds)
        {
            b.Encapsulate(bound);
        }
        var playerOffset = new Vector3(0,b.size.y*1.5f,0);
        
        currentChunk.transform.position += relativeMovement - playerOffset; //new Vector3(absoluteMovement.x, 0, absoluteMovement.z);
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
        // if (previousChunk != null)
        // {
        //     Debug.DrawLine(previousChunk.end.transform.position, previousChunk.start.transform.position, Color.cyan,
        //         100);
        //     Debug.DrawLine(currentChunk.end.transform.position, currentChunk.start.transform.position, Color.cyan, 100);
        //     Debug.DrawLine(previousChunk.end.transform.position, currentChunk.start.transform.position, Color.red, 100);
        // }
        
        currentChunk.transform.position += absoluteMovement;
        previousChunk = currentChunk;
    }
}
