using UnityEngine;

namespace LastChild
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : Interactable
    {
        Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            SetStartConstraints();
        }
        private void OnTriggerExit(Collider other)
        {
            SetStartConstraints();
        }

        public override void Use()
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY| RigidbodyConstraints.FreezeRotationZ;
        }
        private void SetStartConstraints()
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
