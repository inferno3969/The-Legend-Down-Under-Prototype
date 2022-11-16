using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType
{
    key,
    enemy,
    button,
    boss
}

public class Door : Interactable
{

    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public AudioSource doorUnlocked;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerInRange && thisDoorType == DoorType.key && !open)
            {
                // does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    // remove a player key
                    playerInventory.numberOfKeys--;
                    doorUnlocked.Play();
                    // if so, call the open method
                    Open();
                }
                else
                {
                    dialogText.text = dialog;
                    StartCoroutine(LockCo());
                }
            }
            else if (playerInRange && thisDoorType == DoorType.boss && !open)
            {
                // does the player have a key?
                if (playerInventory.numberOfBossKeys > 0)
                {
                    // remove a player key
                    playerInventory.numberOfBossKeys--;
                    doorUnlocked.Play();
                    // if so, call the open method
                    Open();
                }
                else
                {
                    dialogText.text = dialog;
                    StartCoroutine(LockCo());
                }
            }
            else if (playerInRange && thisDoorType == DoorType.button && !open)
            {
                dialogText.text = dialog;
                StartCoroutine(LockCo());
            }
        }
        
    }

    public IEnumerator LockCo()
    {
        dialogBox.SetActive(true);
        yield return new WaitForSeconds(4f);
        dialogBox.SetActive(false);
    }

    public void Open()
    {
        // turn off the door sprite renderer
        doorSprite.enabled = false;
        // set open to true
        open = true;
        // turn off the door's box collider
        physicsCollider.enabled = false;

        // since the Switch script calls this Open() method,
        // we only want to raise context for key and boss doors so that the switch
        // itself can't use context clue signals 
        if (thisDoorType == DoorType.key || thisDoorType == DoorType.boss)
        {
            context.Raise();
        }
    }

    public void Close()
    {
        // turn off the door's sprite renderer
        doorSprite.enabled = true;
        // set open to true
        open = false;
        // turn off the door's box collider
        physicsCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !open)
        {
            if (thisDoorType != DoorType.enemy)
            {
                context.Raise();
            }
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !open)
        {
            if (thisDoorType != DoorType.enemy)
            {
                context.Raise();
            }
            if (dialogBox != null)
            {
                dialogBox.SetActive(false);
            }
            playerInRange = false;
        }
    }
}