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

    public Vector3 pastVelocity;

    public float pastMagnitude;

    private float motionDecay;
    private float movementMagnitude;

    void FixedUpdate()
    {
        var currentVelocity = rigidbody.velocity;
        rigidbody.velocity =(currentVelocity * (1 - motionDecay)) +  playerForward.forward * movementMagnitude;
        // rigidbody.AddForce(playerForward.forward * (playerSpeed * input.y), ForceMode.Force);
        // rigidbody.AddTorque(rigidbody.transform.right * , ForceMode.Force);

        pastVelocity = rigidbody.velocity;
    }

    void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        motionDecay = rigidbody.drag * Time.deltaTime;
        movementMagnitude = (playerSpeed * input.y * Time.fixedDeltaTime);
        rigidbody.MoveRotation(rigidbody.transform.rotation * Quaternion.AngleAxis((playerRotationSpeed * input.x * Time.deltaTime), rigidbody.transform.up));
    }
}
