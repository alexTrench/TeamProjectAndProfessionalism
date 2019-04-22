using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief   Allows the user to select different 
 *          control presets (for PC)
 * @author  Andrew Alford
 * @date    21/04/19
 * @version 1.1 - 22/04/19
 */
public class PresetControls_PC : MonoBehaviour
{
    //[presetSelect] A drop down to select presets.
    [SerializeField] private Dropdown presetSelect = null;

    [SerializeField] private List<GameObject> controlSchemeOptions = new List<GameObject>();

    Event keyEvent;

    Text buttonText;
    KeyCode newKey;

    private bool waitingForKey = false;

    private enum CONTROL_TYPE {
        POSITIVE, 
        NEGATIVE,
        ALT_POSITIVE,
        ALT_NEGATIVE
    };

    //@brief Sets up the Preset Controls
    private void Start() {
        FindPresets();
        FillOptions();

        //Update the preset options every time the dropdown is changed.
        presetSelect.onValueChanged.AddListener(delegate { FillOptions(); });
    }

    private void UpdateControl(Button btn, string inputID, CONTROL_TYPE controlType) {
        Debug.Log("Updating Control");

        Text btnText = btn.GetComponentInChildren<Text>();

        if (btnText) {
            sendText(btnText);
        }

        startAssignmnet(inputID, btn);
    }

    //@brief Reads the selected control scheme and maps the inputs to the buttons.
    private void FillOptions() {

        //[selectedPreset] The name of the currently selected preset.
        string selectedPreset = presetSelect.options[presetSelect.value].text;

        //Read the json file of the preferred control scheme.
        ControlSchemePreset preferredControlScheme = JsonUtility.FromJson<ControlSchemePreset>(
            File.ReadAllText(
                Application.streamingAssetsPath + 
                "/ControlSchemes/" + selectedPreset + ".json"
            )
        );

        foreach(GameObject action in controlSchemeOptions) {

            Button[] options = action.GetComponentsInChildren<Button>();
            Button option1 = options[0];
            Button option2 = null;
            if (options.Length > 1) { option2 = options[1]; }   

            string[] actionInfo = action.name.Split(' ');

            //Find the appropriate indexes.
            FindIndexes(
                preferredControlScheme, actionInfo[0], 
                out int jsonIndex, out int characterHotKeysIndex
            );


            //Add text to buttons and delagate events to update the text when
            //they are clicked.
            if (actionInfo.Length > 1) {
                Debug.Log(actionInfo[1]);
                switch (actionInfo[1]) {
                    case ("pos"):
                        option1.enabled = true;
                        option1.interactable = true;
                        option1.GetComponentInChildren<Text>().text = 
                            preferredControlScheme.contols[jsonIndex].positive_pc;
                        option1.onClick.AddListener(delegate {
                            UpdateControl(option1, actionInfo[0], CONTROL_TYPE.POSITIVE);
                        });
                        if (option2 != null) {
                            option2.enabled = true;
                            option2.interactable = true;
                            option2.GetComponentInChildren<Text>().text = 
                                preferredControlScheme.contols[jsonIndex].altPositive_pc;
                            option2.onClick.AddListener(delegate {
                                UpdateControl(option2, actionInfo[0], CONTROL_TYPE.ALT_POSITIVE);
                            });
                        }
                        break;
                    case ("neg"):
                        option1.enabled = true;
                        option1.interactable = true;
                        option1.GetComponentInChildren<Text>().text = 
                            preferredControlScheme.contols[jsonIndex].negative_pc;
                        option1.onClick.AddListener(delegate {
                            UpdateControl(option1, actionInfo[0], CONTROL_TYPE.NEGATIVE);
                        });
                        if (option2 != null) {
                            option2.enabled = true;
                            option2.interactable = true;
                            option2.GetComponentInChildren<Text>().text = 
                                preferredControlScheme.contols[jsonIndex].altNegative_pc;
                            option2.onClick.AddListener(delegate {
                                UpdateControl(option2, actionInfo[0], CONTROL_TYPE.ALT_NEGATIVE);
                            });
                        }
                        break;
                    case ("Assault"):
                        option1.enabled = true;
                        option1.interactable = true;
                        option1.GetComponentInChildren<Text>().text =
                            preferredControlScheme.contols[characterHotKeysIndex].positive_pc;
                        option1.onClick.AddListener(delegate {
                            UpdateControl(option1, actionInfo[0], CONTROL_TYPE.POSITIVE);
                        });
                        if (option2 != null) {
                            option2.GetComponentInChildren<Text>().text = "N/A";
                            option2.interactable = false;
                        }
                        break;
                    case ("Heavy"):
                        option1.enabled = true;
                        option1.interactable = true;
                        option1.GetComponentInChildren<Text>().text =
                            preferredControlScheme.contols[characterHotKeysIndex].negative_pc;
                        option1.onClick.AddListener(delegate {
                            UpdateControl(option1, actionInfo[0], CONTROL_TYPE.NEGATIVE);
                        });
                        if (option2 != null) {
                            option2.GetComponentInChildren<Text>().text = "N/A";
                            option2.interactable = false;
                        }
                        break;
                    case ("Light"):
                        option1.enabled = true;
                        option1.interactable = true;
                        option1.GetComponentInChildren<Text>().text =
                            preferredControlScheme.contols[characterHotKeysIndex].altPositive_pc;
                        option1.onClick.AddListener(delegate {
                            UpdateControl(option1, actionInfo[0], CONTROL_TYPE.ALT_POSITIVE);
                        });
                        if (option2 != null) {
                            option2.GetComponentInChildren<Text>().text = "N/A";
                            option2.interactable = false;
                        }
                        break;
                    case ("Demolition"):
                        option1.enabled = true;
                        option1.interactable = true;
                        option1.GetComponentInChildren<Text>().text =
                            preferredControlScheme.contols[characterHotKeysIndex].altNegative_pc;
                        option1.onClick.AddListener(delegate {
                            UpdateControl(option1, actionInfo[0], CONTROL_TYPE.ALT_NEGATIVE);
                        });
                        if (option2 != null) {
                            option2.GetComponentInChildren<Text>().text = "N/A";
                            option2.interactable = false;
                        }
                        break;
                }
            }
            else {
                option1.enabled = true;
                option1.interactable = true;
                option1.GetComponentInChildren<Text>().text = 
                    preferredControlScheme.contols[jsonIndex].positive_pc;
                option1.onClick.AddListener(delegate {
                    UpdateControl(option1, actionInfo[0], CONTROL_TYPE.ALT_NEGATIVE);
                });
                if (option2 != null) {
                    option2.enabled = true;
                    option2.interactable = true;
                    option2.GetComponentInChildren<Text>().text = 
                        preferredControlScheme.contols[jsonIndex].altPositive_pc;
                    option2.onClick.AddListener(delegate {
                        UpdateControl(option2, actionInfo[0], CONTROL_TYPE.ALT_NEGATIVE);
                    });
                }
            }
        }
    }

