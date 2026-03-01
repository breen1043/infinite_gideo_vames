using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class monologue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private InputAction mouseClick;

    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();


        mouseClick = InputSystem.actions.FindAction("MouseClick");

        // mouseClickAction = InputSystem.actions.FindAction("MouseClick");

        // mouseClickAction.performed += MouseClickAction_performed;

    }


    // private void MouseClickAction_performed(InputAction.CallbackContext obj)
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         if (textComponent.text == lines[index])
    //         {
    //             NextLine();
    //         }
    //         else
    //         {
    //             StopAllCoroutines();
    //             textComponent.text = lines[index];
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     if (textComponent.text == lines[index])
        //     {
        //         NextLine();
        //     }
        //     else
        //     {
        //         StopAllCoroutines();
        //         textComponent.text = lines[index];
        //     }
            
        // }

        if (mouseClick.WasPressedThisFrame())
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
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
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else
        {
            gameObject.SetActive(false);
        }
    }

    // private void OnDestroy()
    // {
    //     mouseClickAction.performed -= MouseClickAction_performed;
    // }
}
