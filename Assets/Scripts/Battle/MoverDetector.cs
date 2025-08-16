using UnityEngine;

public class MoverDetector : MonoBehaviour
{
    [SerializeField] BoxCollider2D hitBox;
    [SerializeField] public bool hitNow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        hitNow = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        hitNow = false;
    }
}
