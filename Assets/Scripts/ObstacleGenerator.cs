using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public float startDistance = 10;
    public float yDistance = 100;
    public float minSpread = 5;
    public float maxSpread = 10;

    public Transform playerTransform;
    public Transform obstaclePrefab;

    float ySpread;
    float lastYPos;

    void Start()
    {
        ySpread = Random.Range(minSpread, maxSpread);
        lastYPos = playerTransform.position.z + (startDistance - ySpread - yDistance);
    }

    void Update()
    {
        if (playerTransform.position.z - lastYPos >= ySpread)
        {
            float lanePos = Random.Range(0, 3);
            lanePos = (lanePos - 1) * 1.5f;
            Instantiate(obstaclePrefab, new Vector3(lanePos, 0, lastYPos + ySpread + yDistance), Quaternion.identity);

            lastYPos += ySpread;
            ySpread = Random.Range(minSpread, maxSpread);
        }
    }
}
