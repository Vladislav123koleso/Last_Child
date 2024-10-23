using UnityEngine;
using UnityEngine.InputSystem;

namespace LastChild
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;

        [SerializeField] private float _groundedOffset;
        [SerializeField] private float _groundedRadius;
        [SerializeField] private LayerMask _groundLayers;

        private Vector2 _direction;
        private bool _isGrounded;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            Movement();
            GroundedCheck();
        }

        private void Movement()
        {
            if (_isGrounded)
            {
                _rb.velocity = new Vector3(_movementSpeed * Time.fixedDeltaTime * _direction.x, _rb.velocity.y, _rb.velocity.z);
            }
        }
        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);

            _isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
        }

        public void OnMove(InputValue inputValue)
        {
            _direction = inputValue.Get<Vector2>();

            if (_direction.x == -1)
            {
                transform.localRotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (_direction.x == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        public void OnJump()
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z), _groundedRadius);
        }
#endif
    }
}

