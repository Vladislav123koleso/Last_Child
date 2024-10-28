using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource _audioSource;
    public Slider _volumeSlider;

    private void Start()
    {
        // Устанавливаем начальную громкость и значение слайдера
        _volumeSlider.value = _audioSource.volume;
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
