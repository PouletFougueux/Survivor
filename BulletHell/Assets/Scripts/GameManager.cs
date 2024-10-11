using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        GamePlay,
        Paused,
        GameOver
    }

    public GameState currentState;
    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultScreen;

    [Header("Current Stats Display")]
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentRegenDisplay;
    public TextMeshProUGUI currentMoveSpeedDisplay;
    public TextMeshProUGUI currentMightDisplay;
    public TextMeshProUGUI currentProjectileSpeedDisplay;
    public TextMeshProUGUI currentPickupRangeDisplay;

    [Header("Result Screen Display")]
    public Image chosenCharacterImage;
    public TextMeshProUGUI chosenCharacterName;

    public TextMeshProUGUI timeSurvived;
    public TextMeshProUGUI levelReached;

    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    public bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DisableScreens();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.GamePlay:
                CheckForPause();
                break;
            case GameState.Paused:
                CheckForPause();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0;
                    DisplayResults();
                }
                break;
            default:
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
       
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    public void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultScreen.SetActive(true);
    }

    public void AssignCharacterUIData(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReached.text = "Level Reached : " + levelReachedData;
    }

    public void AssignChosenWeaponsAndPassiveItems(List<Image> weapons, List<Image> passives)
    {
        if (weapons.Count != chosenWeaponsUI.Count || passives.Count != chosenPassiveItemsUI.Count)
        {
            return;
        }
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            if (weapons[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = weapons[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }
        
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (passives[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = passives[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }
}
