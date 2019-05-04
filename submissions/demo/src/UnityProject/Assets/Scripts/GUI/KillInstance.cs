using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillInstance : MonoBehaviour
{
    public void KillGame() => GameplayManager.GM.EndGame();
}
