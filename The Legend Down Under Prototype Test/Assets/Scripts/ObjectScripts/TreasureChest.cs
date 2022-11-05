using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpened;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpened)
            {
                // open the chest
                OpenChest();
                playSound.Play();
            }
            else
            {
                // chest is already opened
                ChestAlreadyOpened();
            }
        }
    }

    public void OpenChest()
    {
        // dialog box on
        dialogBox.SetActive(true);
        // dialog text = contents text
        dialogText.text = contents.itemDescriptiopn;
        // add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // raise the signal to the player to animate correctly
        raiseItem.Raise();
        // raise the context clue
        context.Raise();
        // set the chest to opened
        isOpened = true;
        anim.SetBool("opened", true);
    }

    public void ChestAlreadyOpened()
    {
        // turn dialog box off
        dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            playerInRange = false;
            context.Raise();
        }
    }
}
