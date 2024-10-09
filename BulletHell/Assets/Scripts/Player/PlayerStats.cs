using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private CharacterScriptableObject characterData;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRegen;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentPickupRange;

    public List<GameObject> spawnedWeapons;

    [Header("Leveling")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    [Header("IFrames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();
        currentHealth = characterData.MaxHealth;
        currentRegen = characterData.Regen;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentPickupRange = characterData.PickupRange;

        SpawnWeapon(characterData.StartingWeapon);
    }
    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Regen();
    }

    public void IncreaseXP(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
        
    }

    public void Heal(float amount)
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            if (currentHealth > characterData.MaxHealth)
                currentHealth = characterData.MaxHealth;
        }
        
    }
    void Regen()
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRegen * Time.deltaTime;
            if (currentHealth > characterData.MaxHealth)
                currentHealth = characterData.MaxHealth;
        }
    }

    public void Kill()
    {
        Debug.Log("TMOOORT");
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
}
