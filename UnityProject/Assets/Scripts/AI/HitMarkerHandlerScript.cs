using UnityEngine;

public class HitMarkerHandlerScript : MonoBehaviour
{
    public HitMarkerScript m_leftHitMarker;
    public HitMarkerScript m_rightHitMarker;
    
    public void EnableLeftHitMarker()
    {
        m_leftHitMarker.EnableMarker();
    }

    public void DisableLeftHitMarker()
    {
        m_leftHitMarker.DisableMarker();
    }

    public void EnableRightHitMarker()
    {
        m_rightHitMarker.EnableMarker();
    }

    public void DisableRightHitMarker()
    {
        m_rightHitMarker.DisableMarker();
    }

    public void EnableBothHitMarkers()
    {
        m_leftHitMarker.EnableMarker();
        m_rightHitMarker.EnableMarker();
    }

    public void DisableBothHitMarkers()
    {
        m_leftHitMarker.DisableMarker();
        m_rightHitMarker.DisableMarker();
    }
}
