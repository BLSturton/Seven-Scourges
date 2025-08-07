using UnityEngine;

public class BattleMove : MonoBehaviour
{
    //Move
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public bool canMove;
    //Animation
    public Animator CetusAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
