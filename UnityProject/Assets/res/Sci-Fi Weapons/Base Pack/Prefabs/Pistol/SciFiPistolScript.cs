﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiPistolScript : MonoBehaviour
{
    //used to set the id of the item in database so we can get its weapon stats
    int id = 5;
    public GameObject gameController;
    weaponDatabase database;
    //gets where the bullet is supposed to spawn
    public Transform bulletSpawn;
    //how long in between bullets
    private float nextFireTime = 0f;
    //Ammo Variables
    private int CurrentAmmo;
    //is reloding
    private bool IsReloading = false;
    //muzzle flash
    public ParticleSystem muzzleFlash;

    private AudioSource fireSound;




    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();
        CurrentAmmo = database.weapons[id].MaxAmmo;
        fireSound = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!OpenPauseMenu.IsPaused())
        {
            //if the right mouse button is pressed
            if (InputManager.FireWeapon() && Time.time >= nextFireTime && GetComponentInParent<Player>().IsPlayerControlled() && !GetComponentInParent<Player>().IsDead())
            {

                if (CurrentAmmo > 0)
                {
                    Fire();
                    //sets next fire time = to fire rate, making it fire at a rate of ever 0.2 seconds
                    nextFireTime = Time.time + database.weapons[id].fireRate;
                }
                else
                {
                    if (IsReloading == false)
                    {
                        StartCoroutine(Reload());
                    }
                }

            }
        }
    }

    private void OnEnable()
    {
        //stops reloading time when you switch from the weapon
        IsReloading = false;
    }

    public void Fire()
    {


        if (CurrentAmmo > 0)
        {
            muzzleFlash.Play();
            AudioSource.PlayClipAtPoint(fireSound.clip, transform.position);


            //creates a clone of the bullet
            GameObject bullet = Instantiate(database.weapons[id].bulleType);

            //spawns at the bullet spawn point
            bullet.transform.position = bulletSpawn.position;
            //transforms the roatation into angles, into 360 degrees
            Vector3 rotation = bullet.transform.rotation.eulerAngles;
            bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

            //adds the speed to the rigid body, creating movement
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * database.weapons[id].bulletSpeed, ForceMode.Impulse);
            CurrentAmmo--;
        }
        else
        {
            if (IsReloading == false)
            {
                StartCoroutine(Reload());
            }
        }




        //raycasting
        //RaycastHit hit;
        //if we hit something with the ray
        //if (Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, 13))
        //{
    //    Debug.Log(hit.transform.name);

    //    //does damage to enemys with a health script attach
    //    Zombie target = hit.transform.GetComponent<Zombie>();
    //    //other.GetComponent<Zombie>().GetComponentInParent<BaseCharacter>().TakeDamage((int)damage);

    //    if (target != null)
    //    {

    //        target.TakeDamage((int)database.weapons[id].damage);
    //        Debug.Log(target.m_health);
    //    }
   

    }

    IEnumerator Reload()
    {
        //makes it so it doesnt start hundreds of co routines for every frame
        IsReloading = true;
        Debug.Log("reloading");
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
