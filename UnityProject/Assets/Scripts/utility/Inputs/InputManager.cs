using System;
using UnityEngine;

/**
 * @brief   Static helper class to determine 
 *          which inputs are in use.
 * @author  Andrew Alford
 * @date    25/03/2019
 * @version 1.1 - 12/04/2019
 */
public static class InputManager {   

    private static InputBinding backwardAxis = new InputBinding(
        "forward movement",                                             //ID
        InputBinding.AXIS.XBOX_LEFT_STICK_VERTICAL,                     //axis
        KeyCode.W, KeyCode.S, KeyCode.UpArrow, KeyCode.DownArrow,       //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,         //xbox
        true                                                            //rebindable
    );
    private static InputBinding rightAxis = new InputBinding(
        "strafe movement",                                              //ID
        InputBinding.AXIS.XBOX_LEFT_STICK_HORIZONTAL,                   //axis
        KeyCode.D, KeyCode.A, KeyCode.RightArrow, KeyCode.LeftArrow,    //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,         //xbox
        true                                                            //rebindable
    );

    private static InputBinding lookForwardAxis = new InputBinding(
        "look forward",                                             //ID
        InputBinding.AXIS.XBOX_RIGHT_STICK_VERTICAL,                //axis
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,     //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,     //xbox
        false                                                       //rebindable
    );
    private static InputBinding lookRightAxis = new InputBinding(
        "look right",                                               //ID
        InputBinding.AXIS.XBOX_RIGHT_STICK_HORIZONTAL,              //axis
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,     //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,     //xbox
        false                                                       //rebindable
    );

    private static InputBinding swapCharacter = new InputBinding(
        "swap character",                                                               //ID
        InputBinding.AXIS.NONE,                                                         //axis
        KeyCode.E, KeyCode.None, KeyCode.Q, KeyCode.None,                               //pc
        KeyCode.JoystickButton5, KeyCode.None, KeyCode.JoystickButton4, KeyCode.None,   //xbox
        true                                                                            //rebindable
    );

    private static InputBinding throwGrenade = new InputBinding(
        "throw grenade",                                                //ID
        InputBinding.AXIS.XBOX_LEFT_TRIGGER,                            //axis
        KeyCode.Mouse1, KeyCode.None, KeyCode.G, KeyCode.None,          //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,         //xbox
        true                                                            //rebindable
    );

    private static InputBinding fireWeapon = new InputBinding(
        "fire weapon",                                                  //ID
        InputBinding.AXIS.XBOX_RIGHT_TRIGGER,                           //axis
        KeyCode.Mouse0, KeyCode.None, KeyCode.None, KeyCode.None,       //PC
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,         //xbox
        true                                                            //rebindable
    );

    private static InputBinding swapWeapon = new InputBinding(
        "swap weapon",                                                      //ID
        InputBinding.AXIS.MOUSE_WHEEL,                                      //axis
        KeyCode.Y, KeyCode.None, KeyCode.None, KeyCode.None,                //pc
        KeyCode.JoystickButton3, KeyCode.None, KeyCode.None, KeyCode.None,  //xbox
        true                                                                //rebindable
    );  

    private static InputBinding characterHotKeys = new InputBinding(
        "character hot keys",                                               //ID
        InputBinding.AXIS.NONE,                                             //axis
        KeyCode.Alpha1, KeyCode.Alpha3, KeyCode.Alpha2, KeyCode.Alpha4,     //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,             //xbox
        true                                                                //rebindable
    );

    private static InputBinding abilities = new InputBinding(
        "ability hot keys",                                                 //ID
        InputBinding.AXIS.NONE,                                             //axis
        KeyCode.Z, KeyCode.C, KeyCode.X, KeyCode.V,                         //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,             //xbox
        true                                                                //rebindable
    );

    private static InputBinding menuMovement = new InputBinding(
        "menu movement",                                                    //ID
        InputBinding.AXIS.XBOX_LEFT_STICK_VERTICAL,                         //axis
        KeyCode.UpArrow, KeyCode.W, KeyCode.DownArrow, KeyCode.S,           //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,             //xbox
        false                                                               //rebindable
    );

