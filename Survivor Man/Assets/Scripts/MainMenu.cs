using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip buttonClick;
    private AudioSource menuAudio;

    private void Start()
    {
        menuAudio = GetComponent<AudioSource>();    
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void clickAudio()
    {
        menuAudio.PlayOneShot(buttonClick);
    }

}
