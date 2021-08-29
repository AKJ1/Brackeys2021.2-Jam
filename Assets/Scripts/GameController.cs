using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerController playerController;
    public RoadGenerator roadGenerator;

    private void Awake()
    {
        instance = this;
    }

    public void DecreasePlayerSpeed()
    {
        playerController.playerSpeed -= 0.5f;
    }
    
    public void IncreasePlayerSpeed()
    {
        playerController.playerSpeed += 0.5f;
    }
}
