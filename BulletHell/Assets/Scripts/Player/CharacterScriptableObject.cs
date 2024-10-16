using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get =>  icon; private set => icon = value; }
    [SerializeField]
    string name;
    public string Name { get => name; private set => name = value; }
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value;}

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value;}
    [SerializeField]
    float regen;
    public float Regen { get => regen; private set => regen = value;}
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed;  private set => moveSpeed = value;}
    [SerializeField]
    float might;
    public float Might { get => might;  private set => might = value;}
    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed;  private set => projectileSpeed = value;}
    [SerializeField]
    float pickupRange;
    public float PickupRange { get => pickupRange; private set => pickupRange = value; }
}
