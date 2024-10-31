using UnityEngine;
using UnityEngine.InputSystem;

namespace LastChild
{
    [RequireComponent(typeof(Rigidbody))]
    public class Tonnel : MonoBehaviour
    {
        [SerializeField] private BoxCollider _obstacleCollider;

        private void OnTriggerEnter(Collider other)
        {
            other.transform.parent.TryGetComponent(out PlayerController controller);

            if (controller != null )
            {
                controller.SetStateCrawling(true);
                _obstacleCollider.enabled = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.transform.parent.TryGetComponent(out PlayerController controller);

            if (controller != null)
            {
                controller.SetStateCrawling(false);
                _obstacleCollider.enabled = true;
            }
        }
    }
}
