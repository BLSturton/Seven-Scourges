using UnityEngine;
using UnityEngine.UI;
using System.Collections; 

public class Transporter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BoxCollider2D entryBox;
    public GameObject exitBox;
    public GameObject player;

    public PlayerMove playerMove;

    [SerializeField] public CanvasGroup fadeimage;
    void Start()
    {
        fadeimage.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
          
            StartCoroutine(fadeBox());
        }
    }
   public IEnumerator fadeBox()
    {
        playerMove.canMove = false;
        while(fadeimage.alpha < 1) 
        {
            fadeimage.alpha = fadeimage.alpha + .1f;
            yield return new WaitForSeconds(.01f);
        }
        player.transform.position = exitBox.transform.position;
        yield return new WaitForSeconds(1f);
        fadeimage.alpha -= Time.deltaTime;
        while (fadeimage.alpha > 0)
        {
            fadeimage.alpha = fadeimage.alpha - .1f;
            yield return new WaitForSeconds(.05f);
        }
        playerMove.canMove = true;

    }

    
    
}

