using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("01Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
