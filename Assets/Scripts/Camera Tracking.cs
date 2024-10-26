using UnityEngine;

namespace LastChild
{
    public class CameraTracking : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _cameraPositionOffset;
        [SerializeField] private Vector3 _cameraRotationOffset;
        [SerializeField] private float _maxAngle;

        private void Start()
        {
            transform.position = _target.position + _cameraPositionOffset;
        }
        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _cameraPositionOffset, Time.fixedDeltaTime);

            Vector3 newDirection = Vector3.RotateTowards(transform.position, (_target.position + _cameraRotationOffset - transform.position), _maxAngle, 0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
