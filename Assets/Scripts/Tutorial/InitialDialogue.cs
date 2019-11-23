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
        bookOriginalPos = btn.transform.position;
        
        tutorial.dialogueManager.StartDialog(dialogue);
    }

    private void LogBookClick()
    {
        Button btn1 = tutorial.Logbook.GetComponent<Button>();
        btn1.interactable = true;
        firstClickLogBook = true;

       

        //tutorial.setState();
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
                btn1.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 + 50, 0);
                btn1.interactable = false;

                tutorial.falseLogbook.SetActive(true);

                Color myColor = tutorial.Logbook.GetComponent<Image>().material.color;
                myColor.a = 255;

                //tutorial.Logbook.GetComponent<Image>().material.color = myColor;

                btn1.onClick.RemoveListener(this.LogBookClick);
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = true;
                btn.onClick.Invoke();
                flag1 = false;
            }

            if (this.totalSentences - tutorial.dialogueManager.currentSentence() == 5)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
                Button btn1 = tutorial.Logbook.GetComponent<Button>();
                btn1.interactable = true;
                btn1.onClick.AddListener(LogBookClick);
            }

            if (this.totalSentences - tutorial.dialogueManager.currentSentence() == 2)
            {

                tutorial.falseLogbook.SetActive(false);


                Button btn1 = tutorial.Logbook.GetComponent<Button>();
                btn1.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 + 50, 0);
                btn1.interactable = true;

                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
                this.stage = DialogStages.BOOK_OPENED;
             
            }
        }

        if (this.stage == DialogStages.BOOK_OPENED)
        {
            Button btn1 = tutorial.Logbook.GetComponent<Button>();
            btn1.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 + 50, 0);
            int pague = tutorial.Logbook.GetComponent<ImageChange>().GetPageIndex();
            if (pague == 2 && flag2)
            {
                
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.onClick.Invoke();
                flag2 = false;
                
            }
            if (pague == 3 && !flag2)
            {

                tutorial.Logbook.GetComponent<ImageChange>().SetPageIndex(
                    tutorial.Logbook.GetComponent<ImageChange>().GetAllSpritesLength() -1 );
                btn1.onClick.Invoke();
                btn1.transform.position = bookOriginalPos;

                this.stage = DialogStages.BOOK_CLOSED;

            }



        }
        if (this.stage == DialogStages.BOOK_CLOSED)
        {
            int currentIndex = tutorial.indexCurrentDialog;
            Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
            tutorial.setState(new FirstExcercise(tutorial, nextDialog));
        }





    }

}