    private void FindIndexes(
        in ControlSchemePreset controlScheme, in string lookup,
        out int jsonIndex, out int characterHotKeysIndex)
    {
        jsonIndex = 0;
        characterHotKeysIndex = 0;
        for (int i = 0; i < controlScheme.contols.Length; i++) {
            if (lookup.Equals(controlScheme.contols[i].id)) {
                jsonIndex = i;
            }
            if (controlScheme.contols[i].id.Equals("characterHotKeys")) {
                characterHotKeysIndex = i;
            }
        }
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
        List<string> presetOptions = new List<string> { "defualt" };

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

    // Rendering and handling GUI events.
    private void OnGUI()
    {
        keyEvent = Event.current;

        //Track the users input.
        if (keyEvent.isKey && waitingForKey)
        {
            Debug.Log("Key Pressed");
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void startAssignmnet(string keyName, Button btn)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName, btn));
        }
    }

    public void sendText(Text text)
    {
        buttonText = text;
    }

    /**
     * @brief Control statement to wait until 
     *        a user presses a key.
     */
    IEnumerator waitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName, Button btn)
    {
        waitingForKey = true;

        //Stop the corourine from executing until a user
        //presses a key.
        yield return waitForKey();

        //updateKeyBinding(keyName);
        UpdateButtonText(keyName, btn);

        //Wait for a frame before execution ends.
        yield return null;
    }

    private void UpdateButtonText(string keyName, Button btn) {
        btn.GetComponentInChildren<Text>().text = newKey.ToString();
    }

    /**
     * @brief Updates a key and saves the users preferences.
     * @param keyName - The name of the key being updated.
     */
    private void updateKeyBinding(string keyName)
    {
        //foreach (GameInput input in GameManager.GM.inputs)
        //{
        //    if (keyName.Equals(input.name))
        //    {
        //        Debug.Log("updating binding");
        //        input.SetKeyCode(newKey, (GameInput.AVAILABLE_INPUTS)index);
        //        itemsUI[input.GetID()].GetComponentsInChildren<Button>()[index]
        //            .GetComponentInChildren<Text>().text = input.GetInput((GameInput.AVAILABLE_INPUTS)index).ToString();
        //        PlayerPrefs.SetString(
        //            input.GetPlayerPreference((GameInput.AVAILABLE_INPUTS)index),
        //            input.GetInput((GameInput.AVAILABLE_INPUTS)index).ToString()
        //        );
        //    }
        //}
    }
}
