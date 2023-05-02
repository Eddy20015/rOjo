using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    Resolution[] resolutions;

    [SerializeField] TMP_Dropdown resolutionDropdown;

    [SerializeField] public AudioMixer masterVolumeMixer;
    [SerializeField] public AudioMixer sfxVolumeMixer;
    [SerializeField] public AudioMixer musicVolumeMixer;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = new Resolution[3];

        resolutions[0].width = 1280;
        resolutions[0].height = 720;

        resolutions[1].width = 1920;
        resolutions[1].height = 1080;

        resolutions[2].width = 3840;
        resolutions[2].height = 2160;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        masterVolumeMixer.SetFloat("MasterVolume", masterVolumeSlider.value);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        sfxVolumeMixer.SetFloat("SFXVolume", sfxVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        musicVolumeMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolume(float volume)
    {
        masterVolumeMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolumeMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolumeMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
