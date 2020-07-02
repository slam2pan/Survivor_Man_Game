using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yup");
        if (other.gameObject.CompareTag("Player"))
        {
            levelManager.whatLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
