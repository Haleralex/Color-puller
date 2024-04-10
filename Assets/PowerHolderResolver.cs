using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHolderResolver : MonoBehaviour
{
    [SerializeField] private AimController aimController;
    [SerializeField] private PowerHolderUI powerHolderUI;
    private void Awake()
    {
        var powerHolder = new PowerHolder();
        
        aimController.PowerHolder = powerHolder;
        powerHolderUI.SetPowerHolder(powerHolder);
    }
}
