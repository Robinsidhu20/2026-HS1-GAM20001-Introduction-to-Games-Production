using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer speechBubbleRenderer;


    // Start is called before the first frame update
    void Start()
    {
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false; // Hide the speech bubble at the start
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Speech bubble on
            speechBubbleRenderer.enabled = true;

            //Get the player transform
            player = collision.gameObject.GetComponent<Transform>();

            //Check to see where the player is, and then turn towards them
            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            }

            else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Speech bubble off
            speechBubbleRenderer.enabled = false;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1; // Flip the x scale to face the opposite direction
        transform.parent.localScale = currentScale;
    }

}
