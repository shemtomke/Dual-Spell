using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
