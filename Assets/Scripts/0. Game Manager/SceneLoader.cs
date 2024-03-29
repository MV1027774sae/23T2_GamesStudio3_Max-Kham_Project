using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("LoadNextScene called");
        // Load the next scene (assuming scenes are arranged sequentially)
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

