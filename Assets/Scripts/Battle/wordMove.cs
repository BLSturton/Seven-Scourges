using TMPro;
using UnityEngine;

public class wordMove : MonoBehaviour
{

    [SerializeField] public bool onLeft;

    [SerializeField] public float wordSpeed = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       if(transform.position.x < 0) 
        {
            onLeft = true;
        }
        else 
        {
            onLeft= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onLeft) 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(5f, transform.position.y), wordSpeed * Time.deltaTime);
             if(transform.position.x >= 5) 
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-5f, transform.position.y), wordSpeed * Time.deltaTime);
            if (transform.position.x <= -5)
            {
                Destroy(gameObject);
            }
        }
    }
 
}