    private static InputBinding menuInteract = new InputBinding(
        "menu interact",                                                                //ID
        InputBinding.AXIS.NONE,                                                         //axis
        KeyCode.Return , KeyCode.KeypadEnter, KeyCode.Escape, KeyCode.Backspace,        //pc
        KeyCode.JoystickButton0, KeyCode.None, KeyCode.JoystickButton1, KeyCode.None,   //xbox
        false                                                                           //rebindable
    );

    /**
     * @brief Rotates a transform to look in the direction 
     *        of a given axis.
     * @param transform - The transform to be rotated.
     */
    public static void LookAtAxis(Transform transform) {
        if (usingXboxOneController()) {
            //Rotate with controller
            Vector3 playerDirection = Vector3.right *
            lookRightAxis.ToFloat() +
            Vector3.forward * -lookForwardAxis.ToFloat();

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

    public static float GetBackwardAxis() => backwardAxis.ToFloat();

    public static float GetRightAxis() => rightAxis.ToFloat();

    //@returns 'true' if the player should move forward.
    public static bool Forward() {
        try { return backwardAxis.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should move backward.
    public static bool Backward() {
        try { return backwardAxis.GetNegative(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should move right.
    public static bool Right() {
        try { return rightAxis.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should move left.
    public static bool Left() {
        try { return rightAxis.GetNegative(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should throw a grenade.
    public static bool ThrowGrenade() {
        try { return throwGrenade.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the next character.
    public static bool NextCharacter() {
        try { return swapCharacter.PositiveKeyDown(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the previous character.
    public static bool PreviousCharacter() {
        try { return swapCharacter.NegativeKeyDown(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey1() {
        try {
            return Input.GetKeyDown(characterHotKeys.GetPositiveKey_pc()) ||
                Input.GetKeyDown(characterHotKeys.GetPositiveKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey2() {
        try {
            return Input.GetKeyDown(characterHotKeys.GetAltPositiveKey_pc()) ||
                Input.GetKeyDown(characterHotKeys.GetAltPositiveKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey3() {
        try {
            return Input.GetKeyDown(characterHotKeys.GetNegativeKey_pc()) ||
                Input.GetKeyDown(characterHotKeys.GetNegativeKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap to the specific character.
    public static bool CharacterHotKey4() {
        try {
            return Input.GetKeyDown(characterHotKeys.GetAltNegativeKey_pc()) ||
                Input.GetKeyDown(characterHotKeys.GetAltNegativeKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey1() {
        try {
            return Input.GetKeyDown(abilities.GetPositiveKey_pc()) ||
                Input.GetKeyDown(abilities.GetPositiveKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey2() {
        try {
            return Input.GetKeyDown(abilities.GetAltPositiveKey_pc()) ||
                Input.GetKeyDown(abilities.GetAltPositiveKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey3() {
        try {
            return Input.GetKeyDown(abilities.GetNegativeKey_pc()) ||
                Input.GetKeyDown(abilities.GetNegativeKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should use a specific ability.
    public static bool AbilityHotKey4() {
        try {
            return Input.GetKeyDown(abilities.GetAltNegativeKey_pc()) ||
                Input.GetKeyDown(abilities.GetAltNegativeKey_xbox());
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should fire their weapon.
    public static bool FireWeapon() {
        try { return fireWeapon.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player should swap their weapons.
    public static bool SwapWeapon() {
        try { return swapWeapon.PositiveKeyDown(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player goes up on the menu.
    public static bool MenuUp() {
        try { return menuMovement.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player goes down on the menu.
    public static bool MenuDown() {
        try { return menuMovement.GetNegative(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player selects an item on the menu.
    public static bool MenuSelect() {
        try { return menuInteract.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }
    
    //@returns 'true' if the player goes back to the previous menu.
    public static bool MenuBack() {
        try { return menuInteract.GetNegative(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns 'true' if the player is currently using an xbox controller.
    public static bool usingXboxOneController() {

         string[] names = Input.GetJoystickNames();
         for (int i = 0; i < names.Length; i++) {
             if (names[i].Length == 33) {
                 return true;
             }
         }
         return false;
    }
}