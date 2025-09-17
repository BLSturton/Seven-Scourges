using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CetusAttack : MonoBehaviour
{
    [SerializeField] GameObject targetBall;
    [SerializeField] GameObject moverBall;
    [SerializeField] GameObject actionBox;

    [SerializeField] Transform[] TargetWayPoints1;
    [SerializeField] Transform[] TargetWayPoints2;

    [SerializeField] Transform[] moverWayPoints;

    [SerializeField] public float moveSpeed;

    [SerializeField] public bool hitFirstTarget;
    [SerializeField] public bool hitSecondTarget;

    [SerializeField] public bool attackFailed;

    [SerializeField] public int nextPoint;

    [SerializeField] EnemyHealth enemyHealth;
    
    [SerializeField] MoverDetector moverDetector;
    [SerializeField] OptionPicker cetusOptionPicker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (nextPoint) 
        {

            case 1:
                moverBall.transform.position = Vector2.MoveTowards(moverBall.transform.position, moverWayPoints[1].transform.position, moveSpeed * Time.deltaTime);
                if(moverBall.transform.position == moverWayPoints[1].transform.position) 
                {
                    if(hitFirstTarget == false) 
                    {
                        nextPoint = 2;

                    }
                    else 
                    {
                        nextPoint = 3;
                    }
                }
                break;
            case 2:
                moverBall.transform.position = Vector2.MoveTowards(moverBall.transform.position, moverWayPoints[2].transform.position, moveSpeed * Time.deltaTime);
                if (moverBall.transform.position == moverWayPoints[2].transform.position)
                {
                    if(hitFirstTarget == false) 
                    {
                        attackFailed = true;
                        EndAttack();
                    }
                    else 
                    {
                        nextPoint = 1;
                    }
                       
                }
                break;
            case 3:
                moverBall.transform.position = Vector2.MoveTowards(moverBall.transform.position, moverWayPoints[0].transform.position, moveSpeed * Time.deltaTime);
                if (moverBall.transform.position == moverWayPoints[0].transform.position) 
                {
                    EndAttack();
                }
                    break;
        }

        if (moverDetector.hitNow && Input.GetKeyDown(KeyCode.K)) 
        {
            if (!hitFirstTarget) 
            {
                moveSpeed = 8;
                nextPoint = 1;
                if (targetBall.transform.position != TargetWayPoints1[0].transform.position) 
                {
                    targetBall.transform.position = TargetWayPoints2[Random.Range(0, 2)].transform.position;

                }
                else 
                {
                    targetBall.transform.position = TargetWayPoints2[Random.Range(1, 2)].transform.position;

                }
                hitFirstTarget = true;
            }
            else 
            {
                hitSecondTarget = true;
                EndAttack();
            }
        }
        if(moverDetector.hitNow == false && Input.GetKeyDown(KeyCode.K)) 
        {
            if (hitFirstTarget) 
            {
                EndAttack();
            }
            else 
            {
                attackFailed = true;
                EndAttack();
            }
        }
    }

    public void StartAttack() 
    {
       
        enemyHealth = cetusOptionPicker.targetEnemy.gameObject.GetComponent<EnemyHealth>();
        moveSpeed = 6;
        targetBall.transform.position = TargetWayPoints1[Random.Range(0, 3)].transform.position;
        moverBall.transform.position = moverWayPoints[0].transform.position;
        nextPoint = 1;
    }
    public void EndAttack() 
    {
        if (attackFailed) 
        {
            Debug.Log("missed");
        }
        if(hitFirstTarget && !hitSecondTarget) 
        {
            Debug.Log("good");
            enemyHealth.health = enemyHealth.health - 1;
            
        }
        if (hitSecondTarget) 
        {
            Debug.Log("Exellent!~");
            enemyHealth.health = enemyHealth.health - 2;


        }
       
            StartCoroutine(attackWait());

        
       
        attackFailed = false;
        actionBox.SetActive(false);
        targetBall.SetActive(false);
        moverBall.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false; 
    }
  
    public IEnumerator attackWait()
    {
        if (hitFirstTarget && !hitSecondTarget)
        {
          
            cetusOptionPicker.animator.SetBool("AttackHalf", true);
            yield return new WaitForSeconds(.2f);
            cetusOptionPicker.animator.SetBool("AttackHalf", false);
        }
        if (hitSecondTarget)
        {
            Debug.Log("Sexellent");
            cetusOptionPicker.animator.SetBool("AttackFull", true);

            yield return new WaitForSeconds(.5f);
            cetusOptionPicker.animator.SetBool("AttackFull", false);

        }
        hitFirstTarget = false;
        hitSecondTarget = false;
        cetusOptionPicker.endTurn = true;
        cetusOptionPicker.myTurn = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        moverBall.SetActive(true);
        targetBall.SetActive(true);
        gameObject.SetActive(false);
    }
}
