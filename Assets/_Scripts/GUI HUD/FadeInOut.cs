using System.Collections;
using UnityEngine.UI;
using UnityEngine;

    public class FadeInOut : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _fadeTime = 2f;
        public float FadeTime => _fadeTime;

        private void Start()
        {
            _fadeImage.gameObject.SetActive(true);
            StartFadeOut();
        }

        public void StartFadeIn()
        {
            StartCoroutine(Fade(1f, _fadeTime));
        }

        public void StartFadeOut()
        {
            StartCoroutine(Fade(0f, _fadeTime));
        }

        private IEnumerator Fade(float value, float time)
        {
            float currentColor = 0f;
            Color startColor = _fadeImage.color;
            Color finalColor = startColor;
            finalColor.a = value;

            while ((currentColor += Time.deltaTime) <= time)
            {
                _fadeImage.color = Color.Lerp(startColor, finalColor, currentColor / time);

                yield return Time.deltaTime;
            }

            _fadeImage.color = finalColor;
        }

        public bool IsFadeImageActive()
        {
            return _fadeImage.gameObject.activeSelf;
        }


    }

