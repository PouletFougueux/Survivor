using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, weaponData.Duration);
    }

    // Update is called once per frame
    
}
