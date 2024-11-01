using System.Collections;
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
        private bool _isClimbing;
        private bool _isPushing;
        private bool _isCrawling;
        private bool _canClimb;
        private Transform _climbTarget;

        private PlayerProgress _playerProgress;

        private Rigidbody _rb;
        private Animator _animator;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _playerProgress = GetComponent<PlayerProgress>();
        }

        private void FixedUpdate()
        {
            Movement();
            GroundedCheck();
        }

        private void Movement()
        {
            if (_isGrounded && _isClimbing == false)
            {
                _rb.velocity = new Vector3(_movementSpeed * Time.fixedDeltaTime * _moveDirection.x, _rb.velocity.y, _rb.velocity.z);
                _animator.SetFloat("velocity", Mathf.Abs(_moveDirection.x));

                if (_moveDirection.x == -1)
                {
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else if (_moveDirection.x == 1)
                {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }
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
        }

        public void OnJump()
        {
            if (_playerProgress.CanJump == false) return;

            if (_isGrounded && _isClimbing == false && _isCrawling == false)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _animator.SetTrigger("jump");
            }
        }

        public void OnAction()
        {
            if (_playerProgress.CanMoveObjects == false) return;

            if (_isGrounded && _isClimbing == false && _isCrawling == false)
            {
                foreach (var item in Physics.OverlapSphere(_groundedPosition, _groundedRadius, _interactableLayers))
                {
                    item.GetComponentInParent<Interactable>().Use();

                    if (item.GetComponentInParent<Obstacle>() && _isPushing == false)
                    {
                        _isPushing = true;
                        _animator.SetTrigger("startPush");
                    }
                }
            }
        }

        public void OnClimb()
        {
            if (_playerProgress.CanClimb == false || _isClimbing || _isCrawling) return;

            if (_moveDirection.x != 0 && _isGrounded && _canClimb)
            {
                StartCoroutine(Climb());
            }
        }

        private IEnumerator Climb()
        {
            _rb.velocity = Vector3.zero;
            _animator.SetTrigger("climb");

            _isClimbing = true;
            _rb.isKinematic = true;

            float climbHeight = _climbTarget.localScale.y - Mathf.Round(transform.position.y - _climbTarget.position.y);
            float step = climbHeight / _animator.GetCurrentAnimatorStateInfo(0).length * Time.deltaTime;

            for (float i = 0; i <= climbHeight; i += step)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);

                yield return new WaitForSeconds(step);
            }

            for (float i = 0; i <= _groundedRadius; i += step)
            {
                transform.position = new Vector3(transform.position.x + (transform.right * step).x, transform.position.y, transform.position.z);

                yield return new WaitForSeconds(step);
            }

            _rb.isKinematic = false;
            _isClimbing = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_groundLayers.value != 1 << other.gameObject.layer) return;

            if (_isGrounded && other.transform.root.localScale.y - Mathf.Round(transform.position.y - other.transform.root.position.y) <= _climbHeight)
            {
                _climbTarget = other.transform.root;
                _canClimb = true;
            }
            else
            {
                _climbTarget = null;
                _canClimb = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _climbTarget = null;
            _canClimb = false;

            if (_isPushing && other.GetComponentInParent<Obstacle>())
            {
                _isPushing = false;
                _animator.SetTrigger("endPush");
                Debug.Log("Inside");
            }

            Debug.Log("Outside");
        }

        public void SetStateCrawling(bool isCrawling)
        {
            _isCrawling = isCrawling;
            _animator.SetTrigger("crawl");
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

