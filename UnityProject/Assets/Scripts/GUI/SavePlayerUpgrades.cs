using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SavePlayerUpgrades : MonoBehaviour
{
    private Playerstats stats;

    [SerializeField] private GameObject canvas = null;

    private Dictionary<string, float> mapFLOAT;
    private Dictionary<string, int>   mapINT;

    public void Start()
    {
        stats = canvas.GetComponent<Playerstats>();
        //PlayerPrefs.DeleteAll();
    }

    public void SaveUpgrades()
    {
        mapINT   = new Dictionary<string, int>();
        mapFLOAT = new Dictionary<string, float>();

        mapINT.Add("maxHpLevel",                      stats.MaxHpLevel);
        mapINT.Add("currentMaxHpStats",               stats.CurrentMaxHpStats);
        mapINT.Add("maxAmmoSizeLevel",                stats.MaxAmmoSizeLevel);
        mapINT.Add("currentMaxAmmoSizeStats",         stats.CurrentMaxAmmoSizeStats);
        mapINT.Add("maxEnergyLevel",                  stats.MaxEnergyLevel);
        mapINT.Add("maxEnergyStats",                  stats.MaxEnergyStats);
        mapINT.Add("reloadSpeedLevel",                stats.ReloadSpeedLevel);
        mapFLOAT.Add("currentReloadSpeedStats",       stats.CurrentReloadSpeedStats);
        mapINT.Add("moveSpeedLevel",                  stats.MoveSpeedLevel);
        mapINT.Add("currentMoveSpeedStats",           stats.CurrentMoveSpeedStats);
        mapINT.Add("skillCDRLevel",                   stats.SkillCDRLevel);
        mapFLOAT.Add("currentSkillCDRStats",          stats.CurrentSkillCDRStats);
        mapINT.Add("fireRateLevel",                   stats.FireRateLevel);
        mapFLOAT.Add("currentFireRateStats",          stats.CurrentFireRateStats);
        mapINT.Add("grenadeDamageAreaLevel",          stats.GrenadeDamageAreaLevel);
        mapFLOAT.Add("currentGrenadeDamageAreaStats", stats.CurrentGrenadeDamageAreaStats);
        mapINT.Add("gunDamageLevel",                  stats.GunDamageLevel);
        mapFLOAT.Add("currentGunDamageStats",         stats.CurrentGunDamageStats);
        mapINT.Add("grenadeDamageLevel",              stats.GrenadeDamageLevel);
        mapFLOAT.Add("currentGrenadeDamageStats",     stats.CurrentGrenadeDamageAreaStats);
        mapINT.Add("maxHealth",                       stats.MaxHealth);
        mapINT.Add("currentHealth",                   stats.CurrentHealth);
        mapINT.Add("MaxExp",                          stats.MaxExp);
        mapINT.Add("currentExp",                      stats.CurrentExp);
        mapINT.Add("level",                           stats.Level);
        mapINT.Add("skillPoints",                     stats.Skillpoint);

        foreach (KeyValuePair<string, int> item in mapINT)
        {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }

        foreach (KeyValuePair<string, float> item in mapFLOAT)
        {
            PlayerPrefs.SetFloat(item.Key, item.Value);
        }

        PlayerPrefs.Save();
    }

    public void LoadUpgrades()
    {
        stats.MaxHpLevel                    = PlayerPrefs.GetInt("maxHpLevel");
        stats.CurrentMaxHpStats             = PlayerPrefs.GetInt("currentMaxHpStats");
        stats.MaxAmmoSizeLevel              = PlayerPrefs.GetInt("maxAmmoSizeLevel");
        stats.CurrentMaxAmmoSizeStats       = PlayerPrefs.GetInt("currentMaxAmmoSizeStats");
        stats.MaxEnergyLevel                = PlayerPrefs.GetInt("maxEnergyLevel");
        stats.MaxEnergyStats                = PlayerPrefs.GetInt("maxEnergyStats");
        stats.ReloadSpeedLevel              = PlayerPrefs.GetInt("reloadSpeedLevel");
        stats.CurrentReloadSpeedStats       = PlayerPrefs.GetFloat("currentReloadSpeedStats");
        stats.MoveSpeedLevel                = PlayerPrefs.GetInt("moveSpeedLevel");
        stats.CurrentMoveSpeedStats         = PlayerPrefs.GetInt("currentMoveSpeedStats");
        stats.SkillCDRLevel                 = PlayerPrefs.GetInt("skillCDRLevel");
        stats.CurrentSkillCDRStats          = PlayerPrefs.GetFloat("currentSkillCDRStats");
        stats.FireRateLevel                 = PlayerPrefs.GetInt("fireRateLevel");
        stats.CurrentFireRateStats          = PlayerPrefs.GetFloat("currentFireRateStats");
        stats.GrenadeDamageAreaLevel        = PlayerPrefs.GetInt("grenadeDamageAreaLevel");
        stats.CurrentGrenadeDamageAreaStats = PlayerPrefs.GetFloat("currentGrenadeDamageAreaStats");
        stats.GunDamageLevel                = PlayerPrefs.GetInt("gunDamageLevel");
        stats.CurrentGunDamageStats         = PlayerPrefs.GetFloat("currentGunDamageStats");
        stats.GrenadeDamageLevel            = PlayerPrefs.GetInt("grenadeDamageLevel");
        stats.CurrentGrenadeDamageAreaStats = PlayerPrefs.GetFloat("currentGrenadeDamageStats");
        stats.MaxHealth                     = PlayerPrefs.GetInt("maxHealth");
        stats.CurrentHealth                 = PlayerPrefs.GetInt("currentHealth");
        stats.MaxExp                        = PlayerPrefs.GetInt("MaxExp");
        stats.CurrentExp                    = PlayerPrefs.GetInt("currentExp");
        stats.Level                         = PlayerPrefs.GetInt("level");
        stats.Skillpoint                    = PlayerPrefs.GetInt("skillPoints");
    }
}