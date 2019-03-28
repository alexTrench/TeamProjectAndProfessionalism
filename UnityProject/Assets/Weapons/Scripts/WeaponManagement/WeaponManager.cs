using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //selected weapon under weapon manager
    //weapons under the manager increament down 
    //the first item under manager is index 0 ,then 1, ect.
    public int selectedWeapon = 0;
    Inventory inventory;
    
    //used for current weapons

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        SelectWeapon();
        CycleWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        //goes through the children of the weapon manager class to see which one need to be visable
        foreach (Transform weapon in transform)
        {

            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                //this will allow us to know which gun is active when buying weapons
                //so we can switch the active one out
                // becasue it starts at the secondary not primary for some reason, this stops that    
                inventory.isActive[i] = true;
            }
            else
            {
                weapon.gameObject.SetActive(false);
                inventory.isActive[i] = false;
            }
            i++;
          
        }

    }

    void CycleWeapon()
    {
        //used later to check if the weapon has been changed
        int previousSelectedWeapon = selectedWeapon;
    
        //scroll wheel used
        if (Input.mouseScrollDelta.y > 0f)
        {
            //checks how many children the weapon manager had, each weapon is one child
            //this will loop the cycle of changing weapons back to the first child
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        //oposite of the above
        if (Input.mouseScrollDelta.y < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        //if weapon has changed we need to call select weapon function
        //to set the correct weapon to be active
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }       
    }
}  


