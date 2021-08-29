using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SwitchDestructable : MonoBehaviour
{

    public Transform FracturedParent;

    public Transform WholeParent;

    public ParticleSystem CrashParticles;
    public Action onCollisionEnter;

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player"))
        {
            GameController.instance.DecreasePlayerSpeed();

            foreach (var point in col.contacts)
            {
                var particles = Instantiate(CrashParticles.gameObject, point.point, quaternion.LookRotation(point.normal, Vector3.up));
                var system =particles.GetComponent<ParticleSystem>();
                system.Play();
            }
            FracturedParent.gameObject.SetActive(true);
            WholeParent.gameObject.SetActive(false);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
