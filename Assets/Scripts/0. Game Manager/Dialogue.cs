using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue: MonoBehaviour
{

	public TextMeshProUGUI textComponenet;
	public string[] lines;
	public float textSpeed;

	private int index;

    void Start()
    {
        textComponenet.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponenet.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponenet.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char C in lines[index].ToCharArray())
        {
            textComponenet.text += C;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponenet.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
      
    }
}