using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHolder
{
    public event Action<PowerColorsChange> LevelOfPowersChanged;
    private Dictionary<PowerColors, float> levelOfPowers = new();

    public float this[PowerColors index] => levelOfPowers[index];

    public PowerHolder()
    {
        levelOfPowers.Add(PowerColors.Red, 0);
        levelOfPowers.Add(PowerColors.Blue, 0);
        levelOfPowers.Add(PowerColors.Green, 0);
    }

    public void ChangePower(PowerColorsChange powerColorsChange)
    {
        if (powerColorsChange.NewValue < 0)
            throw new Exception("New value powers less than zero");

        levelOfPowers[powerColorsChange.PowerColors] = powerColorsChange.NewValue;
        
        LevelOfPowersChanged?.Invoke(powerColorsChange);
    }
}
