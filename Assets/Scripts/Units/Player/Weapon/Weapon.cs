using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon : MonoBehaviour
{
    public enum Types
    {
        FishingRod = 0,
        Blowgun = 1
    }

    private Types type;
    public Types Type
    {
        get { return type; }
        set { type = value; }
    }

    // Functions
    public abstract void Attack();
    public abstract void SpecialAttack();
}
