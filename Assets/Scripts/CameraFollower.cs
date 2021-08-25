using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    public Camera Cam => gameObject.GetComponent<Camera>();

    public float DistanceThreshold = 0.2f;

    private Vector3 dampVelocity;

    [Range(0.05f, 5f)]
    public float DampMoveTime = 0.5f;
    
    void FixedUpdate()
    {
        if (Target != null) 
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        Vector3 delta = CalculateDelta();
        var pos = transform.position;
        Vector3 newPos = Vector3.SmoothDamp(pos, pos + delta, ref dampVelocity, DampMoveTime);
        transform.position = newPos;
    }

    private Vector3 CalculateDelta()
    {
        var tarPos = Target.transform.position;
        Plane p = new Plane(Vector3.up, tarPos);
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        float enterDistance = 0f;
        p.Raycast(ray, out enterDistance);
        return tarPos - ray.GetPoint(enterDistance);
    }
}