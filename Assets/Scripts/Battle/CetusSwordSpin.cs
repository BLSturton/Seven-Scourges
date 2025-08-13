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

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     spriteRenderer.enabled = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if(battleManager.targetPicked) 
        {
            canSpin = true;
        }
        else 
        {
            canSpin = false;
        }

        if(Input.GetKeyDown(KeyCode.K) && canSpin && !isSpin) 
        {
           isSpin = true;
            StartCoroutine(SwordSpin());
        }
    }

    public IEnumerator SwordSpin() 
    {
        spriteRenderer.enabled = true;
        animator.SetBool("SpinOn", false);
        yield return new WaitForSeconds(.2f);
        animator.SetBool("SpinOn", true);

        float rotationTime = .5f; // Total time for rotation in seconds
        float rotationSpeed = 1080f; // Degrees per second
        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationThisFrame);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until next frame
        }

        animator.SetBool("SpinOn", false);
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(.5f);
        isSpin = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("GoblinRougeAttack"))
        {
            Debug.Log("Aaa");
            Destroy(collision.gameObject);
        }
    }
}
