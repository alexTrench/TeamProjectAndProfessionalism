using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerstats : MonoBehaviour
{
    public Button Reset;

    // -----------------------------MaxHp----------------------------- //

    public Button MaxHpLevelIncrease;       // + button for MaxHp
    public Button MaxHpLevelDecrease;       // - button for MaxHp

    public Text CurrentMaxHpLevel;              // UI text for CurrentMaxHpLevel ' 0 '/10
    public int MaxHpLevel { get; set; }

    public Text CurrentMaxHpIncreaseStats;     // UI text for MaxHP stats ??? <<<
    public int CurrentMaxHpStats { get; set; }


    // -----------------------------Ammunition Size----------------------------- //

    public Button MaxAmmunitionSizeIncrease;    // + button for Ammunitionsize
    public Button MaxAmmunitionSizeDecrease;    // - button for Ammunitionsize

    public Text CurrentMaxAmmunitionSizeLevel;              // UI text for Current Ammunition Size Level ' 0 '/10
    public int MaxAmmunitionSizeLevel { get; set; }

    public Text CurrentMaxAmmunitionSizeStats;     // UI text for Max Ammunition Size Stats ??? <<<
    public int CurrentMaxAmmunitionStats { get; set; }


    // ----------------------------------MaxEnergy-------------------------------------- //

    public Button MaxEnergyIncrease;    // + button for MaxEnergy
    public Button MaxEnergyDecrease;    // - button for MaxEnergy

    public Text CurrentMaxEnergyLevel;              // UI text for Current Max Energy Level ' 0 '/10
    public int MaxEnergyLevel { get; set; }

    public Text CurrentMaxEnergyLevelStats;     // UI text for Max Max Energy Level Stats ??? <<<
    public int MaxEnergyLevelStats { get; set; }

    // ----------------------------------ReloadSpeed-------------------------------------- //

    public Button ReloadSpeedIncrease;    // + button for ReloadSpeed
    public Button ReloadSpeedDecrease;    // - button for ReloadSpeed

    public Text CurrentReloadSpeedLevel;              // UI text for Current Reload Speed Level ' 0 '/10
    public int MaxReloadSpeedLevel { get; set; }

    public Text CurrentReloadSpeedStats;     // UI text for Max Energy Level Stats ??? <<<
    public float CurrentReloadSpeedLevelStats { get; set; }

    // ----------------------------------MoveSpeed-------------------------------------- //

    public Button MoveSpeedIncrease;    // + button for Movespeed
    public Button MoveSpeedDecrease;    // - button for Movespeed

    public Text CurrentMoveSpeedLevel;              // UI text for Current Move Speed Level ' 0 '/10
    public int MaxMoveSpeedLevel { get; set; }

    public Text CurrentMoveSpeedStats;     // UI text for Move Speed Stats ??? <<<
    public int CurrentMoveSpeedLevelStats { get; set; }

    // ----------------------------------Skill CDR-------------------------------------- //

    public Button SkillCDRIncrease;    // + button for Skill CDR
    public Button SkillCDRDecrease;    // - button for Skill CDR

    public Text CurrentSkillCDRLevel;              // UI text for Current Skill CDR Level ' 0 '/10
    public int MaxSkillCDRLevel { get; set; }

    public Text CurrentSkillCDRStats;     // UI text for Skill CDR Stats ??? <<<
    public float CurrentSkillCDRLevelStats { get; set; }







    public Text MaxHp;
    public Text CurrentHp;
    public Text MaxExpText;
    public Text CurrentExpText;
    public Text LevelText;
    public Text CurrentSkillPoint;

    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxExp { get; set; }
    public int CurrentExp { get; set; }
  
    public int Level { get; set; }

    public int Skillpoint { get; set; }

    public Slider healthbar;
    public Slider expbar;


    // Start is called before the first frame update
    void Start()
    {
        //Button MaxHpLevelIncrease = GetComponent<Button>();
       // Button MaxHpLevelDecrease = GetComponent<Button>();

        
        Skillpoint = 100;
        Level = 1;

        // -----------------------------MaxHp------------------------------//
        CurrentMaxHpStats = 0;
        MaxHpLevel = 0;

        // -----------------------------Ammunition Size--------------------//
        MaxAmmunitionSizeLevel = 0;
        CurrentMaxAmmunitionStats = 0;

        // -----------------------------MaxEnergy--------------------------//
        MaxEnergyLevel = 0;
        MaxEnergyLevelStats = 0;

        // -----------------------------ReloadSpeed------------------------//
        MaxReloadSpeedLevel = 0;
        CurrentReloadSpeedLevelStats = 0;

        // -----------------------------MoveSpeed--------------------------//
        MaxMoveSpeedLevel = 0;
        CurrentMoveSpeedLevelStats = 0;

        // -----------------------------Skill CDR--------------------------//
        MaxSkillCDRLevel = 0;
        CurrentSkillCDRLevelStats = 0;



        // MaxHealth = 100;
        //  CurrentHealth = MaxHealth;
        // healthbar.value = CalculateHealth();


        //   MaxExp = 100;
        //   CurrentExp = 0;
        //   expbar.value = CalculateExp();


    }




    // Update is called once per frame
    void Update()
    {
        // -----------------------------MaxHp----------------------------- //
        CurrentMaxHpLevel.text = MaxHpLevel.ToString() + "/10";
        CurrentMaxHpIncreaseStats.text = "+" + CurrentMaxHpStats.ToString();

        // -----------------------------Ammunition Size----------------------------- //
        CurrentMaxAmmunitionSizeLevel.text = MaxAmmunitionSizeLevel.ToString()+"/10";
        CurrentMaxAmmunitionSizeStats.text = "+" + CurrentMaxAmmunitionStats.ToString();

        // ----------------------------------MaxEnergy-------------------------------------- //
        CurrentMaxEnergyLevel.text = MaxEnergyLevel.ToString() + "/10";
        CurrentMaxEnergyLevelStats.text = "+" + MaxEnergyLevelStats.ToString();

        // ----------------------------------ReloadSpeed-------------------------------------- //
        CurrentReloadSpeedLevel.text = MaxReloadSpeedLevel.ToString() + "/10";
        CurrentReloadSpeedStats.text = "-" + CurrentReloadSpeedLevelStats.ToString()+ "s";

        // ----------------------------------MoveSpeed-------------------------------------- //
        CurrentMoveSpeedLevel.text = MaxMoveSpeedLevel.ToString() + "/10";
        CurrentMoveSpeedStats.text = "+" + CurrentMoveSpeedLevelStats.ToString();

        // ----------------------------------Skill CDR-------------------------------------- //
        CurrentSkillCDRLevel.text = MaxSkillCDRLevel.ToString() + "/10";
        CurrentSkillCDRStats.text = "-" + CurrentSkillCDRLevelStats.ToString() + "s";

        CurrentSkillPoint.text = Skillpoint.ToString();

       

        //  MaxHpLevelIncrease.onClick.AddListener(() => { IncreaseMaxHpLevel(1); });
        // MaxHpLevelDecrease.onClick.AddListener(() => { DecreaseMaxHpLevel(1); });
        /*
               LevelText.text = Level.ToString();

              MaxHp.text = MaxHealth.ToString();
              CurrentHp.text = CurrentHealth.ToString();

              MaxExpText.text = MaxExp.ToString();
              CurrentExpText.text = CurrentExp.ToString();

              if (CurrentHealth != 0)
               {
                   if (Input.GetKeyDown(KeyCode.A))
                   {
                       TakeDamage(10);
                   }
               }
               if (Input.GetKeyDown(KeyCode.B))
               {
                   addmaxhp(10);
               }
               if (Input.GetKeyDown(KeyCode.C))
                   Addexp(10);

         */


    }
    /*
   void IncreaseMaxHpLevelonClick()
   {
       IncreaseMaxHpLevel(1);
   }
   void DecreaseMaxHpLevelonClick()
   {
       DecreaseMaxHpLevel(1);
   }
 */

    public void IncreaseMaxHpLevel(int amount)
    {
        if (MaxHpLevel < 10)
        {
            MaxHpLevel += amount;
            CurrentMaxHpStats += 10;
            Skillpoint -= 1;

        }
    }

    public void DecreaseMaxHpLevel(int amount)
   {
       if (MaxHpLevel > 0)
       {
           MaxHpLevel -= amount;
           CurrentMaxHpStats -= 10;
            Skillpoint += 1;
        }
   }

    public void IncreaseMaxAmmunitionSizeLevel(int amount)
    {
        if (MaxAmmunitionSizeLevel < 10)
        {
            MaxAmmunitionSizeLevel += amount;
            CurrentMaxAmmunitionStats += 10;
            Skillpoint -= 1;

        }
    }

    public void DecreaseMaxAmmunitionSizeLevel(int amount)
    {
        if (MaxAmmunitionSizeLevel > 0)
        {
            MaxAmmunitionSizeLevel -= amount;
            CurrentMaxAmmunitionStats -= 10;
            Skillpoint += 1;
        }
    }

    public void IncreaseMaxEnergyLevel(int amount)
    {
        if (MaxEnergyLevel < 10)
        {
            MaxEnergyLevel += amount;
            MaxEnergyLevelStats += 10;
            Skillpoint -= 1;

        }
    }

    public void DecreaseMaxEnergyLevel(int amount)
    {
        if (MaxEnergyLevel > 0)
        {
            MaxEnergyLevel -= amount;
            MaxEnergyLevelStats -= 10;
            Skillpoint += 1;
        }
    }

    public void IncreaseReloadSpeedLevel(int amount)
    {
        if (MaxReloadSpeedLevel < 10)
        {
            MaxReloadSpeedLevel += amount;
            CurrentReloadSpeedLevelStats +=0.1f;
            Skillpoint -= 1;

        }
    }

    public void DecreaseReloadSpeedLevel(int amount)
    {
        if (MaxReloadSpeedLevel > 0)
        {
            MaxReloadSpeedLevel -= amount;
            CurrentReloadSpeedLevelStats -= 0.1f;
            Skillpoint += 1;
        }
    }

    public void IncreaseMoveSpeedLevel(int amount)
    {
        if (MaxMoveSpeedLevel < 10)
        {
            MaxMoveSpeedLevel += amount;
            CurrentMoveSpeedLevelStats += 10;
            Skillpoint -= 1;

        }
    }

    public void DecreaseMoveSpeedLevel(int amount)
    {
        if (MaxMoveSpeedLevel > 0)
        {
            MaxMoveSpeedLevel -= amount;
            CurrentMoveSpeedLevelStats -= 10;
            Skillpoint += 1;
        }
    }


    public void IncreaseSkillCDRLevel(int amount)
    {
        if (MaxSkillCDRLevel < 10)
        {
            MaxSkillCDRLevel += amount;
            CurrentSkillCDRLevelStats += 0.1f;
            Skillpoint -= 1;

        }
    }

    public void DecreaseSkillCDRLevel(int amount)
    {
        if (MaxSkillCDRLevel > 0)
        {
            MaxSkillCDRLevel -= amount;
            CurrentSkillCDRLevelStats -= 0.1f;
            Skillpoint += 1;
        }
    }

    public void ResetAllskill()
    {
        Skillpoint = 100;
     
        // -----------------------------MaxHp------------------------------//
        CurrentMaxHpStats = 0;
        MaxHpLevel = 0;

        // -----------------------------Ammunition Size--------------------//
        MaxAmmunitionSizeLevel = 0;
        CurrentMaxAmmunitionStats = 0;

        // -----------------------------MaxEnergy--------------------------//
        MaxEnergyLevel = 0;
        MaxEnergyLevelStats = 0;

        // -----------------------------ReloadSpeed------------------------//
        MaxReloadSpeedLevel = 0;
        CurrentReloadSpeedLevelStats = 0;

        // -----------------------------MoveSpeed--------------------------//
        MaxMoveSpeedLevel = 0;
        CurrentMoveSpeedLevelStats = 0;

        // -----------------------------Skill CDR--------------------------//
        MaxSkillCDRLevel = 0;
        CurrentSkillCDRLevelStats = 0;
    }


    /*
                void addmaxhp(int amount)
                {
                    MaxHealth += amount;
                }

                void TakeDamage(int amount)
                {
                    CurrentHealth -= amount;

                    healthbar.value = CalculateHealth();
                    Debug.Log(CurrentHealth);
                    if (CurrentHealth <= 0)
                        Die();
                }

                float CalculateHealth()
                {
                    return CurrentHealth / MaxHealth;
                }


                void Die()
                {
                    CurrentHealth = 0;
                    Debug.Log("You are dead");

                }
                void Addexp(int amountexp)
                {
                    CurrentExp += amountexp;
                    expbar.value = CalculateExp();
                    if (CurrentExp == 100)
                        LevelUp();
                }
                int CalculateExp()
                {
                    return CurrentExp / MaxExp;
                }
                void LevelUp()
                {
                    Level += 1;
                    CurrentExp = 0;
                    MaxExp += 10;
                    CurrentHealth = MaxHealth;
                    Debug.Log("You Leveled Up");
                }
                 */
}