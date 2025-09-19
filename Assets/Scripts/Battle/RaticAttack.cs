using System.Collections;
using UnityEngine;
//using Unity.UI;
using UnityEngine.UI;
public class RaticAttack : MonoBehaviour
{
    [SerializeField] OptionPicker raticOptionPicker;
    [SerializeField] GameObject barOutline;
    [SerializeField] GameObject boneSaw;
    [SerializeField] GameObject[] bones;
    [SerializeField] GameObject[] boneSpawns;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject midPoint;
    [SerializeField] GameObject actionBox;
    [SerializeField] GameObject boneTopPoint;
    [SerializeField] GameObject boneBottomPoint;

    [SerializeField] Text countDown;

    [SerializeField] public float sawSpeed;
    [SerializeField] public float reverseSpeed;
    [SerializeField] public float boneSpeed1;
    [SerializeField] public float boneSpeed2;

    [SerializeField] public bool attackFailed;
    [SerializeField] public bool midWay;
    [SerializeField] public bool fullWay;
    [SerializeField] public bool bone1Bottom;
    [SerializeField] public bool bone2Bottom;

    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] MoverDetector moverDetector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K)) 
        {
            raticOptionPicker.animator.SetBool("AttackGo", true);   
            boneSaw.transform.position = Vector2.MoveTowards(boneSaw.transform.position, endPoint.transform.position, sawSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.K)) 
        {
            raticOptionPicker.animator.SetBool("AttackGo", false);

        }
        else if(boneSaw.transform.position != startPoint.transform.position) 
        {
            boneSaw.transform.position = Vector2.MoveTowards(boneSaw.transform.position, startPoint.transform.position, (reverseSpeed) * Time.deltaTime);

        }
        if(boneSaw.transform.position.x >= midPoint.transform.position.x) 
        {
            midWay = true;
        }
        if(boneSaw.transform.position == endPoint.transform.position) 
        {
            fullWay = true;
            EndAttack();
        }
        foreach (GameObject obj in bones) 
        {
            if(obj.gameObject == bones[0]) 
            {
                if (!bone1Bottom) 
                {
                    Vector2 targetPosition = new Vector2(obj.transform.position.x, boneBottomPoint.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, boneSpeed1 * Time.deltaTime);
                    if(obj.transform.position.y == boneBottomPoint.transform.position.y) 
                    {
                        bone1Bottom = true;
                    }
                }
                if (bone1Bottom)
                {
                    Vector2 targetPosition = new Vector2(obj.transform.position.x, boneTopPoint.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, boneSpeed1 * Time.deltaTime);
                    if (obj.transform.position.y == boneTopPoint.transform.position.y)
                    {
                        bone1Bottom = false;
                    }
                }
            }
            if (obj.gameObject == bones[1])
            {
                if (!bone1Bottom)
                {
                    Vector2 targetPosition = new Vector2(obj.transform.position.x, boneBottomPoint.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, boneSpeed2 * Time.deltaTime);
                    if (obj.transform.position.y == boneBottomPoint.transform.position.y)
                    {
                        bone1Bottom = true;
                    }
                }
                if (bone1Bottom)
                {
                    Vector2 targetPosition = new Vector2(obj.transform.position.x, boneTopPoint.transform.position.y);

                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, boneSpeed2 * Time.deltaTime);
                    if (obj.transform.position.y == boneTopPoint.transform.position.y)
                    {
                        bone1Bottom = false;
                    }
                }
            }
        }
        if (moverDetector.hitNow) 
        {
            if (!midWay) 
            {
                attackFailed = true;
                EndAttack();
            }
            else 
            {  
                EndAttack();
            }
        }
    }

   public void StartAttack() 
    {
        enemyHealth = raticOptionPicker.targetEnemy.gameObject.GetComponent<EnemyHealth>();

        bones[0].transform.position = boneSpawns[Random.Range(0, 2)].transform.position;
        bones[1].transform.position = boneSpawns[Random.Range(3, 5)].transform.position;

        barOutline.SetActive(true);
        boneSaw.SetActive(true);
        countDown.gameObject.SetActive(true);
        countDown.text = "7";
        boneSaw.transform.position = startPoint.transform.position;
        StartCoroutine(CountDown());
    }

    public void EndAttack() 
    {
        raticOptionPicker.animator.SetBool("AttackStart", true);
        raticOptionPicker.animator.SetBool("AttackTransition", true);

        raticOptionPicker.animator.SetBool("AttackStop", true);

        raticOptionPicker.animator.SetBool("AttackStop", false);

        if (attackFailed)
        {
        }
        if (midWay && !fullWay)
        {
            enemyHealth.health = enemyHealth.health - 1;

        }
        if (fullWay)
        {
            enemyHealth.health = enemyHealth.health - 2;

        }
        midWay = false;
        fullWay = false;
        attackFailed = false;
        actionBox.SetActive(false);
        this.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);
        raticOptionPicker.endTurn = true;
        raticOptionPicker.myTurn = false;
    }
    public IEnumerator CountDown() 
    {
        countDown.text = "10";
        yield return new WaitForSeconds(1f);
        countDown.text = "9";
        yield return new WaitForSeconds(1f);
        countDown.text = "8";
        yield return new WaitForSeconds(1f);
        countDown.text = "7";
        yield return new WaitForSeconds(1f);
        countDown.text = "6";
        yield return new WaitForSeconds(1f);
        countDown.text = "5";
        yield return new WaitForSeconds(1f);
        countDown.text = "4";
        yield return new WaitForSeconds(1f);
        countDown.text = "3";
        yield return new WaitForSeconds(1f);
        countDown.text = "2";
        yield return new WaitForSeconds(1f);
        countDown.text = "1";
        yield return new WaitForSeconds(1f);
        countDown.text = "0";
        if (!midWay)
            attackFailed = true;
        EndAttack();
    }
}
