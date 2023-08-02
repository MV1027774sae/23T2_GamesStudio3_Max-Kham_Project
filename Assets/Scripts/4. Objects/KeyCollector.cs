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
}
