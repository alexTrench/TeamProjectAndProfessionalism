using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]

public class Playerstats : MonoBehaviour
{
    [SerializeField] private Button Reset     = null;
    [SerializeField] private Text   levelText = null;
    private int levelNumber;
    private int updatedLevelNumber;



    [SerializeField] private Text expText = null; // UI Text element for the EXP value
    private int expNumber;

    // local characters manager
    private CharacterManagerScript charactersManager;
    private GameObject gameController;
    private weaponDatabase database;

    // local zombie manager
    private ZombieManagerScript zombieManager;

    public Button loadButton;

    // -----------------------------MaxHp----------------------------- //

    public Button MaxHpLevelIncrease;       // + button for MaxHp
    public Button MaxHpLevelDecrease;       // - button for MaxHp

    public Text MaxHpLevelDisplay;              // UI text for CurrentMaxHpLevel ' 0 '/10
    public int MaxHpLevel { get; set; }

    public Text MaxHpStatsDisplay;     // UI text for MaxHP stats ??? <<<
    public int CurrentMaxHpStats { get; set; }


    // -----------------------------Ammunition Size----------------------------- //

    public Button MaxAmmoSizeIncrease;    // + button for Ammunitionsize
    public Button MaxAmmoSizeDecrease;    // - button for Ammunitionsize

    public Text MaxAmmoSizeLevelDisplay;              // UI text for Current Ammunition Size Level ' 0 '/10
    public int MaxAmmoSizeLevel { get; set; }

    public Text MaxAmmoSizeStatsDisplay;     // UI text for Max Ammunition Size Stats ??? <<<
    public int CurrentMaxAmmoSizeStats { get; set; }


    // ----------------------------------MaxEnergy-------------------------------------- //

    public Button MaxEnergyIncrease;    // + button for MaxEnergy
    public Button MaxEnergyDecrease;    // - button for MaxEnergy

    public Text MaxEnergyLevelDisplay;              // UI text for Current Max Energy Level ' 0 '/10
    public int MaxEnergyLevel { get; set; }

    public Text MaxEnergyStatsDisplay;     // UI text for Max Max Energy Level Stats ??? <<<
    public int MaxEnergyStats { get; set; }

    // ----------------------------------ReloadSpeed-------------------------------------- //

    public Button ReloadSpeedIncrease;    // + button for ReloadSpeed
    public Button ReloadSpeedDecrease;    // - button for ReloadSpeed

    public Text ReloadSpeedLevelDisplay;              // UI text for Current Reload Speed Level ' 0 '/10
    public int ReloadSpeedLevel { get; set; }

    public Text ReloadSpeedStatsDisplay;     // UI text for Max Energy Level Stats ??? <<<
    public float CurrentReloadSpeedStats { get; set; }

    // ----------------------------------MoveSpeed-------------------------------------- //

    public Button MoveSpeedIncrease;    // + button for Movespeed
    public Button MoveSpeedDecrease;    // - button for Movespeed

    public Text MoveSpeedLevelDisplay;              // UI text for Current Move Speed Level ' 0 '/10
    public int MoveSpeedLevel { get; set; }

    public Text MoveSpeedStatsDisplay;     // UI text for Move Speed Stats ??? <<<
    public int CurrentMoveSpeedStats { get; set; }

    // ----------------------------------Skill CDR-------------------------------------- //

    public Button SkillCDRIncrease;    // + button for Skill CDR
    public Button SkillCDRDecrease;    // - button for Skill CDR

    public Text SkillCDRLevelDisplay;              // UI text for Current Skill CDR Level ' 0 '/10
    public int SkillCDRLevel { get; set; }

    public Text SkillCDRStatsDisplay;     // UI text for Skill CDR Stats ??? <<<
    public float CurrentSkillCDRStats { get; set; }

    // ----------------------------------Fire rate-------------------------------------- //

    public Button FireRateIncrease;    // + button for FireRate
    public Button FireRateDecrease;    // - button for FireRate

    public Text FireRateLevelDisplay;              // UI text for Current Skill FireRate Level ' 0 '/10
    public int FireRateLevel { get; set; }

    public Text FireRateStatsDisplay;     // UI text for Skill CDR Stats ??? <<<
    public float CurrentFireRateStats { get; set; }


    // ----------------------------------Gun Damage Increase-------------------------------------- //

    public Button GunDamageIncrease;    // + button for Grenade Damage Area
    public Button GunDamageDecrease;    // - button for Grenade Damage Area

    public Text GunDamageLevelDisplay;              // UI text for Current skill Grenade Damage Area Level ' 0 '/10
    public int GunDamageLevel { get; set; }

