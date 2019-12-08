using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialDialogue : DialogueState
{
    enum DialogStages  {
        BEFORE_OPEN_BOOK,
        BOOK_OPENED,
        BOOK_CLOSED
    };

    private int totalSentences;
    private bool firstClickLogBook;
    private bool flag1 = true;
    private bool flag2 = true;
    private DialogStages stage = DialogStages.BEFORE_OPEN_BOOK;
    private Vector3 bookOriginalPos;


    public InitialDialogue(Tutorial tutorial, Dialogue dialogue) 
        : base(tutorial, dialogue, "Tutorial1InitDialog")
    {
        firstClickLogBook = false;
    }

    public override void OnStateEnter()
    {
        this.totalSentences = dialogue.sentences.Length;
        Button btn = tutorial.Logbook.GetComponent<Button>();
        btn.interactable = false;
        
        tutorial.dialogueManager.StartDialog(dialogue);
    }

    private void LogBookClick()
    {
        Button btn1 = tutorial.Logbook.GetComponent<Button>();
        btn1.interactable = true;
        firstClickLogBook = true;
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
        if (this.stage == DialogStages.BEFORE_OPEN_BOOK)
        {
            if (firstClickLogBook && flag1)
            {
                Button btn1 = tutorial.Logbook.GetComponent<Button>();
                btn1.interactable = false;

                Color myColor = tutorial.Logbook.GetComponent<Image>().material.color;
                myColor.a = 255;

                btn1.onClick.RemoveListener(this.LogBookClick);
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = true;
                btn.onClick.Invoke();
                flag1 = false;
            }

            if (tutorial.dialogueManager.currentSentence() == 6)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
                Button btn1 = tutorial.Logbook.GetComponent<Button>();
                btn1.interactable = true;
                btn1.onClick.AddListener(LogBookClick);
            }

            if (tutorial.dialogueManager.currentSentence() == 7)
            {
                Button btn1 = tutorial.Logbook.GetComponent<Button>();
                btn1.interactable = true;

                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
                this.stage = DialogStages.BOOK_OPENED;
            }
        }

        if (this.stage == DialogStages.BOOK_OPENED)
        {
            int page = tutorial.LogbookPage.GetComponent<ImageChange>().GetPageIndex();

            if (page == 0 && flag2)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.onClick.Invoke();
                flag2 = false;
            }
            if (page == 1 && !flag2)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.onClick.Invoke();
            }
            if (!tutorial.LogbookPage.activeInHierarchy && !flag2)
            {
                this.stage = DialogStages.BOOK_CLOSED;
            }
        }

        if (this.stage == DialogStages.BOOK_CLOSED)
        {
            int currentIndex = tutorial.indexCurrentDialog;
            Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
            tutorial.setState(new Naming(tutorial, nextDialog));
        }

    }

}
