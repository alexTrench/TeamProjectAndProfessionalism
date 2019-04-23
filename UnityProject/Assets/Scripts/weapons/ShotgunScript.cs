using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    //the same id as what the shotgun is in the gamecontroller database of weapons
    int id = 1;
    public GameObject gameController;
    weaponDatabase database;
    //amount of shell fired at one time
    public int shellCount;
    //whats the max angle the bullets fly out at
    public float spreadAngle;
    //spawn point location
    public Transform bulletSpawn;
    //how long in between bullets
    private float nextFireTime = 0f;
    //list of angles the bullets have
    List<Quaternion> bullets;

    //Ammo Variables
    private int CurrentAmmo;
    private bool IsReloading = false;

    public ParticleSystem muzzleFlash;
    private AudioSource fireSound;

    //awkae called before the game starts
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();
        CurrentAmmo = database.weapons[id].MaxAmmo;
        fireSound = GetComponent<AudioSource>();
        //list of bullet angles for the amount of shells in a shotgun
        bullets = new List<Quaternion>(shellCount);
        for (int i = 0; i < shellCount; i++)
        {
            //empty rotation added to stop potential errors
            bullets.Add(Quaternion.Euler(Vector3.zero));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!OpenPauseMenu.IsPaused())
        {
            //mouse 1 is right click
            //get key does while
            if (InputManager.FireWeapon() == true && Time.time >= nextFireTime && GetComponentInParent<Player>().IsPlayerControlled())
            {

                //the fire function
                Fire();
                //sets next fire time = to fire rate, making it fire at a rate of ever 0.2 seconds
                nextFireTime = Time.time + database.weapons[id].fireRate;
            }
        }

    }


    private void OnEnable()
    {
        IsReloading = false;
    }

    public void Fire()
    {
        if(gameObject != null && gameObject.activeInHierarchy) {
            if (CurrentAmmo > 0)
            {
                if(fireSound) {
                    fireSound.Play();
                }
                muzzleFlash.Play();
                for (int i = 0; i < bullets.Capacity; i++)
                {
                    //creates a randam rotation for each bullet quaternion
                    bullets[i] = Random.rotation;
                    //spawns a bullets in at the spawnpoint
                    GameObject o = Instantiate(
                        database.weapons[id].bulleType, 
                        bulletSpawn.position, 
                        bulletSpawn.rotation 
                       );
                    //shoots out the bullets forward at a angle max to spread angle
                    o.transform.rotation = Quaternion.RotateTowards(o.transform.rotation, bullets[i], spreadAngle);
                    //add the speed of the bullet to the rigid body
                    o.GetComponent<Rigidbody>().AddForce(o.transform.forward * database.weapons[id].bulletSpeed);
                    //next object in list   

                    CurrentAmmo--;
                }
            }
            //if they have no ammo, they must reload
            else
            {
                if (IsReloading == false)
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    IEnumerator Reload()
    {
        //makes it so it doesnt start hundreds of co routines for every frame
        IsReloading = true;
        //Debug.Log("reloading");
        //waits for the duration of the reload time before reloading
        yield return new WaitForSeconds(database.weapons[id].reloadTime);
        CurrentAmmo = database.weapons[id].MaxAmmo;

        //once reloaded set back false so can be called again
        IsReloading = false;

    }

    public bool GetIsReloading()
    {
        return IsReloading;
    }
}
