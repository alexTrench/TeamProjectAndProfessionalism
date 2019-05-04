using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    //all the obkects the make up the weapon system
    //game controller stores all the guns for the game as well as their id's
    //and prebafs for bullets and weapons
    GameObject gameController;
    //used to store the instanciation object when attaching to the weapon manager slots
    GameObject primaryWeapon;
    //used as a tempory storage for the weapon swapped out is inventory is full
    GameObject SwapWeapon;
    //used to get the database component from the game controller
    weaponDatabase database;
    //used to get the weapon manager, to attach weapons to the children components
    WeaponManager manager;
    //inventory, to see if the weapon slots are full or not
    Inventory inventory;


    
    //used to store the id of the wall gun from the collider get component
    int id;
    //used on the collison of the wall guns
    //= true when in the collider box to pickup weapon
    bool inPickupRange = false;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();
        manager = GetComponentInChildren<WeaponManager>();
        inventory = GetComponentInParent<Inventory>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (inPickupRange == true))
        {
  
        

         //this function checks if the inventory is full
         //and if so it replaces the weapon with the one from the wall
         ReplaceFromWall();

         //picks up the item from the wall
         //only if their is space in the inventory
         PickupFromWall();

        }
    }

    //when player collides with hit box
    private void OnCollisionEnter(Collision collision)
    {
        //if the other collision is a wall gun which can be purchased
        if (collision.gameObject.tag == "WallGun")
        {
           
            id = collision.transform.GetComponent<ItemId>().itemId;
            Debug.Log("WallGunCollsion" + "id = " + id);
            inPickupRange = true;
        }
   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "WallGun")
        {
            Debug.Log("Left Buy Zone");
            inPickupRange = false;
        }
    }

    void PickupFromWall()
    {

        //checks all of the inventory slots to see if any are free
        for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false && database.weapons[id].isPurchased == false)
                {
                   

                    //weapons that are these id's have a wired 90 angle,
                    //so this spawns them in differently to compencate          
                    if(database.weapons[id].itemID == 4 || database.weapons[id].itemID == 5 || database.weapons[id].itemID == 6)
                    {
                       //this spawns the weapon on the primary or secondary inventory slot
                       //at a rotated angle to take into consideration the -90 degree of the gun when imported
                       primaryWeapon = Instantiate(database.weapons[id].weaponObject, (manager.transform.position - new Vector3(-0.2f, 0.5f, 0.40f)), manager.transform.rotation * Quaternion.Euler(0,-90,0));
                       primaryWeapon.transform.SetParent(inventory.slots[i].transform);
                       //Debug.Log(inventory.slots[i].transform.position);
                    }
                    else
                    {
                       //these guns are the right allignment so no need to mess with the rotation
                       primaryWeapon = Instantiate(database.weapons[id].weaponObject, (manager.transform.position - new Vector3(-0.2f, 0.5f, 0.25f)), manager.transform.rotation);
                       primaryWeapon.transform.SetParent(inventory.slots[i].transform);
                       //Debug.Log(inventory.slots[i].transform.position);
                    }
                     //sets weapon as purchased so cannot buy again
                     database.weapons[id].isPurchased = true;
                     //item can be added to inventory
                     //once picked up set inventory full
                     inventory.isFull[i] = true;

                break;
                }             
            }      
    }

    //used if all gun slots are taken and new weapon bought
    void ReplaceFromWall()
    {
        //goes through each of the inventory slots 
        //used to check to see if all slots are full
        //+1 added so we can go past the full length of the array
        if(NoSpace() == true && database.weapons[id].isPurchased == false)
        {
            Debug.Log("Deleting Active Weapon");
          
            //checks through to see which slot was active when purchase made
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isActive[i] == true)
                {

                    //removes the weapon from the primary or secondary slot, dependent on which was activer
                    //getchild(0) is just the first child, since there is only ever going to be one child for each one this is fine
                    Debug.Log(inventory.slots[i].transform.GetChild(0).gameObject);
                    SwapWeapon = inventory.slots[i].transform.GetChild(0).gameObject;
                    Destroy(SwapWeapon);
                    //need to set back the is full boolean of the same inventory slot
                    //only for now 
                    inventory.isFull[i] = false;
                    //Needs get the gun id and reset that   
                    int itemId = inventory.slots[i].gameObject.GetComponentInChildren<ItemId>().itemId;
                    Debug.Log(itemId);
                    database.weapons[itemId].isPurchased = false;
                }
            }
        }
        else
        {
            Debug.Log("Space");
        }

    }

    //used is gun already of the same type in inventory
    void RefillAmmo()
    {


    }

    private bool NoSpace()
    {
        for (int i = 0; i < inventory.slots.Length + 1; i++)
        {
            //if past the full length of the array
            //it is becasue the is full bool is true on both
            //so we can say to the console its full
            //pretty dodgy code
            if (i < 2)
            {
                if (inventory.isFull[i] == true)
                {

                }
                else
                {
                    //if any of the inventory slots are not full
                    //break the loop 
                    return false;
                  
                }
            }
            //if i == 2 this means the inventory must be full
            if (i >= 2)
            {
                Debug.Log("Full Inventory" + inventory.slots);
                return true;
            }
        }
        //if all else returns nothing out the ordinary, must not have a full inventory
        return false;
    }
}

