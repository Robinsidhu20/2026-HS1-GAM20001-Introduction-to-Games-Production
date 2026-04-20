using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public AdvancedDialogueSO[] conversation;

    [SerializeField] private SpriteRenderer speechBubbleRenderer;
    private AdvancedDialogueManager advancedDialogueManager;
    private bool dialogueInitiated;
    private Transform playerInRange = null;

    void Start()
    {
        advancedDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvancedDialogueManager>();
        if (speechBubbleRenderer != null)
        {
            speechBubbleRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D called on NPCDialogue for: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //speech bubble on
            if (speechBubbleRenderer != null)
                speechBubbleRenderer.enabled = true;

            advancedDialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
        }
        if (collision.CompareTag("Player"))
        {
            playerInRange = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (speechBubbleRenderer != null)
            {
                speechBubbleRenderer.enabled = false;
            }
            advancedDialogueManager.TurnOffDialogue();
            dialogueInitiated = false;
            playerInRange = null;
        }
    }

    void Update()
    {
        if (playerInRange != null)
        {
            FacePlayer(playerInRange.position);
        }
    }

    private void FacePlayer(Vector3 playerPosition)
    {
        float directionToPlayer = playerPosition.x - transform.position.x;
        float npcFacing = Mathf.Sign(transform.localScale.x);
        float desiredFacing = Mathf.Sign(directionToPlayer);
        if (directionToPlayer != 0 && npcFacing != desiredFacing)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
