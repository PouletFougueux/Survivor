using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> hitEnemies;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hitEnemies = new List<GameObject>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !hitEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());

            hitEnemies.Add(col.gameObject);
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps prop) && !hitEnemies.Contains(col.gameObject))
            {
                prop.TakeDamage(GetCurrentDamage());
                hitEnemies.Add(col.gameObject);
            }
        }
    }
}