    public Text GunDamageStatsDisplay;     // UI text for Skill Grenade Damage Area Stats ??? <<<
    public float CurrentGunDamageStats { get; set; }



    // ----------------------------------SpeedBoost Increase-------------------------------------- //

    public Button SpeedBoostIncrease;    // + button for SpeedBoost
    public Button SpeedBoostDecrease;    // - button for SpeedBoost

    public Text SpeedBoostLevelDisplay;              // UI text for Current skill SpeedBoost Level ' 0 '/10
    public int SpeedBoostLevel { get; set; }

    public Text SpeedBoostStatsDisplay;     // UI text for Skill SpeedBoost Stats ??? <<<
    public float CurrentSpeedBoostStats { get; set; }

    // ----------------------------------Health Regen Increase-------------------------------------- //

    public Button HealthRegenIncrease;    // + button for HealthRegen
    public Button HealthRegenDecrease;    // - button for HealthRegen

    public Text HealthRegenLevelDisplay;              // UI text for Current skill HealthRegen Level ' 0 '/10
    public int HealthRegenLevel { get; set; }

    public Text HealthRegenStatsDisplay;     // UI text for Skill HealthRegen Stats ??? <<<
    public float CurrentHealthRegenStats { get; set; }




    public Text MaxHp;
    public Text CurrentHp;
    public Text MaxExpText;
    public Text CurrentExpText;
    public Text LevelText;
    public Text CurrentGeneralSkillPoint;
    public Text CurrentCharacterSkillPoint;

    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxExp { get; set; }
    public int CurrentExp { get; set; }

    public int Level { get; set; }
    public int skillCD { get; set; }

    public  int Skillpoint { get; set; }
    private int CurrentSkillpoint;

    public Slider healthbar;
    public Slider expbar;


