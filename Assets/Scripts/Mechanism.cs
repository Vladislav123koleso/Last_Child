using System.Collections;
using UnityEngine;

namespace LastChild
{
    public class Mechanism : MonoBehaviour
    {
        [SerializeField] private Switcher _switcher;
        [SerializeField] private Collider _invisibleWall;
        [SerializeField] private Transform _leftGate;
        [SerializeField] private Transform _rightGate;

        private void Start()
        {
            _switcher.OnSwitch += OpenGates;
        }

        private void OnDestroy()
        {
            _switcher.OnSwitch -= OpenGates;
        }

        private void OpenGates()
        {
            StartCoroutine(GatesMovement(_leftGate));
            StartCoroutine(GatesMovement(_rightGate));
        }

        private IEnumerator GatesMovement(Transform gate)
        {  
            float step = Time.deltaTime / gate.localScale.x * gate.localPosition.x;

            for (float i = 0; i < gate.localScale.x; i += Mathf.Abs(step))
            {
                gate.position = new Vector3(gate.position.x + step, gate.position.y, gate.position.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            if (_invisibleWall.enabled)
                _invisibleWall.enabled = false;
        }
    }
}
