using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int level = 0;

    TextMeshProUGUI levelText;
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

    // Lose and restart if player runs out of health
    private void Update()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        if (characterController.GetHealth() == 0)
        {
            whatLevel = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("You lose");
        }
    }

    private void OnLevelWasLoaded()
    {
        SetLevelText();
    }

    void SetLevelText()
    {
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        levelText.SetText("Level " + level);
    }
}
