using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
