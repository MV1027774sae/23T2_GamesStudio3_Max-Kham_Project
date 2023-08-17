using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 5.0f; // Time in seconds before transitioning to the next scene
    private float timer = 0.0f;
    [SerializeField] Animator transitionAim;

    private bool isTransitioning = false;

    void Update()
    {
        if (!isTransitioning)
        {
            timer += Time.deltaTime;

            if (timer >= transitionTime)
            {
                isTransitioning = true;
                StartCoroutine(StartTransition());
            }
        }
    }

    IEnumerator StartTransition()
    {
        transitionAim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        LoadNextScene();

        transitionAim.SetTrigger("Start");
    }

    void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene in the build order
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
    }
}




