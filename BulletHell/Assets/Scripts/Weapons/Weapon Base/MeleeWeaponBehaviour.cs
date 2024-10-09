using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCdDuration;
    protected float currentDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCdDuration = weaponData.CoolDownDuration;
        currentDuration = weaponData.Duration;
        currentPierce = weaponData.Pierce;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, weaponData.Duration);
    }

    protected virtual void OnTrigegrEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps prop))
            {
                prop.TakeDamage(currentDamage);
            }
        }
    }
    
}
