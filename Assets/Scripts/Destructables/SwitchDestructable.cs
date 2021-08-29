using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class SwitchDestructable : MonoBehaviour
{

    public Transform FracturedParent;

    public Transform WholeParent;

    public ParticleSystem CrashParticles;
    public Action onCollisionEnter;

    private Collider[] hitObjects = new Collider[25];
    private Vector3[] collisionPoints;

    private float impactMagnitude;

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player"))
        {
            GameController.instance.IncreasePlayerSpeed();
            impactMagnitude = col.impulse.magnitude;
            
            collisionPoints = col.contacts.Select(c => c.point).ToArray();
            foreach (var point in col.contacts)
            {
                Coroutiner.StartCoroutine(KnockStuffDown());
                var particles = Instantiate(CrashParticles.gameObject, point.point, quaternion.LookRotation(point.normal, Vector3.up));
                var system =particles.GetComponent<ParticleSystem>();
                system.Play();
            }
            FracturedParent.gameObject.SetActive(true);
            WholeParent.gameObject.SetActive(false);

        }
    }

    IEnumerator KnockStuffDown()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        foreach (var cp in collisionPoints)
        {
            Physics.OverlapSphereNonAlloc(cp, 10, this.hitObjects);
            foreach (var obj in hitObjects)
            {
                if (obj != null)
                {
                    if (obj.attachedRigidbody != null)
                    {
                        obj.attachedRigidbody.AddExplosionForce(200, cp, 10);
                    }
                }
            }
        }
    }
}
