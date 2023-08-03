using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCollector : MonoBehaviour
{
    public int totalKeys = 5;
    private int collectedKeys = 0;
    public GameObject flag;
    public TextMeshProUGUI keysText; // Reference to the UI Text object
    public TextMeshProUGUI infoText; // Reference to the UI Text object for displaying info

    private void Start()
    {
        flag.SetActive(false);
        UpdateKeysText();
    }

    public void KeyCollected()
    {
        collectedKeys++;
        UpdateKeysText();

        if (collectedKeys >= totalKeys)
        {
            StartCoroutine(ShowInfoTextForSeconds("Go And Find the Flag and Get Out Here", 5f));
            ActivateFlag();
        }
    }

    private void UpdateKeysText()
    {
        keysText.text = "Keys: " + collectedKeys + " / " + totalKeys;
    }

    private void ActivateFlag()
    {
        flag.SetActive(true);
    }

    private IEnumerator ShowInfoTextForSeconds(string message, float seconds)
    {
        infoText.text = message;
        infoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        infoText.gameObject.SetActive(false);
    }
}

