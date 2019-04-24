using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief Sets up the logic for selecting different game modes.
 * @author Andrew Alford
 * @date 24/04/19
 * @version 1.0 - 24/04/19
 */
public class GameModeSelection : MonoBehaviour {
    
    //@brief Sets up the dropdown menu.
    void Start() {

        //Get the dropdown menu and clear its options.
        Dropdown gameModeSelect = gameObject.GetComponent<Dropdown>();
        gameModeSelect.ClearOptions();

        //Add in the new game modes.
        gameModeSelect.AddOptions(new List<string> {
            "Classic",
            "Barrage"
        });

        //Change the prefered game mode when the dropdown's value is changed.
        gameModeSelect.onValueChanged.AddListener(delegate {
            PlayerPrefs.SetInt("GAME_MODE", gameModeSelect.value);
        });
    }
}
