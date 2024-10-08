using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected playerMovement pm;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<playerMovement>();
        currentCooldown = weaponData.CoolDownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0 )
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CoolDownDuration;
    }
}
