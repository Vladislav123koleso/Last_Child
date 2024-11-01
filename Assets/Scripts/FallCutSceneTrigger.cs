using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LastChild
{
    [RequireComponent(typeof(Rigidbody))]
    public class FallCutSceneTrigger : MonoBehaviour
    {
        [SerializeField] private float _timeToNextLevel;
        [SerializeField] private float _fallTime;
        [SerializeField] private float _fallDistance;

        public event UnityAction EndScene;

        private void OnTriggerEnter(Collider other)
        {
            var anim = other.transform.parent.GetComponentInChildren<Animator>();
            var input = other.transform.parent.GetComponent<PlayerInput>();


            if (anim != null && input != null)
            {
                StartCoroutine(Fall(other.transform.parent));
                anim.SetTrigger("fall");
                input.enabled = false;
            }
        }

        private IEnumerator Fall(Transform body)
        {
            float step = Time.deltaTime / _fallTime;

            float startPos = body.position.x;
            float endPos = body.position.x + _fallDistance;

            for (float i = startPos; i < endPos; i += step)
            {
                body.position = new Vector3(body.position.x + step, body.position.y, body.position.z);
                
                yield return new WaitForSeconds(step);
            }

            yield return new WaitForSeconds(_timeToNextLevel);

            EndScene?.Invoke();
        }
    }
}
