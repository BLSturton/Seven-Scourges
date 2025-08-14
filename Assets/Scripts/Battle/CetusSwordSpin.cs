using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CetusSwordSpin : MonoBehaviour
{
    [SerializeField] OptionPicker cetusOptionPicker;
    [SerializeField] BattleManager battleManager;
    [SerializeField] GameObject Cetus;

    [SerializeField] public bool canSpin;
    [SerializeField] public bool isSpin;
    [SerializeField] public bool isSafe;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;

    [SerializeField] GameObject coolDown;
    [SerializeField] CooldownWipe cooldownWipe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     spriteRenderer.enabled = false;   
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      

        if(Input.GetKeyDown(KeyCode.K) && !isSpin) 
        {
           isSpin = true;
            StartCoroutine(SwordSpin());
        }
    }

    public IEnumerator SwordSpin() 
    {
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;

        animator.SetBool("SpinOn", true);

        float rotationTime = .3f; // Total time for rotation in seconds
        float rotationSpeed = 1080f; // Degrees per second
        float elapsedTime = 0f;
        isSafe = true;
        while (elapsedTime < rotationTime)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationThisFrame);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until next frame
        }

        animator.SetBool("SpinOn", false);
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        isSafe = false;
        cooldownWipe.StartCooldown();
        yield return new WaitForSeconds(1.7f);
        isSpin = false;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("GoblinRougeAttack") && isSafe)
        {
            Debug.Log("Trigger");
            Debug.Log("Aaa");
            collision.gameObject.SetActive(false);
        }
    }
}
