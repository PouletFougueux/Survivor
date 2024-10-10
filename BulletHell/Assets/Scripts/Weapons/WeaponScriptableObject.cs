using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    [SerializeField]
    float damage;
    public float Damage { get =>  damage; private set => damage = value; }
    [SerializeField]
    float speed;
    public float Speed { get =>  speed; private set => speed = value; }
    [SerializeField]
    float coolDownDuration;
    public float CoolDownDuration { get => coolDownDuration; private set => coolDownDuration = value; }
    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
    [SerializeField]
    float duration;
    public float Duration { get => duration; private set => duration = value; }

    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

}
