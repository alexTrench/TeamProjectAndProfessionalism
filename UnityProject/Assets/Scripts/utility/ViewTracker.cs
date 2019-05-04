using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Camera class to check if objects are in its frustrum
 * @author Andrew Alford
 * @date 23/04/2019
 * @version 1.0 - 23/04/2019
 */
public class ViewTracker : MonoBehaviour {
    
    public bool IsInView(Transform t) {
        Vector3 screenPoint = FindObjectOfType<Camera>().WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1; ;
    }

    public List<Transform> GetPointsInRange(Transform[] transforms) {
        List<Transform> pointsInRange = new List<Transform>();
        foreach(Transform t in transforms) {
            if(IsInView(t)) { pointsInRange.Add(t); }
        }
        return pointsInRange;
    }

    public List<Transform> GetPointsNotInRange(Transform[] transforms) {
        List<Transform> pointsNotInRange = new List<Transform>();
        foreach(Transform t in transforms) {
            if(!IsInView(t)) { pointsNotInRange.Add(t); }
        }
        return pointsNotInRange;
    }
}
