using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPickup : MonoBehaviour
{
    public int ItemID;
    public AudioClip audioClip;
    public weaponDatabase weaponList;


    private void OnTriggerEnter(Collider other)
    {
        WeaponManager manageList = other.GetComponentInChildren<WeaponManager>();

        if(manageList != null)
        {
            Debug.Log("not empty");
        }
        else
        {
            Debug.Log("empty");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
