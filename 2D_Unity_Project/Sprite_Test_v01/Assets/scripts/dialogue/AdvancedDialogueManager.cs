using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedDialogueManager : MonoBehaviour
{
    private AdvancedDialogueSO currentConversation;
    private int stepNum;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitiateDialogue(NPCDialogue npcDialogue)
    {
        currentConversation = npcDialogue.conversation[0];
        Debug.Log("Started conversation: " + currentConversation);
    }

    public void TurnOffDialogue()
    {
       stepNum = 0;
       Debug.Log("Ended conversation. Reset the step to " + stepNum);
    }
}

public enum DialogueActors
{
    samurai,
    Player,
    Random,
    Branch
};