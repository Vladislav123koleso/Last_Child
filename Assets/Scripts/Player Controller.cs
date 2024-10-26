using UnityEngine;
using UnityEngine.InputSystem;

namespace LastChild
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _climbHeight;

        [Space]
        [SerializeField] private float _groundedOffset;
        [SerializeField] private float _groundedRadius;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private LayerMask _interactableLayers;

        private Vector2 _moveDirection;
        private Vector3 _groundedPosition;
        private bool _isGrounded;
        //private bool _canClimb;
        private Transform _climbTarget;

        private PlayerProgress _playerProgress;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            _playerProgress = GetComponent<PlayerProgress>();
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
                _rb.velocity = new Vector3(_movementSpeed * Time.fixedDeltaTime * _moveDirection.x, _rb.velocity.y, _rb.velocity.z);
            }
        }

        private void GroundedCheck()
        {
            _groundedPosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);

            _isGrounded = Physics.CheckSphere(_groundedPosition, _groundedRadius, _groundLayers);
        }

        public void OnMove(InputValue inputValue)
        {
            _moveDirection = inputValue.Get<Vector2>();

            if (_moveDirection.x == -1)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (_moveDirection.x == 1)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        public void OnJump()
        {
            if (_isGrounded && _playerProgress.CanJump == true)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        public void OnAction()
        {
            if (_isGrounded && _playerProgress.CanMoveObjects == true)
            {
                foreach (var item in Physics.OverlapSphere(_groundedPosition, _groundedRadius, _interactableLayers))
                {
                    item.GetComponentInParent<Interactable>().Use();
                }
            }
        }

        public void OnClimb()
        {
            if (_isGrounded && _playerProgress.CanClimb == true)
            {
                //transform.position = new Vector3(_climbTarget.position.x, _climbTarget.position.y + _climbTarget.localScale.y, 0f);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_climbTarget.position.x, _climbTarget.position.y + _climbTarget.localScale.y, 0f), 1f);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_groundLayers.value != 1 << other.gameObject.layer) return;

            if (_isGrounded && other.transform.root.localScale.y - Mathf.Round(transform.position.y - other.transform.root.position.y) <= _climbHeight)
            {
                _climbTarget = other.transform.root;
                _playerProgress.CanClimb = true;
            }
            else
            {
                _climbTarget = null;
                _playerProgress.CanClimb = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _climbTarget = null;
            _playerProgress.CanClimb = false;
        }

        public void SetMovementSpeed(float speed)
        {
            _movementSpeed = speed;
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z), _groundedRadius);
        }
#endif
    }
}

