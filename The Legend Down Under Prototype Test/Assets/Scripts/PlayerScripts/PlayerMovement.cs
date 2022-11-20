using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}
public class PlayerMovement : MonoBehaviour
{

    public PlayerState currentState;
    public float speed;
    public Vector2 velocity = Vector2.zero;
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;
    public VectorValue startingPosition;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public SignalSender playerHit;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public SceneManager gameOver;
    public AudioSource swordSwing;

    // this bool is important to prevent player from
    // being able to spam thr attack button
    // and also prevents Player sprite from
    // moving while attacking
    public bool isAttacking = false;

    [Header("Projectile Stuff")]
    public GameObject projectile;
    public GameObject bow;

    [Header("IFrame stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            velocity = new Vector2(1, 0);
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            velocity = new Vector2(0, 1);
        }
        Update();
    }

    void Update()
    {
        // is the player in an interaction
        if (currentState == PlayerState.interact)
        {
            return;
        }
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger
            && !isAttacking)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetKeyDown(KeyCode.M) && currentState != PlayerState.attack && currentState != PlayerState.stagger
            && !isAttacking)
        {
            StartCoroutine(SecondAttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        isAttacking = true;
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        if (swordSwing != null)
        {
            swordSwing.Play();
        }
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        yield return new WaitForSeconds(.25f);
        isAttacking = false;
    }

    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("attacking", true);
        isAttacking = true;
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        yield return new WaitForSeconds(.25f);
        isAttacking = false;
    }

    private void MakeArrow()
    {
        //if (playerInventory.currentMagic > 0)
        //{
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            //playerInventory.ReduceMagic(arrow.magicCost);
            //reduceMagic.Raise();
        //}
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero && !isAttacking)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("StartMenu");
        }

    }

    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;

        while (temp < numOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
