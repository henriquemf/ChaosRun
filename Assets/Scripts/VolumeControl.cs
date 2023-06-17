using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private const string musicVolumeKey = "MusicVolume";
    private const string sfxVolumeKey = "SfxVolume";

    private void Start()
    {
        if (PlayerPrefs.HasKey(musicVolumeKey))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat(musicVolumeKey);
            musicVolumeSlider.value = savedMusicVolume;
        }
        else
        {
            musicVolumeSlider.value = 1f;
            PlayerPrefs.SetFloat(musicVolumeKey, musicVolumeSlider.value);
        }

        if (PlayerPrefs.HasKey(sfxVolumeKey))
        {
            float savedSfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey);
            sfxVolumeSlider.value = savedSfxVolume;
        }
        else
        {
            sfxVolumeSlider.value = 1f;
            PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolumeSlider.value);
        }
    }

    public void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat(musicVolumeKey, musicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void SaveSfxVolume()
    {
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolumeSlider.value);
        PlayerPrefs.Save();
    }
}
