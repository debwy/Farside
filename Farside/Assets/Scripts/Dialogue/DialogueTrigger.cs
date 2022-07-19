using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    private bool playerAbleToDialogue;

    //initial state: player not in range
    private void Awake() 
    {
        playerInRange = false;
        playerAbleToDialogue = true;
        visualCue.SetActive(false);
    }

    //show the chatbox indicator above NPC when in range
    private void Update()
    {
        //if dialogue is playing, visual cue becomes inactive and cannot
        //trigger dialogue until current dialogue has finished
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            playerAbleToDialogue = GameObject.Find("Player").GetComponent<PlayerMain>().IsAbleToDialogue();

            if (playerAbleToDialogue) {
                visualCue.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    //determine when player collider enters NPC range
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
   
}
