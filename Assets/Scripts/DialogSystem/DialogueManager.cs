using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    
    public Text nameText;
    public Text dialogText;
    public Animator animator;
    public bool dialogEnded;
    private int dialogSentences;
    private int currentSentece;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialogue dialogue)
    {
        this.currentSentece = 0;
        dialogEnded = false;
        animator.SetBool("isOpen", true);
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
        this.currentSentece++;
        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));
    }

    IEnumerator TypeSentece(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.08f);
        }
    }

    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
        dialogEnded = true;
    }

    public int currentSentence()
    {
        return this.currentSentece;
    }

}
