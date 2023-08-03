using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private GameObject heartsUI;
    private GameObject manaUI;
    private GameObject keysUI;

    [SerializeField] private GameObject gameOverMenu;

    void Start()
    {
        heartsUI = GameObject.Find("HeartsUI");
        manaUI = GameObject.Find("ManaUI");
        keysUI = GameObject.Find("KeysUI");

        DisableMenu();
    }
    public void GameOver()
    {
        EnableMenu();
    }

    public void Retry()
    {
        DisableMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        DisableMenu();
        SceneManager.LoadScene(1);
    }

    private void EnableMenu()
    {
        heartsUI.SetActive(false);
        manaUI.SetActive(false);
        keysUI.SetActive(false);
        Cursor.visible = true;

        gameOverMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    private void DisableMenu()
    {
        heartsUI.SetActive(true);
        manaUI.SetActive(true);
        keysUI.SetActive(true);
        Cursor.visible = false;

        gameOverMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}
