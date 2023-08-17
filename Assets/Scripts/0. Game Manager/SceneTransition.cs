using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 5.0f; // Time in seconds before transitioning to the next scene
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= transitionTime)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene in the build order
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

