using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BattleMove : MonoBehaviour
{
    //Move
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] Vector2 movement;
    [SerializeField] public bool canMove;
    //Animation
    [SerializeField] public Animator CetusAnim;

    //Hitreg
    [SerializeField] public BoxCollider2D hitBox;
    [SerializeField] public bool canBeHit;

    //Scripts
    [SerializeField] BattleManager battleManager;
    [SerializeField] CetusSwordSpin swordSpin;
    [SerializeField] CooldownWipe cooldownWipe; 

    [SerializeField] public bool isDash;
    [SerializeField] public bool canDash;

    [SerializeField] public float ultCoolDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canBeHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (battleManager.turnReset == true)
        {
            cooldownWipe.isOnCooldown = false;
        }
            //This section detects movement by input, and then plays the animations depending on the input.
            if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = new Vector2(0, 0);
        }
        if (movement == new Vector2(0, 0))
        {
            CetusAnim.SetBool("Idle", true);
            CetusAnim.SetBool("WalkDown", false);
            CetusAnim.SetBool("WalkUp", false);
            CetusAnim.SetBool("WalkLeft", false);
            CetusAnim.SetBool("WalkRight", false);
        }
        if (movement.y == -1)
        {
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkDown", true);
            CetusAnim.SetBool("WalkLeft", false);
            CetusAnim.SetBool("WalkRight", false);
        }
        if (movement.y == 1)
        {
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkUp", true);
            CetusAnim.SetBool("WalkLeft", false);
            CetusAnim.SetBool("WalkRight", false);
        }
        if (movement.x == -1)
        {
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkLeft", true);
            CetusAnim.SetBool("WalkDown", false);
            CetusAnim.SetBool("WalkUp", false);
        }
        if (movement.x == 1)
        {
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkRight", true);
            CetusAnim.SetBool("WalkDown", false);
            CetusAnim.SetBool("WalkUp", false);
        }
        if (this.gameObject.name == "RaticDodge" && Input.GetKeyDown(KeyCode.K) && canDash && !isDash)
        {
            Debug.Log("Dodging");
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        //Move
        if (!isDash) 
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            
        }
       
    }

    public IEnumerator Dash() 
    {
        canDash = false;
        isDash = true;

        // Capture dash direction immediately when input is pressed
        Vector2 dashDirection = movement.normalized;

        // If no direction is pressed, default to facing right (or choose another default)
        if (dashDirection == Vector2.zero)
        {
            canDash = true;
            isDash = false;
            yield break;

        }
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        float dashDuration = 0.15f; // Total duration of dash
        float dashSpeed = 15f;     // Speed of dash

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate(); // Wait for next physics update
        }
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isDash = false;
        cooldownWipe.StartCooldown();

        yield return new WaitForSeconds(ultCoolDown); // Cooldown before can dash again
        canDash = true;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
         if (this.gameObject.name == "CetusDodge" && canBeHit && swordSpin.isSafe == false)
        {
            StartCoroutine(HitCooldown());
            if (collision.CompareTag("GoblinRougeAttack"))
            {
                battleManager.damageValue = 2;
                battleManager.CetusDamage();
            }
        }

        if (this.gameObject.name == "RaticDodge" && canBeHit)
        {
            StartCoroutine(HitCooldown());
            if (collision.CompareTag("GoblinRougeAttack"))
            {
                battleManager.damageValue = 2;
                battleManager.RaticDamage();
            }
        }

    }

    public IEnumerator HitCooldown() 
    {
        canBeHit = false;
        for (int i = 0; i < 5; i++) 
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        canBeHit = true;
    }
}
