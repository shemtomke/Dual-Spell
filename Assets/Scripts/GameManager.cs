using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ninja NinjaMovement;
    public Knight KnightMovement;

    public CinemachineVirtualCamera cinemachineVirtualCamera;

    public GameObject ninja;
    public GameObject knight;

    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject LevelComplete;

    public AudioSource GamePlayAudio; //loop

    public int UnlockedKeys = 0;

    public bool Gameispaused = false;

    public float waitTimeforgameover = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //on start make player A the one to start 
        //Player A : Ninja
        //Player B : Knight
        NinjaMovement.enabled = true;
        KnightMovement.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        ToggleAnotherPlayer();
        PauseUI();
        UnlockDoor();
        GameOver();
    }

    public void ToggleAnotherPlayer()
    {
        //if i press a certain button then game changes from player A to B
        if(Input.GetKey(KeyCode.T))
        {
            NinjaMovement.enabled = false;
            KnightMovement.enabled = true;

            //camera changes to the player toggled
            cinemachineVirtualCamera.Follow = knight.GetComponent<Transform>();
        }
        else
        {
            NinjaMovement.enabled = true;
            KnightMovement.enabled = false;

            //change to ninja
            cinemachineVirtualCamera.Follow = ninja.GetComponent<Transform>();
        }
    }

    //GameOver
    private void GameOver()
    {
        if (NinjaMovement.isdead == true || KnightMovement.isdead == true)
        {
            Invoke("ShowGameOverScreen", waitTimeforgameover);
            NinjaMovement.ismove = false;
            KnightMovement.ismove = false;
            
        }

    }
    private void ShowGameOverScreen()
    {
        GameOverMenu.SetActive(true);
    }

    public void PauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Gameispaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }
    public void Resume()
    {
        Gameispaused = false;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Pause()
    {
        Gameispaused = true;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnClickRetryGame()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void UnlockDoor()
    {
        if(UnlockedKeys >= 2)
        {
            //level complete
            LevelComplete.SetActive(true);
        }
    }
}
