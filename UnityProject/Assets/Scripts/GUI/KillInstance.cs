using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillInstance : MonoBehaviour
{
    public void KillGame()
    {
        Wave.WAVE_ID = 0;
        GameplayManager.GM = null;
    }
}
