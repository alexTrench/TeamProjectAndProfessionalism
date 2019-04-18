using System;
using UnityEngine;

/**
 * @brief   Class to define game inputs which can be
 *          customised at runtime.
 * @author  Andrew Alford
 * @date    26/03/2019
 * @version 1.0 - 26/03/2019
 */
public class InputBinding {

    //[AXIS] All the different axis that an input can use.
    public enum AXIS {
        MOUSE_HORIZONTAL,
        MOUSE_VERTICAL,
        MOUSE_WHEEL,
        XBOX_DPAD_HORIZONTAL,
        XBOX_DPAD_VERTICAL,
        XBOX_LEFT_STICK_HORIZONTAL,
        XBOX_LEFT_STICK_VERTICAL,
        XBOX_RIGHT_STICK_HORIZONTAL,
        XBOX_RIGHT_STICK_VERTICAL,
        XBOX_LEFT_TRIGGER,
        XBOX_RIGHT_TRIGGER,
        NONE
    }

    //[currentAxis] The axis this input is currently using.
    private string currentAxis;

    //[positive] The key to be used to make the input 'true' when pressed.
    private KeyCode positive;
    //[negative] The key to be used to make the input 'false' when pressed.
    private KeyCode negative;
    //[altPositive] The other key to be used to make the input 'true' when pressed.
    private KeyCode altPositive;
    //[altNegative] The other key to be used to make the input 'false' when pressed.
    private KeyCode altNegative;

    /**
     * @brief A constructor for a new input.
     * @param initialAxisBinding    - The axis this input will use.
     * @param positive              - The positive key this input will use.
     * @param negative              - The negative key this input will use.
     * @param altPositive           - The other positive key this input will use.
     * @param altNegative           - The other negative key this input will use.
     */
    public InputBinding(
        AXIS initialAxisBinding, 
        KeyCode positive      = KeyCode.None, 
        KeyCode negative      = KeyCode.None,
        KeyCode altPositive   = KeyCode.None, 
        KeyCode altNegative   = KeyCode.None) {
        SetAxisBinding(initialAxisBinding);
        this.positive = positive;
        this.negative = negative;
        this.altPositive = altPositive;
        this.altNegative = altNegative;
    }

    /**
     * @brief Allows the inputs axis to be changed.
     * @param axis - The new axis to be used.
     */
    public void SetAxisBinding(AXIS axis) {
        switch(axis) {
            case(AXIS.MOUSE_HORIZONTAL): 
                currentAxis = "MOUSE_HORIZONTAL"; 
                break;
            case(AXIS.MOUSE_VERTICAL):
                currentAxis = "MOUSE_VERTICAL";
                break;
            case (AXIS.MOUSE_WHEEL):
                currentAxis = "MOUSE_WHEEL";
                break;
            case(AXIS.XBOX_DPAD_HORIZONTAL):
                currentAxis = "XBOX_DPAD_HORIZONTAL";
                break;
            case(AXIS.XBOX_DPAD_VERTICAL):
                currentAxis = "XBOX_DPAD_VERTICAL";
                break;
            case(AXIS.XBOX_LEFT_STICK_HORIZONTAL):
                currentAxis = "XBOX_LEFT_STICK_HORIZONTAL";
                break;
            case(AXIS.XBOX_LEFT_STICK_VERTICAL):
                currentAxis = "XBOX_LEFT_STICK_VERTICAL";
                break;
            case(AXIS.XBOX_RIGHT_STICK_HORIZONTAL):
                currentAxis = "XBOX_RIGHT_STICK_HORIZONTAL";
                break;
            case(AXIS.XBOX_RIGHT_STICK_VERTICAL):
                currentAxis = "XBOX_RIGHT_STICK_VERTICAL";
                break;
            case(AXIS.XBOX_LEFT_TRIGGER):
                currentAxis = "XBOX_LEFT_TRIGGER";
                break;
            case(AXIS.XBOX_RIGHT_TRIGGER):
                currentAxis = "XBOX_RIGHT_TRIGGER";
                break;                               
            case(AXIS.NONE):
                currentAxis = "";
                break;                                                                                 
        }
    }

    /**
     * @returns 'true' if the axis is currently positive,
     *          or either positive key is currently 
     *          being pressed.
     */
    public bool GetPositive() {
        try {
            if(currentAxis.Equals("")) {
                return Input.GetKey(positive) ||
                Input.GetKey(altPositive);
            }
            return (Input.GetAxis(currentAxis) > 0.0f) || 
                Input.GetKey(positive) ||
                Input.GetKey(altPositive);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if the axis is currently negative,
     *          or either negative key is currently 
     *          being pressed.
     */
    public bool GetNegative() {
        try {
            if(currentAxis.Equals("")) {
                return Input.GetKey(negative) ||
                Input.GetKey(altNegative);
            }
            return (Input.GetAxis(currentAxis) < 0) || 
                Input.GetKey(negative) ||
                Input.GetKey(altNegative);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns the bindings positive key.
    public KeyCode GetPositiveKey() => positive;
    //@returns the bindings alt positive key.
    public KeyCode GetAltPositiveKey() => altPositive;
    //@reutns the bindings negative key.
    public KeyCode GetNegativeKey() => negative;
    //@returns the bindings alt negative key.
    public KeyCode GetAltNegativeKey() => altNegative;

    /**
     * @returns The state of this input in float format.
     *          +1   = positive
     *          +0   = not in use
     *          -1   = negative
     */
    public float ToFloat() {
        if(Input.GetKey(positive) || Input.GetKey(altPositive)) {
            return 1.0f;
        } else if(Input.GetKey(negative) || Input.GetKey(altNegative)) {
            return -1.0f;
        }
        try {
            if(currentAxis.Equals("")) {
                return 0.0f;
            }
            return Input.GetAxis(currentAxis);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return 0.0f;
        }
    }

    /**
     * @returns Information about this input (overridden from the superclass).
     */
    public override string ToString() {
        return "\n" +
        "Axis:\t"           + currentAxis               + "\n" +
        "Positive:\t"       + positive.ToString()       + "\n" +
        "Negative:\t"       + negative.ToString()       + "\n" +
        "Alt Positive:\t"   + altPositive.ToString()    + "\n" +
        "Alt Negative:\t"   + altNegative.ToString();
    }
}
