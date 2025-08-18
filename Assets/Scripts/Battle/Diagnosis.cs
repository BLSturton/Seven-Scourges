using UnityEngine;

public class Diagnosis : MonoBehaviour
{
    [SerializeField] public GameObject[] books;
    [SerializeField] public GameObject questionMark;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack() 
    {
        foreach (GameObject book in books) 
        {
            book.SetActive(true);
        }
        questionMark.SetActive(true);
    }
}
