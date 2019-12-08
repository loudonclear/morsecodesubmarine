using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Naming : DialogueState
{

    private int totalSentences;
    private bool flag1 = false;
    private bool flag2 = false;


    public Naming(Tutorial tutorial, Dialogue dialogue)
        : base(tutorial, dialogue, "NamingDialogue")
    {

    }

    public override void OnStateEnter()
    {
        this.totalSentences = dialogue.sentences.Length;
        Button btn = tutorial.continueButton.GetComponent<Button>();
        btn.interactable = true;
        tutorial.dialogueManager.StartDialog(dialogue);

        tutorial.shipName.text = "S.S. ";
    }

    public override void OnStateExit()
    {

    }

    private string temp = "";

    public override void Tick()
    {
        int sentence = tutorial.dialogueManager.currentSentence();
        if (sentence == 4)
        {
            if (temp != tutorial.morseCodeMachine.GetComponent<MorseCodeController>().decodedText.text)
            {
                temp = tutorial.morseCodeMachine.GetComponent<MorseCodeController>().decodedText.text;

                if (tutorial.shipName.text.Length < 15 && temp.Length >= 1)
                {
                    tutorial.shipName.text += temp[temp.Length - 1];
                }
            }

            Button btn = tutorial.continueButton.GetComponent<Button>();
            if (tutorial.shipName.text.Length < 6)
            {
                btn.interactable = false;
            } else
            {
                btn.interactable = true;
            }
        }

        if (sentence == 5)
        {
            tutorial.morseCodeMachine.GetComponent<CommandInterpreter>().commandListen = true;
            int currentIndex = tutorial.indexCurrentDialog + 1;
            Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
            tutorial.setState(new FirstExcercise(tutorial, nextDialog));
        }
    }
}
