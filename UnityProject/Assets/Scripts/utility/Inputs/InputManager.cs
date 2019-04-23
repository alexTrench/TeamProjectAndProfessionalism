using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**
 * @brief   Static helper class to determine 
 *          which inputs are in use.
 * @author  Andrew Alford
 * @date    25/03/2019
 * @version 1.2 - 21/04/2019
 */
public static class InputManager {

    //[inputs] A list of all game inputs.
    public static List<InputBinding> inputs = new List<InputBinding>();

    /**
     * @brief Reads through the users preferred control 
     *        scheme and assigns the controls.
     */
    public static void AssignControls() {

        //[scheme] Holds the player's preferred control scheme.
        string scheme = PlayerPrefs.GetString("preferredScheme", "defualt");

        //Read the json file of the preferred control scheme.
        ControlSchemePreset preferredControlScheme = JsonUtility.FromJson<ControlSchemePreset>(
            File.ReadAllText(Application.streamingAssetsPath + "/ControlSchemes/" + scheme + ".json")
        );

        //Extract each input from the control scheme.
        foreach (InputData input in preferredControlScheme.contols) {
            inputs.Add((InputBinding)ScriptableObject.CreateInstance("InputBinding"));
            inputs[inputs.Count - 1].Init(input);
        }
    }

    /**
     * @brief Retrieves an Input Binding via its ID.
     * @param id - The ID of the Input Binding being retrieved.
     * @returns the requested Input Binding.
     */
    private static InputBinding GetInputByID(string id) {
        try {
            foreach(InputBinding input in inputs) {
                if(input.GetID().Equals(id)) {
                    return input;
                }
            }
            throw new Exception("Input not found");
        }
        catch(Exception e) {
            Debug.LogError(e);
            return null;
        }
    }

    /**
     * @brief Rotates a transform to look in the direction 
     *        of a given axis.
     * @param transform - The transform to be rotated.
     */
    public static void LookAtAxis(Transform transform) {
        if (UsingXboxOneController()) {
            //Rotate with controller
            Vector3 playerDirection = Vector3.right *
            GetInputByID("lookRight").ToFloat() +
            Vector3.forward * -GetInputByID("lookForward").ToFloat();

            //If the player has moved.
            if (playerDirection.sqrMagnitude > 0.0f) {
                transform.rotation = Quaternion.LookRotation(
                    playerDirection,
                    Vector3.up
                );
            }
        }
        else {
            Plane floor = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

            //Rotate with mouse
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (floor.Raycast(cameraRay, out float rayLength)) {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(
                    pointToLook.x,
                    transform.position.y,
                    pointToLook.z
                ));
            }
        }
    }

    public static float GetBackwardAxis() => GetInputByID("forwardMovement").ToFloat();

    public static float GetRightAxis() => GetInputByID("strafeMovement").ToFloat();

    //@returns 'true' if the player should move forward.
    public static bool Forward() {
        try {
            if(GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("forwardMovement").GetPositive();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should move backward.
    public static bool Backward() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("forwardMovement").GetNegative();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should move right.
    public static bool Right() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("strafeMovement").GetPositive();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should move left.
    public static bool Left() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("strafeMovement").GetNegative();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should throw a grenade.
    public static bool ThrowGrenade() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("throwGrenade").GetPositive();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the next character.
    public static bool NextCharacter() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("swapCharacter").PositiveKeyDown();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the previous character.
    public static bool PreviousCharacter() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("swapCharacter").NegativeKeyDown();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey1() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("characterHotKeys").GetPositiveKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("characterHotKeys").GetPositiveKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey2() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("characterHotKeys").GetAltPositiveKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("characterHotKeys").GetAltPositiveKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey3() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("characterHotKeys").GetNegativeKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("characterHotKeys").GetNegativeKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey4() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("characterHotKeys").GetAltNegativeKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("characterHotKeys").GetAltNegativeKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey1() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("abilityHotKeys").GetPositiveKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("abilityHotKeys").GetPositiveKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey2() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("abilityHotKeys").GetAltPositiveKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("abilityHotKeys").GetAltPositiveKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey3() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("abilityHotKeys").GetNegativeKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("abilityHotKeys").GetNegativeKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey4() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return Input.GetKeyDown(GetInputByID("abilityHotKeys").GetAltNegativeKey_pc()) ||
                    Input.GetKeyDown(GetInputByID("abilityHotKeys").GetAltNegativeKey_xbox());
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should fire their weapon.
    public static bool FireWeapon() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("fireWeapon").GetPositive();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap their weapons.
    public static bool SwapWeapon() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("swapWeapon").PositiveKeyDown();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player has interacted with something.
    public static bool Interact() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("interact").PositiveKeyDown();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player goes up on the menu.
    public static bool MenuUp() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("menuMovement").GetPositive();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player goes down on the menu.
    public static bool MenuDown() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("menuMovement").GetNegative();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player selects an item on the menu.
    public static bool MenuSelect() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("menuInteract").GetPositive();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }
    
    //@returns 'true' if the player goes back to the previous menu.
    public static bool MenuBack() {
        try {
            if (GameplayManager.GM != null && GameplayManager.GM.isActiveAndEnabled) {
                return GetInputByID("menuInteract").GetNegative();
            } else {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player is currently using an xbox controller.
    public static bool UsingXboxOneController() {

         string[] names = Input.GetJoystickNames();
         for (int i = 0; i < names.Length; i++) {
             if (names[i].Length == 33) {
                 return true;
             }
         }
         return false;
    }
}