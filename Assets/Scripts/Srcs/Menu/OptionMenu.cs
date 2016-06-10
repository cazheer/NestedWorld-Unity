using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField]
    private Dropdown resolutionDrop;
    [SerializeField]
    private Slider slider;

    private GameObject menuContainer;

    private float currentSoundVolume = 1f;
    private int currentResolution = 0;

	void Start ()
    {
        menuContainer = GameObject.FindGameObjectWithTag("MenuContainer");
        transform.SetParent(menuContainer.transform);

        // sound
        currentSoundVolume = slider.value;

        // Resolution
        List<string> listOption = new List<string>();
        Resolution[] resolutions = Screen.resolutions;
        string currentRes = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
        int resPosition = 0;
        int i = 0;
        foreach (Resolution res in resolutions)
        {
            string resString = res.width + "x" + res.height;
            listOption.Add(resString);
            if (resString == currentRes)
                resPosition = i;
            i++;
        }
        resolutionDrop.value = currentResolution = resPosition;
        resolutionDrop.AddOptions(listOption);
    }

    public void Save()
    {
        if (currentResolution != resolutionDrop.value)
        {
            Resolution[] resolutions = Screen.resolutions;
            if (resolutionDrop.value < resolutions.Length)
            {
                Screen.SetResolution(resolutions[resolutionDrop.value].width, resolutions[resolutionDrop.value].height, true);
            }
            currentResolution = resolutionDrop.value;
        }
        if (currentSoundVolume != slider.value)
        {
            // change sound
        }
        Return();
    }

    public void Cancel()
    {
        resolutionDrop.value = currentResolution;
        slider.value = currentSoundVolume;
        Return();
    }

    void Return()
    {
        Transform home = menuContainer.transform.FindChild("HomeMenu");
        home.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
