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
        currentChunk.transform.position = absoluteMovement;//new Vector3(absoluteMovement.x, 0, absoluteMovement.z);
        previousChunk = currentChunk;
    }

    private void UpdateChunk()
    {
        currentChunk = Instantiate(Resources.Load<RoadChunk>("Road/TestRoad/2"));
        var absoluteMovement = previousChunk.end.position - currentChunk.start.position;
        currentChunk.transform.position = absoluteMovement;
        previousChunk = currentChunk;
    }
}
