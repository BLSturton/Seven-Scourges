
using UnityEngine;

public class Diagnosis : MonoBehaviour
{
    [SerializeField] public GameObject[] books;
    [SerializeField] public GameObject questionMark;
    [SerializeField] public GameObject actionBox;
    [SerializeField] public OptionPicker raticOptionPicker;

    [SerializeField] public float bookSpeed1;
    [SerializeField] public float bookSpeed2;
    [SerializeField] public float bookSpeed3;

    [SerializeField] public Transform leftPoint;
    [SerializeField] public Transform rightPoint;
    [SerializeField] public Transform[] QuestionMarkSpawns;

    [SerializeField] public bool book1left;
    [SerializeField] public bool book2left;
    [SerializeField] public bool book3left;

    [SerializeField] MoverDetector moverDetector;
    [SerializeField] TextScript dialouge;

    [SerializeField] public int booksHit;

    [SerializeField] public GameObject DialogAsset;

    [SerializeField] public bool goAgain;

    [SerializeField] BattleManager battleManager;
    [SerializeField] Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindWithTag("InventorySystem").GetComponent<Inventory>();

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            if (moverDetector.hitNow) 
            {
                booksHit++;
                switch (booksHit) 
                {
                    case 1:
                        books[0].gameObject.SetActive(false);
                        questionMark.transform.position = QuestionMarkSpawns[1].transform.position;
                        break;
                    case 2:
                        books[1].gameObject.SetActive(false);
                        questionMark.transform.position = QuestionMarkSpawns[2].transform.position;
                        break;
                    case 3:
                        books[2].gameObject.SetActive(false);
                        EndAttack();
                        break;
                }
            }
            else 
            {
                EndAttack();
            }
        }
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
                if (!book2left)
                {
                    Vector2 targetPosition = new Vector2(rightPoint.transform.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed2 * Time.deltaTime);
                    if (obj.transform.position.x == rightPoint.transform.position.x)
                    {
                        book2left = true;
                    }
                }
                if (book2left)
                {
                    Vector2 targetPosition = new Vector2(leftPoint.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed2 * Time.deltaTime);
                    if (obj.transform.position.x == leftPoint.transform.position.x)
                    {
                        book2left = false;
                    }
                }
            }
            if (obj.gameObject == books[2])
            {
                if (!book3left)
                {
                    Vector2 targetPosition = new Vector2(rightPoint.transform.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed3 * Time.deltaTime);
                    if (obj.transform.position.x == rightPoint.transform.position.x)
                    {
                        book3left = true;
                    }
                }
                if (book3left)
                {
                    Vector2 targetPosition = new Vector2(leftPoint.position.x, obj.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, bookSpeed3 * Time.deltaTime);
                    if (obj.transform.position.x == leftPoint.transform.position.x)
                    {
                        book3left = false;
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
        questionMark.transform.position = QuestionMarkSpawns[0].transform.position;
    }
    public void EndAttack()
    {
        switch (booksHit) 
        {
            case 0:
                Debug.Log("Failed!");
                break;
            case 1:
                Debug.Log("Failed!");
                break;
            case 2:
                FindEnemy();
                if(battleManager.diagnosisList.Contains(raticOptionPicker.targetEnemy.tag.ToString()) == false)
                {
                    battleManager.diagnosisList.Add(raticOptionPicker.targetEnemy.tag.ToString());

                }
                break;
            case 3:
                FindEnemy();
                if (battleManager.diagnosisList.Contains(raticOptionPicker.targetEnemy.tag.ToString()) == false)
                {
                    battleManager.diagnosisList.Add(raticOptionPicker.targetEnemy.tag.ToString());

                }
                break;
        }
      
    }
    public void FindEnemy() 
    {
        DialogAsset.SetActive(true);
        actionBox.SetActive(false);
        this.gameObject.SetActive(false);
        switch (raticOptionPicker.targetEnemy.tag) 
        {
            case "GoblinRouge":
                dialouge.textName = "GoblinRouge";
                dialouge.startText = true;
                if(inventory.enemyTattles.Contains("GoblinRouge") == false)
                {
                    inventory.enemyTattles.Add("GoblinRouge");  
                }
                    break;
        }
    }

    public void EndAttackForReal() 
    {
        actionBox.SetActive(false);
        this.gameObject.SetActive(false);
        if (booksHit == 3) 
        {
            goAgain = true;
            booksHit = 0;

        }
        else 
        {
            raticOptionPicker.endTurn = true;
            raticOptionPicker.myTurn = false;
            booksHit = 0;

        }

    }
}
