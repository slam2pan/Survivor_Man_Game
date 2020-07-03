using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int level = 0;

    TextMeshProUGUI levelText;
    TextMeshProUGUI instructionsText;
    private static LevelManager levelInstance;
    CharacterController characterController;

    public int whatLevel
    {
        get { return level;  }
        set { level = value; }
    }

    // Ensure only one levelmanager exists
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (levelInstance == null)
        {
            levelInstance = this;
        } else
        {
            Destroy(this.gameObject);
        }

        SetLevelText();
    }

    // Lose and take player to game over scene
    private void Update()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        if (characterController.GetHealth() == 0)
        {
            whatLevel = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(this.gameObject); // Reset game
        }
    }

    private void OnLevelWasLoaded()
    {
        SetLevelText();
    }

    void SetLevelText()
    {
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        instructionsText = GameObject.Find("Instructions").GetComponent<TextMeshProUGUI>();
        levelText.SetText("Level " + level);
        if (whatLevel == 0)
        {
            instructionsText.enabled = true;
        } else
        {
            instructionsText.enabled = false;
        }
    }
}
