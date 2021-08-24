using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PathCreator pathCreator;
    public Rigidbody rigidbody;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 10.0f;


    void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.AddForce(transform.forward * (playerSpeed * input.y), ForceMode.Force);
        rigidbody.AddForce(transform.right * (playerSpeed * input.x), ForceMode.Force);
    }
}
