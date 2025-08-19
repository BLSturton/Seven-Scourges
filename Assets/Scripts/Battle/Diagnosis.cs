using System.Diagnostics;
using UnityEngine;

public class Diagnosis : MonoBehaviour
{
    [SerializeField] public GameObject[] books;
    [SerializeField] public GameObject questionMark;

    [SerializeField] public float bookSpeed1;
    [SerializeField] public float bookSpeed2;
    [SerializeField] public float bookSpeed3;

    [SerializeField] public Transform leftPoint;
    [SerializeField] public Transform rightPoint;

    [SerializeField] public bool book1left;
    [SerializeField] public bool book2left;
    [SerializeField] public bool book3left;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in books)
        {
            if (obj.gameObject == books[0])
            {
                if (!book1left)
                {
                    Vector2 targetPosition = new Vector2(rightPoint.transform.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed1 * Time.deltaTime);
                    if (obj.transform.position.x == rightPoint.transform.position.x)
                    {
                        book1left = true;
                    }
                }
                if (book1left)
                {
                    Vector2 targetPosition = new Vector2(leftPoint.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed1 * Time.deltaTime);
                    if (obj.transform.position.x == leftPoint.transform.position.x)
                    {
                        book1left = false;
                    }
                }
            }
            if (obj.gameObject == books[1])
            {
                if (!book1left)
                {
                    Vector2 targetPosition = new Vector2(rightPoint.transform.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed2 * Time.deltaTime);
                    if (obj.transform.position.x == rightPoint.transform.position.x)
                    {
                        book1left = true;
                    }
                }
                if (book1left)
                {
                    Vector2 targetPosition = new Vector2(leftPoint.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed2 * Time.deltaTime);
                    if (obj.transform.position.x == leftPoint.transform.position.x)
                    {
                        book1left = false;
                    }
                }
            }
            if (obj.gameObject == books[2])
            {
                if (!book1left)
                {
                    Vector2 targetPosition = new Vector2(rightPoint.transform.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed3 * Time.deltaTime);
                    if (obj.transform.position.x == rightPoint.transform.position.x)
                    {
                        book1left = true;
                    }
                }
                if (book1left)
                {
                    Vector2 targetPosition = new Vector2(leftPoint.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed3 * Time.deltaTime);
                    if (obj.transform.position.x == leftPoint.transform.position.x)
                    {
                        book1left = false;
                    }
                }
            }
        }
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
