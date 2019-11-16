using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public Animator animator;


    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        Debug.Log("Star conversation with " + dialogue.name);
        sentences.Clear();
        nameText.text = dialogue.name;

        foreach (string sentece in dialogue.sentences)
        {
            sentences.Enqueue(sentece);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of conversation");
    }
}
