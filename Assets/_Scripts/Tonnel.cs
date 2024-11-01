using UnityEngine;

namespace LastChild
{
    [RequireComponent(typeof(Rigidbody))]
    public class Tonnel : MonoBehaviour
    {
        [SerializeField] private BoxCollider _obstacleCollider;

        private void OnTriggerStay(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out PlayerController controller) && controller.IsCrawling)
            {
                controller.SetStateStand(false);
                _obstacleCollider.enabled = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out PlayerController controller) && controller.IsCrawling)
            {
                controller.SetStateStand(true);
                controller.SetStateCrawling();
                _obstacleCollider.enabled = true;
            }
        }
    }
}
