using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief   Allows the user to select different 
 *          control presets (for PC)
 * @author  Andrew Alford
 * @date    22/04/19
 * @version 1.0 - 22/04/19
 */
public class PresetControls_PC : MonoBehaviour
{
    //[presetSelect] A drop down to select presets.
    [SerializeField] private Dropdown presetSelect = null;

    [SerializeField] private List<GameObject> controlSchemeOptions = new List<GameObject>();

    //@brief Sets up the Preset Controls
    private void Start() {
        foreach(GameObject GO in controlSchemeOptions) {
            Debug.Log(GO.name);
        }
        FindPresets();
    }

    /**
     * @brief Searches through the player's preferences
     *        to find the presets they have previously 
     *        saved.
     */
    private void FindPresets() {
        //[numPresets] How many presets the player has 
        //previously saved.
        int numPresets = PlayerPrefs.GetInt("numPresets", 0);

        //[presetOptions] Holds the file names of the 
        //player's preferred prests.
        List<string> presetOptions = new List<string>();

        //Add the defualt preset.
        presetOptions.Add("defualt");

        //Find all other presets.
        for (int i = 0; i < numPresets; i++) {
            //[presetName] Stores the name of the preset.
            string presetName = PlayerPrefs.GetString("preset" + i, null);

            //Add the preset.
            if(!presetName.Equals(null)) {
                presetOptions.Add(presetName);
            }
        }

        //Add all options to the dropdown list.
        if(presetSelect != null && presetSelect.isActiveAndEnabled) {
            presetSelect.ClearOptions();
            presetSelect.AddOptions(presetOptions);
        }
    }
}
