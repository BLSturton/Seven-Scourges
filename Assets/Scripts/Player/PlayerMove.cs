using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Move
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public bool canMove;
    //Animation
    public Animator CetusAnim;

    public BoxCollider2D interactBox;
    [SerializeField] public Inventory inventory;

    [SerializeField] TextScript dialog;
    private void Awake()
    {
        inventory = GameObject.FindWithTag("InventorySystem").gameObject.GetComponent<Inventory>();

        dialog = GameObject.FindWithTag("Dialog").GetComponent<TextScript>();
        rb = GetComponent<Rigidbody2D>();
        CetusAnim.SetBool("Idle", true);
        if(dialog.FirstScene == false) 
        {
            canMove = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inventory.cantMove == true)
        {
            canMove = true;
        }
        if (inventory.inventoryOn) 
        {
            canMove = false;
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
        if(movement.y == -1) 
        {
            interactBox.offset = new Vector2(0, -.5f);
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkDown", true);
            CetusAnim.SetBool("WalkLeft", false);
            CetusAnim.SetBool("WalkRight", false);
        }
        if (movement.y == 1)
        {
            interactBox.offset = new Vector2(0, 1.5f);
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkUp", true);
            CetusAnim.SetBool("WalkLeft", false);
            CetusAnim.SetBool("WalkRight", false);
        }
        if (movement.x == -1)
        {
            interactBox.offset = new Vector2(-1.2f, 0);
            CetusAnim.SetBool("Idle", false);
            CetusAnim.SetBool("WalkLeft", true);
            CetusAnim.SetBool("WalkDown", false);
            CetusAnim.SetBool("WalkUp", false);
        }
        if (movement.x == 1)
        {
            interactBox.offset = new Vector2(1.2f, 0);
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
