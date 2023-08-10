using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Image imageComponent; // Add this reference to your Image component
    public string[] lines;
    public Sprite[] images; // Add an array of images corresponding to your lines
    public float textSpeed;

    private int index;
    private bool isDisplayingText;

    void Start()
    {
        textComponent.text = string.Empty;
        imageComponent.sprite = null; // Set the initial image to be empty
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isDisplayingText)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isDisplayingText = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        isDisplayingText = false;
        imageComponent.sprite = images[index]; // Show the image for the first line
    }

    IEnumerator TypeLine()
    {
        isDisplayingText = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isDisplayingText = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            imageComponent.sprite = images[index]; // Show the image for the next line
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
