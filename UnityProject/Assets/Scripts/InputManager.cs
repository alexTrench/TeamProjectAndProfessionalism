using System;
using UnityEngine;

/**
 * @brief Static helper class to determine 
 *        which inputs are in use.
 * @author Andrew Alford
 * @date 25/03/2019
 * @version 1.0 - 25/03/2019
 */
public static class InputManager {
    
    /**
     * @returns 'true' if the forward input is active.
     */
    public static bool Forward() {
        try {
            return Input.GetAxis("Vertical") > 0;
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
            return Input.GetAxis("Vertical") < 0;
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
            return Input.GetAxis("Horizontal") > 0;
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
            return Input.GetAxis("Horizontal") < 0;
        } catch(ArgumentOutOfRangeException e) {
            Debug.LogError(e);
            return false;
        }
    }
}