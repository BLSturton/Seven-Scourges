using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Move
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    //Animation
    public Animator CetusAnim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CetusAnim.SetBool("Idle", true);
    }
    // Update is called once per frame
    void Update()
    {
        //This section detects movement by input, and then plays the animations depending on the input.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement == new Vector2(0, 0))
        {
            CetusAnim.SetBool("Idle", true);
            CetusAnim.SetBool("WalkDown", false);
            CetusAnim.SetBool("WalkUp", false);
            CetusAnim.SetBool("WalkLeft", false);
            CetusAnim.SetBool("WalkRight", false);
        }
        if(movement.y == -1) 
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
