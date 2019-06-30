using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            Debug.Log("Game is playing");
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log("Game is paused");
        }
    }
}
