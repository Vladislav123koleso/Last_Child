using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource _audioSource1;
    public AudioSource _audioSource2;
    public Slider _volumeSlider;

    private void Start()
    {
        // Устанавливаем начальную громкость и значение слайдера
        _volumeSlider.value = _audioSource1.volume;
        _volumeSlider.value = _audioSource2.volume;
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        _audioSource1.volume = volume;
        _audioSource2.volume = volume;
    }
}
