using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float _smoothSpeed;
    private Vector3 _offset;

    private void Start()
    {
        _offset = new Vector3(0, 0, -10);
        _smoothSpeed = 0.6f;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + _offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}
