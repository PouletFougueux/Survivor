using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Character Stats
    private CharacterScriptableObject characterData;
    float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health : " + currentHealth;
                }
            }
        }
    }
    float currentRegen;
    public float CurrentRegen
    {
        get { return currentRegen; }
        set
        {
            if (currentRegen != value)
            {
                currentRegen = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRegenDisplay.text = "Regen : " + currentRegen;
                }
            }
        }
    }
    float currentMoveSpeed;
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed : " + currentMoveSpeed;
                }
            }
        }
    }
    float currentMight;
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might : " + currentMight;
                }
            }
        }
    }
    float currentProjectileSpeed;
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed : " + currentProjectileSpeed;
                }
            }
        }
    }
    float currentPickupRange;
    public float CurrentPickupRange
    {
        get { return currentPickupRange; }
        set
        {
            if (currentPickupRange != value)
            {
                currentPickupRange = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentPickupRangeDisplay.text = "Pickup Range : " + currentPickupRange;
                }
            }
        }
    }
    #endregion

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

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveTest, secondPassiveTest;

    private void Awake()
    {
        
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();


        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterData.MaxHealth;
        CurrentRegen = characterData.Regen;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentPickupRange = characterData.PickupRange;

        SpawnWeapon(characterData.StartingWeapon);
        SpawnWeapon(secondWeaponTest);
        SpawnPassiveItem(firstPassiveTest);
        SpawnPassiveItem(secondPassiveTest);
    }
    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.currentHealthDisplay.text = "Health : " + currentHealth;
        GameManager.instance.currentRegenDisplay.text = "Regen : " + currentRegen;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed : " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might : " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed : " + currentProjectileSpeed;
        GameManager.instance.currentPickupRangeDisplay.text = "Pickup Range : " + currentPickupRange;
        GameManager.instance.AssignCharacterUIData(characterData);

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
            CurrentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }
        
    }

    public void Heal(float amount)
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;
            if (CurrentHealth > characterData.MaxHealth)
                CurrentHealth = characterData.MaxHealth;
        }
        
    }
    void Regen()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRegen * Time.deltaTime;
            if (currentHealth > characterData.MaxHealth)
                CurrentHealth = characterData.MaxHealth;
        }
    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItems(inventory.weaponsUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if ( weaponIndex >= inventory.weaponSlots.Count -1)
        {
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }
}
