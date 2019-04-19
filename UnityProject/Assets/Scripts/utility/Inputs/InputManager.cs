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
        InputBinding.AXIS.XBOX_LEFT_STICK_VERTICAL,                     //axis
        KeyCode.W, KeyCode.S, KeyCode.UpArrow, KeyCode.DownArrow,       //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None          //xbox
    );
    private static InputBinding rightAxis = new InputBinding(
        InputBinding.AXIS.XBOX_LEFT_STICK_HORIZONTAL,                   //axis
        KeyCode.D, KeyCode.A, KeyCode.RightArrow, KeyCode.LeftArrow,    //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None          //xbox
    );

    private static InputBinding lookForwardAxis = new InputBinding(
        InputBinding.AXIS.XBOX_RIGHT_STICK_VERTICAL,                //axis
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,     //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None      //xbox
    );
    private static InputBinding lookRightAxis = new InputBinding(
        InputBinding.AXIS.XBOX_RIGHT_STICK_HORIZONTAL,              //axis
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None,     //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None      //xbox
    );

    private static InputBinding swapCharacter = new InputBinding(
        InputBinding.AXIS.NONE,                                                         //axis
        KeyCode.E, KeyCode.None, KeyCode.Q, KeyCode.None,                               //pc
        KeyCode.JoystickButton5, KeyCode.None, KeyCode.JoystickButton4, KeyCode.None    //xbox
    );

    private static InputBinding throwGrenade = new InputBinding(
        InputBinding.AXIS.XBOX_LEFT_TRIGGER,                            //axis
        KeyCode.Mouse1, KeyCode.None, KeyCode.G, KeyCode.None,          //pc
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None          //xbox
    );

    private static InputBinding fireWeapon = new InputBinding(
        InputBinding.AXIS.XBOX_RIGHT_TRIGGER,                           //axis
        KeyCode.Mouse0, KeyCode.None, KeyCode.None, KeyCode.None,       //PC
        KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None          //xbox
    );

    private static InputBinding swapWeapon = new InputBinding(
        InputBinding.AXIS.MOUSE_WHEEL,                                      //axis
        KeyCode.Y, KeyCode.None, KeyCode.None, KeyCode.None,                //pc
        KeyCode.JoystickButton3, KeyCode.None, KeyCode.None, KeyCode.None   //xbox
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

    /**
     * @returns 'true' if the forward input is active.
     */
    public static bool Forward() {
        try { return backwardAxis.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the backward input is active.
     */
    public static bool Backward() {
        try { return backwardAxis.GetNegative(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the right input is active.
     */
    public static bool Right() {
        try { return rightAxis.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the left input is active.
     */
    public static bool Left() {
        try { return rightAxis.GetNegative(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool ThrowGrenade() {
        try { return throwGrenade.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool NextCharacter() {
        try { return swapCharacter.PositiveKeyDown(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool PreviousCharacter() {
        try { return swapCharacter.NegativeKeyDown(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool CharacterHotKey1() {
        try {
            return Input.GetKeyDown(KeyCode.Alpha1);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool CharacterHotKey2() {
        try {
            return Input.GetKeyDown(KeyCode.Alpha2);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool CharacterHotKey3() {
        try {
            return Input.GetKeyDown(KeyCode.Alpha3);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool CharacterHotKey4() {
        try {
            return Input.GetKeyDown(KeyCode.Alpha4);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool AbilityHotKey1() {
        try {
            return Input.GetKeyDown(KeyCode.Z);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool AbilityHotKey2() {
        try {
            return Input.GetKeyDown(KeyCode.X);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool AbilityHotKey3() {
        try {
            return Input.GetKeyDown(KeyCode.C);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool AbilityHotKey4() {
        try {
            return Input.GetKeyDown(KeyCode.V);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool FireWeapon() {
        try { return fireWeapon.GetPositive(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool SwapWeapon() {
        try { return swapWeapon.PositiveKeyDown(); }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool MenuUp() {
        try {
            return Input.GetKeyDown(KeyCode.UpArrow);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool MenuDown() {
        try {
            return Input.GetKeyDown(KeyCode.DownArrow);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool MenuSelect() {
        try {
            return Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.KeypadEnter);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool MenuBack() {
        try {
            return Input.GetKeyDown(KeyCode.Escape) ||
                Input.GetKeyDown(KeyCode.Backspace);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

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