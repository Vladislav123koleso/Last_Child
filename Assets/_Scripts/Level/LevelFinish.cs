using System.Collections;
using UnityEngine;

namespace LastChild
{
    public class LevelFinish : MonoBehaviour
    {
        [SerializeField] private int _seedsNeeded;
        [SerializeField] private FadeInOut _fadeInOut;

        private FallCutSceneTrigger _finishCutScene;

        private void Start()
        {
            _finishCutScene = FindObjectOfType<FallCutSceneTrigger>();

            if (_finishCutScene != null)
            {
                _finishCutScene.EndScene += StartOnFinish;
            }
        }

        private void OnDestroy()
        {
            if (_finishCutScene != null)
            {
                _finishCutScene.EndScene -= StartOnFinish;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.TryGetComponent(out Bag bag))
            {
                if (_seedsNeeded <= bag.SeedsCount)
                {
                    StartOnFinish();
                }
            }
        }

        public void StartOnFinish()
        {
            StartCoroutine(OnFinish());
        }

        private IEnumerator OnFinish()
        {
            _fadeInOut.StartFadeIn();

            yield return new WaitForSeconds(_fadeInOut.FadeTime);

            LevelController.Instance.NextLevel();
        }
    }
}
