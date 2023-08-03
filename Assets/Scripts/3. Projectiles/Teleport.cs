using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        MoveToNextScene();
    }

    // Move to the next scene
    private void MoveToNextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available. You have reached the end of the build settings scene list.");
        }
    }
}

