using System;
using UnityEngine;

/**
 * @brief   Class to define game inputs which can be
 *          customised at runtime.
 * @author  Andrew Alford
 * @date    26/03/2019
 * @version 2.0 - 19/04/2019
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

    //[positive_pc] The key to be used to make the input 'true' when pressed.
    private KeyCode positive_pc;
    //[negative_pc] The key to be used to make the input 'false' when pressed.
    private KeyCode negative_pc;
    //[altPositive_pc] The other key to be used to make the input 'true' when pressed.
    private KeyCode altPositive_pc;
    //[altNegative_pc] The other key to be used to make the input 'false' when pressed.
    private KeyCode altNegative_pc;
    //[positive_xbox] The key to be used to make the input 'true' when pressed.
    private KeyCode positive_xbox;
    //[negative_xbox] The key to be used to make the input 'false' when pressed.
    private KeyCode negative_xbox;
    //[altPositive_xbox] The other key to be used to make the input 'true' when pressed.
    private KeyCode altPositive_xbox;
    //[altNegative_xbox] The other key to be used to make the input 'false' when pressed.
    private KeyCode altNegative_xbox;

    /**
     * @brief A constructor for a new input.
     * @param initialAxisBinding         - The axis this input will use.
     * @param positive_pc                - The positive key this input will use.
     * @param negative_pc                - The negative key this input will use.
     * @param altPositive_pc             - The other positive key this input will use.
     * @param altNegative_pc             - The other negative key this input will use.
     * @param positive_xbox              - The positive key this input will use.
     * @param negative_xbox              - The negative key this input will use.
     * @param altPositive_xbox           - The other positive key this input will use.
     * @param altNegative_xbox           - The other negative key this input will use.
     */
    public InputBinding(
        AXIS initialAxisBinding, 
        KeyCode positive_pc         = KeyCode.None, 
        KeyCode negative_pc         = KeyCode.None,
        KeyCode altPositive_pc      = KeyCode.None, 
        KeyCode altNegative_pc      = KeyCode.None,
        KeyCode positive_xbox       = KeyCode.None,
        KeyCode negative_xbox       = KeyCode.None,
        KeyCode altPositive_xbox    = KeyCode.None,
        KeyCode altNegative_xbox    = KeyCode.None) {
        SetAxisBinding(initialAxisBinding);
        this.positive_pc = positive_pc;
        this.negative_pc = negative_pc;
        this.altPositive_pc = altPositive_pc;
        this.altNegative_pc = altNegative_pc;
        this.positive_xbox = positive_xbox;
        this.negative_xbox = negative_xbox;
        this.altPositive_xbox = altPositive_xbox;
        this.altNegative_xbox = altNegative_xbox;
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
            case (AXIS.XBOX_DPAD_HORIZONTAL):
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
                return Input.GetKey(positive_pc) ||
                Input.GetKey(altPositive_pc) ||
                Input.GetKey(positive_xbox) ||
                Input.GetKey(altPositive_xbox);
            }
            return (Input.GetAxis(currentAxis) > 0) || 
                Input.GetKey(positive_pc) ||
                Input.GetKey(altPositive_pc) ||
                Input.GetKey(positive_xbox) ||
                Input.GetKey(altPositive_xbox);
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
                return Input.GetKey(negative_pc) ||
                Input.GetKey(altNegative_pc) ||
                Input.GetKey(negative_xbox) ||
                Input.GetKey(altNegative_xbox);
            }
            return (Input.GetAxis(currentAxis) < 0) || 
                Input.GetKey(negative_pc) ||
                Input.GetKey(altNegative_pc) ||
                Input.GetKey(negative_xbox) ||
                Input.GetKey(altNegative_xbox);
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if any of the positive keys 
     *          have been pressed.
     */
    public bool PositiveKeyDown() {
        try {
            return
                Input.GetKeyDown(positive_pc) ||
                Input.GetKeyDown(altPositive_pc) ||
                Input.GetKeyDown(positive_xbox) ||
                Input.GetKeyDown(altPositive_xbox);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    /**
     * @returns 'true' if any of the negative keys 
     *          have been pressed.
     */
    public bool NegativeKeyDown() {
        try {
            return 
                Input.GetKeyDown(negative_pc) ||
                Input.GetKeyDown(altNegative_pc) ||
                Input.GetKeyDown(negative_xbox) ||
                Input.GetKeyDown(altNegative_xbox);
        }
        catch (ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }

    //@returns the binding for the positive key.
    public KeyCode GetPositiveKey_pc() => positive_pc;
    //@returns the binding for the alt positive key.
    public KeyCode GetAltPositiveKey_pc() => altPositive_pc;
    //@returns the binding for the negative key.
    public KeyCode GetNegativeKey_pc() => negative_pc;
    //@returns the binding for the alt negative key.
    public KeyCode GetAltNegativeKey_pc() => altNegative_pc;
    //@returns the binding for the positive key.
    public KeyCode GetPositiveKey_xbox() => positive_xbox;
    //@returns the binding for the alt positive key.
    public KeyCode GetAltPositiveKey_xbox() => altPositive_xbox;
    //@returns the binding for the negative key.
    public KeyCode GetNegativeKey_xbox() => negative_xbox;
    //@returns the binding for the alt negative key.
    public KeyCode GetAltNegativeKey_xbox() => altNegative_xbox;

    /**
     * @returns The state of this input in float format.
     *          +1   = positive
     *          +0   = not in use
     *          -1   = negative
     */
    public float ToFloat() {
        if(Input.GetKey(positive_pc) || Input.GetKey(altPositive_pc)) {
            return 1.0f;
        } else if(Input.GetKey(negative_pc) || Input.GetKey(altNegative_pc)) {
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
        "Axis:\t"               + currentAxis                   + "\n" +
        "Positive pc:\t"        + positive_pc.ToString()        + "\n" +
        "Negative pc:\t"        + negative_pc.ToString()        + "\n" +
        "Alt Positive pc:\t"    + altPositive_pc.ToString()     + "\n" +
        "Alt Negative pc:\t"    + altNegative_pc.ToString()     + "\n" +
        "Positive xbox:\t"      + positive_xbox.ToString()      + "\n" +
        "Negative xbox:\t"      + negative_xbox.ToString()      + "\n" +
        "Alt Positive xbox:\t"  + altPositive_xbox.ToString()   + "\n" +
        "Alt Negative xbox:\t"  + altNegative_xbox.ToString();
    }
}
