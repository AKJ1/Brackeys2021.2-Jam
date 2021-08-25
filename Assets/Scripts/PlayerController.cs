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

    public Transform playerForward;
    public float playerSpeed = 10.0f;
    public float playerRotationSpeed = 10.0f;


    void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.velocity = playerForward.forward * (playerSpeed * input.y);
        // rigidbody.AddForce(playerForward.forward * (playerSpeed * input.y), ForceMode.Force);
        rigidbody.MoveRotation(rigidbody.transform.rotation * Quaternion.AngleAxis((playerRotationSpeed * input.x * Time.deltaTime), rigidbody.transform.up));
        // rigidbody.AddTorque(rigidbody.transform.right * , ForceMode.Force);
    }
}
