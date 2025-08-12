using System.Collections;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canBeHit = true;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        //Move
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (this.gameObject.name == "CetusDodge" && canBeHit) 
        {
            StartCoroutine(HitCooldown());
            if (collision.CompareTag("GoblinRougeAttack"))
            {
                battleManager.damageValue = 2;
                battleManager.CetusDamage();
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
