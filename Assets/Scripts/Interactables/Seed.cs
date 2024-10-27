using UnityEngine;

namespace LastChild
{
    [RequireComponent(typeof(Rigidbody))]
    public class Seed : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.TryGetComponent(out Bag bag))
            {
                Destroy(gameObject);
                bag.AddSeed();
            }
        }
    }
}
