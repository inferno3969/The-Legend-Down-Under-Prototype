using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionNPCDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                StartCoroutine(MinionDialog());
            }
        }
    }

    private IEnumerator MinionDialog()
    {
        dialogBox.SetActive(true);
        dialogText.text = "BANANA!!!!";
        playSound.Play();
        yield return new WaitForSeconds(4f);
        dialogText.text = "We didn't serve anyone from 1933 to 1945, we were frozen in an ice cave, don't ask.";
        yield return new WaitForSeconds(4f);
        dialogText.text = "Speaking of ice cave... if you continue right, you can enter the ice cave... if you dare choose so...";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
