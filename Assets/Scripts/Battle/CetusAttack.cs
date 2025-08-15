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

    
    [SerializeField] MoverDetector moverDetector;
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
                targetBall.transform.position = TargetWayPoints2[Random.Range(0, 3)].transform.position;
                hitFirstTarget = true;
            }
            else 
            {
                hitSecondTarget = true;
                EndAttack();
            }
        }
    }

    public void StartAttack() 
    {
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
        }
        if (hitSecondTarget) 
        {
            Debug.Log("Exellent!~");
        }
        hitFirstTarget = false;
        hitSecondTarget = false;
        attackFailed = false;
        actionBox.SetActive(false);
        this.gameObject.SetActive(false);
    }
  
}
