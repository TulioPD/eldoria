using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.1f;

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned to the camera script.");
            return;
        }
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
