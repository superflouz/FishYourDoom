using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
    // Variables
    private string weaponName;
    private WeaponType.Type type;
    private Animator animator;

    // Functions
    public abstract void Attack();
    public abstract void SpecialAttack();
}
