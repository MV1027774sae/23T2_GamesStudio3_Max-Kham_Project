using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    private void Start()
    {
        Cursor.visible = true;
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene(4);
    }
    public void EnableCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void DisableCredits()
    {
        creditsPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
