using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button playButton;
    public Button continueButton;
    public Button optionsButton;
    public Button quitButton;
    public GameObject optionsPanel;

    private bool isPaused = false;

    private void Start()
    {
        // Assign button click event handlers
        playButton.onClick.AddListener(PlayGame);
        continueButton.onClick.AddListener(ContinueGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);

        // Hide the pause menu initially
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        // Check for pause input (e.g., pressing the Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ContinueGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        // Show the pause menu
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void ContinueGame()
    {
        // Hide the pause menu
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenOptions()
    {
        // Show the options panel
        optionsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        // Add your code to quit the game here
    }

    public void ClosePanel()
    {
       optionsPanel.SetActive(false);
    }
}

