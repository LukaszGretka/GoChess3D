using UnityEngine;

public class OfflineSceneCamera : MonoBehaviour
{
    [SerializeField]
    private float _cameraRotationSpeed; 

    private void Start()
    {
        _cameraRotationSpeed = _cameraRotationSpeed == default ? 10f : _cameraRotationSpeed;
    }

    private void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.down, Time.deltaTime * _cameraRotationSpeed);
    }
}
