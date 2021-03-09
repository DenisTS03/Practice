using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject levelPanel;
    
    public void LevelHandler() {
        SceneManager.LoadScene(2);
        /*levelPanel.SetActive(true);
        menuPanel.SetActive(false); */
    }

    public void BackMenuHandler() {
        menuPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void ExitHandler() {
        Application.Quit();
    }

    public void FirstLevelHandler() {
        SceneManager.LoadScene(2);
    }

    public void SecondLevelHandler() {
        SceneManager.LoadScene(3);
    }

    public void ThirdLevelHandler() {
        SceneManager.LoadScene(4);
    }
}
