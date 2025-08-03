using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool canInteract;
    public BoxCollider2D interactBox;

    public TextScript textScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && canInteract && !textScript.startText) 
        {
            textScript.startText = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable")) 
        {
            canInteract = true;
            
            textScript.textName = collision.gameObject.name; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canInteract = false;
            
        }
    }
}
