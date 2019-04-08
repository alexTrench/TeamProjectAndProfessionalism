using System;
using UnityEngine;

public class EXPSystem
{
    public event EventHandler OnEXPChanged;

    private int exp;
    private int expMAX;

    public EXPSystem(int expMAX)
    {
        this.expMAX = expMAX;
        exp = 0;
    }

    public int GetEXP()
    {
        return exp;
    }

    public float GetExpPercent()
    {
        return (float)exp / expMAX;
    }

    public void IncreaseExp(int increaseAmount)
    {
        exp = increaseAmount;
        if (exp >= expMAX) exp = 0;
        Debug.Log(exp + " " + increaseAmount + " " + GetExpPercent());
        OnEXPChanged?.Invoke(this, EventArgs.Empty);
    }
}
