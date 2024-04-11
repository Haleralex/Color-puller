using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerHolderUI : MonoBehaviour
{
    private Dictionary<PowerColors, Slider> _powerSliders = new();

    [SerializeField] private List<Slider> sliders = new();
    [SerializeField] private List<PowerColors> powers = new();

    private PowerHolder _powerHolder;
    
    private void Awake()
    {
        var maxAmount = Mathf.Min(sliders.Count, powers.Count);

        for (int i = 0; i < maxAmount; i++)
        {
            _powerSliders.Add(powers[i],sliders[i]);
        }
    }

    public void SetPowerHolder(PowerHolder powerHolder)
    {
        _powerHolder = powerHolder;
        _powerHolder.LevelOfPowersChanged += OnLevelOfPowersChanged;
    }

    private void OnLevelOfPowersChanged(PowerColorsChange obj)
    {
        _powerSliders[obj.PowerColors].value = obj.NewValue;
    }
}