    // Start is called before the first frame update
    void Start()
    {
        // initialise the local value
        expNumber = int.Parse(expText.text);

        loadButton.onClick.AddListener(updateEverything);

        levelNumber = int.Parse(levelText.text);

        // look up on the list of objects and get CharacterManagerScript component for the object tagged as CharacterManager
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
      
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();

        Skillpoint = 0;
        CurrentSkillpoint = 0;
        Level = 1;
        skillCD = 60;

        // -----------------------------MaxHp------------------------------//
        CurrentMaxHpStats = 0;
        MaxHpLevel = 0;

        // -----------------------------Ammunition Size--------------------//
        MaxAmmoSizeLevel = 0;
        CurrentMaxAmmoSizeStats = 0;

        // -----------------------------MaxEnergy--------------------------//
        MaxEnergyLevel = 0;
        MaxEnergyStats = 0;

        // -----------------------------ReloadSpeed------------------------//
        ReloadSpeedLevel = 0;
        CurrentReloadSpeedStats = 0;

        // -----------------------------MoveSpeed--------------------------//
        MoveSpeedLevel = 0;
        CurrentMoveSpeedStats = 0;

        // -----------------------------Skill CDR--------------------------//
        SkillCDRLevel = 0;
        CurrentSkillCDRStats = 0;

        // ----------------------------------Fire rate-------------------------------------- //
        FireRateLevel = 0;
        CurrentFireRateStats = 0;

        // ----------------------------------Gun Damage Increase-------------------------------------- //  
        GunDamageLevel = 0;
        CurrentGunDamageStats = 0;

        // ----------------------------------SpeedBoost Increase-------------------------------------- //
        SpeedBoostLevel = 0;
        CurrentSpeedBoostStats = 0;

        // ----------------------------------Health Regen Increase-------------------------------------- //

        HealthRegenLevel = 0;
        CurrentHealthRegenStats = 0;




}




// Update is called once per frame
void Update()
    {

        updatedLevelNumber = int.Parse(levelText.text);
        CurrentSkillpoint = updatedLevelNumber * 2 - 2 + Skillpoint;
        if (levelNumber != updatedLevelNumber)
        {

            levelNumber = updatedLevelNumber;
            charactersManager.SetPlayerHealth(charactersManager.GetCurrentPlayer().GetMaxHealth());
            charactersManager.SetPlayerEnergy(charactersManager.GetMaxEnergy());
            Debug.Log("current level " + levelNumber + " updated level " + updatedLevelNumber + " skill points " + Skillpoint);
            //to-do for the rest
        }
        

        CurrentGeneralSkillPoint.text = CurrentSkillpoint.ToString();
        CurrentCharacterSkillPoint.text = CurrentSkillpoint.ToString();

        // -----------------------------MaxHp----------------------------- //
        MaxHpLevelDisplay.text = MaxHpLevel.ToString() + "/30";
        MaxHpStatsDisplay.text = "+" + CurrentMaxHpStats.ToString();

        // -----------------------------Ammunition Size----------------------------- //
        MaxAmmoSizeLevelDisplay.text = MaxAmmoSizeLevel.ToString() + "/10";
        MaxAmmoSizeStatsDisplay.text = "+" + CurrentMaxAmmoSizeStats.ToString();

        // ----------------------------------MaxEnergy-------------------------------------- //
        MaxEnergyLevelDisplay.text = MaxEnergyLevel.ToString() + "/20";
        MaxEnergyStatsDisplay.text = "+" + MaxEnergyStats.ToString();

        // ----------------------------------ReloadSpeed-------------------------------------- // 
        ReloadSpeedLevelDisplay.text = ReloadSpeedLevel.ToString() + "/10";
        ReloadSpeedStatsDisplay.text = "-" + CurrentReloadSpeedStats.ToString() + "%";

        // ----------------------------------MoveSpeed-------------------------------------- //  
        MoveSpeedLevelDisplay.text = MoveSpeedLevel.ToString() + "/10";
        MoveSpeedStatsDisplay.text = "+" + CurrentMoveSpeedStats.ToString() + "%";

        // ----------------------------------Skill CDR-------------------------------------- //
        SkillCDRLevelDisplay.text = SkillCDRLevel.ToString() + "/10";
        SkillCDRStatsDisplay.text = "-" + CurrentSkillCDRStats.ToString() + "s";

        // ----------------------------------Fire rate-------------------------------------- //  
        FireRateLevelDisplay.text = FireRateLevel.ToString() + "/10";
        FireRateStatsDisplay.text = "+" + CurrentFireRateStats.ToString()+ "%";

        // ----------------------------------Gun Damage Increase-------------------------------------- // 
        GunDamageLevelDisplay.text = GunDamageLevel.ToString() + "/10";
        GunDamageStatsDisplay.text = "+" + CurrentGunDamageStats.ToString() + "%";


        // ----------------------------------SpeedBoost Increase-------------------------------------- //
        SpeedBoostLevelDisplay.text = SpeedBoostLevel.ToString() + "/10";
        SpeedBoostStatsDisplay.text = "+" + CurrentSpeedBoostStats.ToString() + "%";


        // ----------------------------------Health Regen Increase-------------------------------------- //

        HealthRegenLevelDisplay.text = HealthRegenLevel.ToString() + "/10";
        HealthRegenStatsDisplay.text = "+" + CurrentHealthRegenStats.ToString();
        }


// -----------------------------MaxHp----------------------------- //
public void IncreaseMaxHpLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (MaxHpLevel < 30)
            {
                charactersManager.IncrementPlayerMaxHealth(10);
                MaxHpLevel += amount;
                CurrentMaxHpStats += 10;
                Skillpoint -= 1;
            }
        }
    }

    public void DecreaseMaxHpLevel(int amount)
    {
            if (MaxHpLevel > 0)
            {
                charactersManager.DecrementPlayerMaxHealth(10);
                MaxHpLevel -= amount;
                CurrentMaxHpStats -= 10;
                Skillpoint += 1;
            }
    }
    // -----------------------------MaxHp----------------------------- //


    // -----------------------------Ammunition Size----------------------------- //
    public void IncreaseMaxAmmoSizeLevel(int amount)
    {
        if (CurrentSkillpoint >= 2)
        {
            if (MaxAmmoSizeLevel < 10)
            {
                MaxAmmoSizeLevel += amount;
                CurrentMaxAmmoSizeStats += 1;
                Skillpoint -= 2;
                for (int i = 0; i < database.weapons.Capacity; i++)
                {
                    database.weapons[i].MaxAmmo += 1;
                }
               
            }
        }
    }

    public void DecreaseMaxAmmoSizeLevel(int amount)
    {
            if (MaxAmmoSizeLevel > 0)
            {
                MaxAmmoSizeLevel -= amount;
                CurrentMaxAmmoSizeStats -= 1;
                Skillpoint += 2;
            for (int i = 0; i < database.weapons.Capacity; i++)
            {
                database.weapons[i].MaxAmmo -= 1;
            }
        }
    }
    // -----------------------------Ammunition Size----------------------------- //


    // ----------------------------------MaxEnergy-------------------------------------- //
    public void IncreaseMaxEnergyLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (MaxEnergyLevel < 20)
            {
                charactersManager.IncrementPlayerMaxEnergy(10);
                MaxEnergyLevel += amount;
                MaxEnergyStats += 10;
                Skillpoint -= 1;
                
           
            }
        }
    }
    public void DecreaseMaxEnergyLevel(int amount)
    {
            if (MaxEnergyLevel > 0)
            {
                charactersManager.DecrementPlayerMaxEnergy(10);
                MaxEnergyLevel -= amount;
                MaxEnergyStats -= 10;
                Skillpoint += 1;
            }
    }
    // ----------------------------------MaxEnergy-------------------------------------- //

    // ----------------------------------ReloadSpeed-------------------------------------- //
    public void IncreaseReloadSpeedLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (ReloadSpeedLevel < 10)
            {
                ReloadSpeedLevel += amount;
                CurrentReloadSpeedStats += 1;
                Skillpoint -= 1;
               
           for (int i = 0; i < database.weapons.Capacity; i++)
           {
               database.weapons[i].reloadTime += 0.01f;
           }
           
            }
        }
    }

    public void DecreaseReloadSpeedLevel(int amount)
    {
            if (ReloadSpeedLevel > 0)
            {
                ReloadSpeedLevel -= amount;
                CurrentReloadSpeedStats -= 1;
                Skillpoint += 1;
            
             for (int i = 0; i < database.weapons.Capacity; i++)
             {
                 database.weapons[i].reloadTime -= 0.01f;
             }
            
        }
    }
    // ----------------------------------ReloadSpeed-------------------------------------- //


    // ----------------------------------MoveSpeed-------------------------------------- //
    public void IncreaseMoveSpeedLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (MoveSpeedLevel < 10)
            {
                charactersManager.IncrementPlayerMovementSpeed(0.2f);
                MoveSpeedLevel += amount;
                CurrentMoveSpeedStats += 2;
                Skillpoint -= 1;
            }
        }
    }

    public void DecreaseMoveSpeedLevel(int amount)
    {
            if (MoveSpeedLevel > 0)
            {
                charactersManager.DecrementPlayerMovementSpeed(0.2f);
                MoveSpeedLevel -= amount;
                CurrentMoveSpeedStats -= 2;
                Skillpoint += 1;
            }
    }
    // ----------------------------------MoveSpeed-------------------------------------- //


    // ----------------------------------Skill CDR-------------------------------------- //
    public void IncreaseSkillCDRLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (SkillCDRLevel < 10)
            {
                SkillCDRLevel += amount;
                CurrentSkillCDRStats += 1;
                skillCD -= 1;
                Skillpoint -= 1;
            }
        }
    }
    public void DecreaseSkillCDRLevel(int amount)
    {
            if (SkillCDRLevel > 0)
            {
                SkillCDRLevel -= amount;
                CurrentSkillCDRStats -= 1;
                skillCD += 1;
                Skillpoint += 1;
            }
    }
    // ----------------------------------Skill CDR-------------------------------------- //


    // ----------------------------------Fire rate-------------------------------------- //
    public void IncreaseFireRateLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (FireRateLevel < 10)
            {
                FireRateLevel += amount;
                CurrentFireRateStats += 1;
                Skillpoint -= 1;
                
                for (int i = 0; i < database.weapons.Capacity; i++)
                {
                    database.weapons[i].reloadTime += 0.01f;
                }
                
            }
        }
    }
    public void DecreaseFireRateLevel(int amount)
    {
            if (FireRateLevel > 0)
            {
                FireRateLevel -= amount;
                CurrentFireRateStats -= 1;
                Skillpoint += 1;
            
             for (int i = 0; i < database.weapons.Capacity; i++)
             {
                 database.weapons[i].reloadTime -= 0.01f;
             }
             
        }
    }
    // ----------------------------------Fire rate-------------------------------------- //

   


    // ----------------------------------Gun Damage Increase-------------------------------------- //
    public void IncreaseGunDamageLevel(int amount)
    {
        if (CurrentSkillpoint >= 3)
        {
            if (GunDamageLevel < 10)
            {
                GunDamageLevel += amount;
                CurrentGunDamageStats += 2;
                Skillpoint -= 3;
               
                for (int i = 0; i < database.weapons.Capacity; i++)
                {
                    database.weapons[i].damage += 0.1f;
                }
               
            }
        }
    }
    public void DecreaseGunDamageLevel(int amount)
    {
            if (GunDamageLevel > 0)
            {
                GunDamageLevel -= amount;
                CurrentGunDamageStats -= 2;
                Skillpoint += 3;
            for (int i = 0; i < database.weapons.Capacity; i++)
            {
                database.weapons[i].damage -= 0.1f;
            }
        }
    }
    // ----------------------------------Gun Damage Increase-------------------------------------- //


    // ----------------------------------SpeedBoost Increase-------------------------------------- //
    public void IncreaseSpeedBoostLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (SpeedBoostLevel < 10)
            {
                SpeedBoostLevel += amount;
                CurrentSpeedBoostStats += 10;
                Skillpoint -= 1;
            }
        }
    }
    public void DecreaseSpeedBoostLevel(int amount)
    {
            if (SpeedBoostLevel > 0)
            {
                SpeedBoostLevel -= amount;
                CurrentSpeedBoostStats -= 10;
                Skillpoint += 1;
            }
    }

    // ----------------------------------Health Regen Increase-------------------------------------- //

    public void IncreaseHealthRegenLevel(int amount)
    {
        if (CurrentSkillpoint > 0)
        {
            if (HealthRegenLevel < 10)
            {
                HealthRegenLevel += amount;
                CurrentHealthRegenStats += 10;
                Skillpoint -= 1;
            }
        }
    }
    public void DecreaseHealthRegenLevel(int amount)
    {
            if (HealthRegenLevel > 0)
            {
                HealthRegenLevel -= amount;
                CurrentHealthRegenStats -= 10;
                Skillpoint += 1;
            }
    }

    public void ResetAllskill()
    {
        CurrentSkillpoint = levelNumber * 2 - 2;

        // -----------------------------MaxHp------------------------------//
        charactersManager.DecrementPlayerMaxHealth(CurrentMaxHpStats);
        CurrentMaxHpStats = 0;
        MaxHpLevel = 0;

        // -----------------------------Ammunition Size--------------------//
        for (int i = 0; i < database.weapons.Capacity; i++)
        {
            database.weapons[i].MaxAmmo -= 1* MaxAmmoSizeLevel;
        }
        MaxAmmoSizeLevel = 0;
        CurrentMaxAmmoSizeStats = 0;

        // -----------------------------MaxEnergy--------------------------//
        charactersManager.DecrementPlayerMaxEnergy(MaxEnergyStats);
        MaxEnergyLevel = 0;
        MaxEnergyStats = 0;

        // -----------------------------ReloadSpeed------------------------//
        for (int i = 0; i < database.weapons.Capacity; i++)
        {
            database.weapons[i].reloadTime -= 0.01f * ReloadSpeedLevel;
        }
        ReloadSpeedLevel = 0;
        CurrentReloadSpeedStats = 0;
       

        // -----------------------------MoveSpeed--------------------------//
        charactersManager.DecrementPlayerMovementSpeed(0.2f* MoveSpeedLevel);
        MoveSpeedLevel = 0;
        CurrentMoveSpeedStats = 0;

        // -----------------------------Skill CDR--------------------------//
        skillCD += 1 * SkillCDRLevel;
        SkillCDRLevel = 0;
        CurrentSkillCDRStats = 0;

        // ----------------------------------Fire rate-------------------------------------- //
        for (int i = 0; i < database.weapons.Capacity; i++)
        {
            database.weapons[i].fireRate -= 0.01f * FireRateLevel;
        }
        FireRateLevel = 0;
        CurrentFireRateStats = 0;

        // ----------------------------------Gun Damage Increase-------------------------------------- //  
        for (int i = 0; i < database.weapons.Capacity; i++)
        {
            database.weapons[i].damage -= 0.1f * GunDamageLevel;
        }
        GunDamageLevel = 0;
        CurrentGunDamageStats = 0;

        // ----------------------------------SpeedBoost Increase-------------------------------------- //
        SpeedBoostLevel = 0;
        CurrentSpeedBoostStats = 0;

        // ----------------------------------Health Regen Increase-------------------------------------- //

        HealthRegenLevel = 0;
        CurrentHealthRegenStats = 0;
    }

    public void Addexp()
    {
        expNumber = int.Parse(expText.text);
        expNumber = expNumber + 10;
        expText.text = expNumber.ToString();
    }
   

    public void updateEverything()
    {
        Debug.Log(CurrentMaxHpStats + " " + MaxEnergyStats);
        charactersManager.SetPlayerMaxHealth(100 + CurrentMaxHpStats);
      
        charactersManager.SetPlayerHealth(100 + CurrentMaxHpStats);
        charactersManager.SetMaxEnergy(100 + MaxEnergyStats);
      
        //to-do for the rest
        charactersManager.SetPlayerMovementSpeed(12+ 0.2f * MoveSpeedLevel);
    }
}




