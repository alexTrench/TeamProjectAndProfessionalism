using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo
{
    public int itemID;
    public string name;
    public GameObject weaponObject;
    public GameObject bulleType;
    public float damage;
    public float fireRate;
    public int MaxAmmo;
    public float bulletSpeed;
    public float reloadTime;
    public bool isPurchased;
}
