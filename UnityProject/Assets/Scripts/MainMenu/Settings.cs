using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Text label;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution( int requiredResolutionIndex)
    {
        Resolution requiredResolution = resolutions[requiredResolutionIndex];

        Screen.SetResolution(requiredResolution.width, requiredResolution.height, Screen.fullScreen);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        
        if(isFullscreen)
        {
            label.text = "Fullscreen Mode";
        } else
        {
            label.text = "Windowed Mode";
        }

        
        
        Screen.fullScreen = isFullscreen;
    }
}
