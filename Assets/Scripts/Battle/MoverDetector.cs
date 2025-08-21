using UnityEngine;

public class MoverDetector : MonoBehaviour
{
    [SerializeField] BoxCollider2D hitBox;
    [SerializeField] public bool hitNow;

    [SerializeField] public Taunt taunt;

    [SerializeField] public bool wordHit;
    [SerializeField] public GameObject touchedWord;

    [SerializeField] FirstAid firstAid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wordHit && Input.GetKeyDown(KeyCode.K)) 
        {
            taunt.wordsHit = taunt.wordsHit + 1;

            Destroy(touchedWord);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        hitNow = true;
        if(collision.CompareTag("WordTaunt")) 
        {
            wordHit = true;
            touchedWord = collision.gameObject;
        }

        if(collision.gameObject == firstAid.lines[0]) 
        {
            firstAid.hitLine = firstAid.lines[0];
        }
        if (collision.gameObject == firstAid.lines[1])
        {
            firstAid.hitLine = firstAid.lines[1];
        }
        if (collision.gameObject == firstAid.lines[2])
        {
            firstAid.hitLine = firstAid.lines[2];
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        hitNow = false;
        if (collision.CompareTag("WordTaunt"))
        {
            wordHit = false;
        }
    }
}
