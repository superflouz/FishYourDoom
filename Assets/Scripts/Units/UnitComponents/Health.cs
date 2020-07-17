﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Base values
    public float maxHealth = 100;
    public float threshHoldHealth = 50;
    private float minHealth = 0;

    // Events
    public delegate void OnHealthLoss();
    public OnHealthLoss onHealthLoss;

    public delegate void OnHealthGain();
    public OnHealthGain onHealthGain;

    public delegate void OnThreshold();
    public OnThreshold onThreshold;

    public delegate void OnDeath();
    public OnDeath onDeath;

    // Health management
    private float currentHealth;
    private float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            float oldValue = currentHealth;
            currentHealth = value;

            // Check if the health isn't too high or too low
            if (currentHealth > maxHealth) { currentHealth = maxHealth; }
            if (currentHealth < minHealth) { currentHealth = minHealth; }

            // Invoke event on health loss or gain
            if (oldValue > currentHealth) { onHealthLoss?.Invoke(); }
            if (oldValue < currentHealth) { onHealthGain?.Invoke(); }
        }
    }

    void Start()
    {
        onHealthLoss += HealthLost;
    }

    // Fired Every time there is a health loss
    public void HealthLost()
    {
        if (currentHealth <= minHealth) { onDeath?.Invoke(); }
        else if(currentHealth <= threshHoldHealth) { onThreshold?.Invoke(); }
    }

    /// <summary>
    /// Modify Unit's health, use negative number for health loss
    /// </summary>
    /// <param name="amount">Amount of Health to give to this Unit</param>
    public void ModifyHealth(float amount)
    {
        CurrentHealth += amount;
    }
}
