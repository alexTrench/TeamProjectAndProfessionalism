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

    private static InputBinding backwardAxisPC = new InputBinding(
        InputBinding.AXIS.NONE,
        KeyCode.W, KeyCode.S, KeyCode.UpArrow, KeyCode.DownArrow
    );
    private static InputBinding rightAxisPC = new InputBinding(
        InputBinding.AXIS.NONE,
        KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow
    );

    private static InputBinding backwardAxisXbox = new InputBinding(
        InputBinding.AXIS.XBOX_LEFT_STICK_VERTICAL
    );
    private static InputBinding rightAxisXbox = new InputBinding(
        InputBinding.AXIS.XBOX_LEFT_STICK_HORIZONTAL
    );

    private static InputBinding throwGrenade = new InputBinding(
        InputBinding.AXIS.XBOX_LEFT_TRIGGER
    );
    private static InputBinding fireWeapon = new InputBinding(
        InputBinding.AXIS.XBOX_RIGHT_TRIGGER
    );

    private static InputBinding lookForwardAxis = new InputBinding(
        InputBinding.AXIS.XBOX_RIGHT_STICK_VERTICAL
    );
    private static InputBinding lookRightAxis = new InputBinding(
        InputBinding.AXIS.XBOX_RIGHT_STICK_HORIZONTAL
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

    public static float GetBackwardAxis() {
        if(usingXboxOneController()) {
            return backwardAxisXbox.ToFloat();
        } else {
            return backwardAxisPC.ToFloat();
        }
    }

    public static float GetRightAxis() {
        if(usingXboxOneController()) {
            return rightAxisXbox.ToFloat();
        } else {
            return rightAxisPC.ToFloat();
        }
    }

    /**
     * @returns 'true' if the forward input is active.
     */
    public static bool Forward() {
        try {
            if(usingXboxOneController()) {
                return backwardAxisXbox.GetNegative();
            }
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the backward input is active.
     */
    public static bool Backward() {
        try {
            if(usingXboxOneController()) {
                return backwardAxisXbox.GetPositive();
            }
            return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the right input is active.
     */
    public static bool Right() {
        try {
            if(usingXboxOneController()) {
                return rightAxisXbox.GetPositive();
            }
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the left input is active.
     */
    public static bool Left() {
        try {
            if(usingXboxOneController()) {
                return rightAxisXbox.GetNegative();
            }
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool Reload() {
        try {
            if(usingXboxOneController()) {
                return Input.GetKeyDown(KeyCode.JoystickButton2);
            }
            return Input.GetKeyDown(KeyCode.R);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool ThrowGrenade() {
        try {
            if(usingXboxOneController()) {
                return throwGrenade.GetPositive();
            }
            return Input.GetKeyDown(KeyCode.Mouse1);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool Die() {
        try {
            if(usingXboxOneController()) {
                return Input.GetKeyDown(KeyCode.JoystickButton3);
            }
            return Input.GetKeyDown(KeyCode.P);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool NextCharacter() {
        try {
            if(usingXboxOneController()) {
                return Input.GetKeyDown(KeyCode.JoystickButton5);
            }
            return Input.GetKeyDown(KeyCode.E);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    public static bool PreviousCharacter() {
        try {
            return Input.GetKeyDown(KeyCode.Q) ||
                Input.GetKeyDown(KeyCode.JoystickButton4);
        } catch(ArgumentOutOfRangeException e) {
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
        try {
            return Input.GetKey(KeyCode.Mouse0);
        } catch(ArgumentOutOfRangeException e) {
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