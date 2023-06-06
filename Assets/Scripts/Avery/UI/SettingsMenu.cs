using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    private const float DEFAULT_VOLUME = 50f;

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    [SerializeField] private float masterVolume = 50;
    [SerializeField] private float musicVolume = 50;
    [SerializeField] private float sfxVolume = 50;


    
    
    Resolution[] resolutions;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    

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

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", DEFAULT_VOLUME);
        masterVolume = masterVolumeSlider.value;
        AkSoundEngine.SetRTPCValue("Master_Volume", masterVolume);

        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", DEFAULT_VOLUME);
        musicVolume = musicVolumeSlider.value;
        AkSoundEngine.SetRTPCValue("Music_Volume", musicVolume);

        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", DEFAULT_VOLUME);
        sfxVolume = sfxVolumeSlider.value;
        AkSoundEngine.SetRTPCValue("SFX_Volume", sfxVolume);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    /*public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = volumeSlider.value;

        if(whatValue == "Master") {
            masterVolume = volumeSlider.value;
            AkSoundEngine.SetRTPCValue("Master_Volume", masterVolume);
        }

        if (whatValue == "Music") {
            musicVolume = volumeSlider.value;
            AkSoundEngine.SetRTPCValue("Music_Volume", musicVolume);
        }

        if (whatValue == "Sounds") {
            sfxVolume = volumeSlider.value;
            AkSoundEngine.SetRTPCValue("SFX_Volume", sfxVolume);
        }

    }*/

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        AkSoundEngine.SetRTPCValue("Master_Volume", masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        AkSoundEngine.SetRTPCValue("Music_Volume", musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        AkSoundEngine.SetRTPCValue("SFX_Volume", sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
