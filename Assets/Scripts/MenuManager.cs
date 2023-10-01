using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void Play()
    {
        SceneManager.LoadScene("InitScene", LoadSceneMode.Single);
    }
}
