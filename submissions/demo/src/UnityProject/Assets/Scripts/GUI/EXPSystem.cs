using System;
using UnityEngine;

public class EXPSystem
{
    // public event that deals with changes of the value of experience
    public event EventHandler OnEXPChanged;

    // local private variables that hold experience and max experience
    private int exp;
    private int expMAX;

    // initialise function for the local private variables
    public EXPSystem(int expMAX)
    {
        this.expMAX = expMAX;
        exp = 0;
    }

    // Get EXP
    public int GetEXP()
    {
        return exp;
    }

    // Get EXP percent
    public float GetExpPercent()
    {
        return (float)exp / expMAX;
    }

    // Set Max EXP
    public void SetMaxExp(int newExpMax)
    {
        expMAX = newExpMax;
    }

    // Increase EXP
    public void IncreaseExp(int increaseAmount)
    {
        exp = increaseAmount;
        if (exp >= expMAX) exp = 0;
        OnEXPChanged?.Invoke(this, EventArgs.Empty);
    }
}
