using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup, ICollectible
{
    public float healthGranted;
    public void Collect()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        stats.Heal(healthGranted);
    }
}
