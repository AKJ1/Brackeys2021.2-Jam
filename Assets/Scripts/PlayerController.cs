using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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


    [FormerlySerializedAs("StabilityLossRate")] public float stabilityLossFactor = 10;

    public float currentStability = 0;
    public float stabilityGainPerSecond = 1;
    
    [Range(0,1)]
    public float minTraction = 0.02f;
    public float maxTraction = 0.2f;
    public float currentTraction;

    public Quaternion previousRotation;

    private float motionDecay;
    private float movementMagnitude;

    void FixedUpdate()
    {
        var currentVelocity = rigidbody.velocity;

        Quaternion.Angle(this.rigidbody.transform.rotation, previousRotation);

        var newVelocity = (currentVelocity * (1 - motionDecay));
        var reroutedVelocity = playerForward.forward * (newVelocity.magnitude * currentTraction);
        var continuedVelocity = newVelocity * ( 1 - currentTraction);

        var finalVelocity = reroutedVelocity + continuedVelocity;
        rigidbody.velocity = finalVelocity + (playerForward.forward * movementMagnitude);
        // rigidbody.AddForce(playerForward.forward * (playerSpeed * input.y), ForceMode.Force);
        // rigidbody.AddTorque(rigidbody.transform.right * , ForceMode.Force);

        pastVelocity = rigidbody.velocity;
        previousRotation = rigidbody.transform.rotation;
    }

    void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        motionDecay = rigidbody.drag * Time.deltaTime;
        movementMagnitude = (playerSpeed * input.y * Time.fixedDeltaTime);
        // rigidbody.angularVelocity = Vector3.forward * playerRotationSpeed *

        var newRotation = rigidbody.transform.rotation *
                          Quaternion.AngleAxis((playerRotationSpeed * input.x * Time.deltaTime),
                              rigidbody.transform.up);
        currentStability -= (1-Quaternion.Dot(rigidbody.transform.rotation, newRotation))* stabilityLossFactor;
        currentStability += stabilityGainPerSecond * Time.deltaTime;
        currentStability = Mathf.Clamp01(currentStability);
        currentTraction = Mathf.Lerp(minTraction, maxTraction, currentStability);
        rigidbody.MoveRotation(newRotation);
        
        
        
    }
}
