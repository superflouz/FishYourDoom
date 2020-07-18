using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    #region Events

    // Attack
    public delegate void OnDamageGain();
    public OnDamageGain onDamageGain;

    public delegate void OnDamageLoss();
    public OnDamageLoss onDamageLoss;

    // AttackSpeed
    public delegate void OnAttackSpeedGain();
    public OnAttackSpeedGain onAttackSpeedGain;

    public delegate void OnAttackSpeedLoss();
    public OnAttackSpeedLoss onAttackSpeedLoss;

    // Defense
    public delegate void OnDefenseGain();
    public OnDefenseGain onDefenseGain;

    public delegate void OnDefenseLoss();
    public OnDefenseLoss onDefenseLoss;

    #endregion

    // Variables

    private readonly float attackCap = 10;
    private readonly float attackCapMin = 0;
    public float attack = 1;
    public float Attack
    {
        get { return attack; }
        set
        {
            float oldValue = attack;
            attack = value;

            if (attack > attackCap) { attack = attackCap; }
            if (attack < attackCapMin) { attack = attackCapMin; }

            // Invoke event on loss or gain
            if (oldValue > attack) { onDamageLoss?.Invoke(); }
            if (oldValue < attack) { onDamageGain?.Invoke(); }
        }
    }

    private readonly float attackSpeedCap = 10;
    private readonly float attackSpeedCapMin = 0;
    public float attackSpeed = 1;   
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            float oldValue = attack;
            attackSpeed = value;

            if (attackSpeed > attackSpeedCap) { attackSpeed = attackSpeedCap; }
            if (attackSpeed < attackSpeedCapMin) { attackSpeed = attackSpeedCapMin; }

            // Invoke event on loss or gain
            if (oldValue > attackSpeed) { onAttackSpeedLoss?.Invoke(); }
            if (oldValue < attackSpeed) { onAttackSpeedGain?.Invoke(); }
        }
    }

    private readonly float defenseCap = 10;
    private readonly float defenseCapMin = 0;
    public float defense = 1;
    public float Defense
    {
        get { return defense; }
        set
        {
            float oldValue = defense;
            defense = value;

            if (defense > defenseCap) { defense = defenseCap; }
            if (defense < defenseCapMin) { defense = defenseCapMin; }

            // Invoke event on loss or gain
            if (oldValue > defense) { onDefenseLoss?.Invoke(); }
            if (oldValue < defense) { onDefenseGain?.Invoke(); }
        }
    }


    
}
