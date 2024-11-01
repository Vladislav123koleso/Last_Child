using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LastChild
{
    public class Switcher : Interactable
    {
        [SerializeField] private Transform _lever;
        [SerializeField] private float _rotationAngle = 60f;
        [SerializeField] private float _rotationStep = 1f;

        public event UnityAction OnSwitch;

        private bool _isSwitched;

        public override void Use()
        {
            if (_isSwitched == false)
            {
                OnSwitch?.Invoke();
                StartCoroutine(RotateLever());
                _isSwitched = true;
            }
        }

        private IEnumerator RotateLever()
        {
            for (float i = 0; i < _rotationAngle; i += _rotationStep)
            {
                _lever.Rotate(new Vector3(0f, 0f, -_rotationStep));

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
