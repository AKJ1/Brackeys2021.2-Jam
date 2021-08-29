using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player"))
        {
            GameController.instance.DecreasePlayerSpeed();
            gameObject.SetActive(false);
        }
    }
}
