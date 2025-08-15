using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CetusAttack : MonoBehaviour
{
    [SerializeField] GameObject targetBall;
    [SerializeField] GameObject moverBall;

    [SerializeField] Transform[] TargetWayPoints1;
    [SerializeField] Transform[] TargetWayPoints2;

    [SerializeField] Transform[] moverWayPoints;

    [SerializeField] public float moveSpeed;

    [SerializeField] public bool hitFirstTarget;
    [SerializeField] public bool hitSecondTarget;

    [SerializeField] public int nextPoint;
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
                    nextPoint = 2;
                }
                break;
            case 2:
                moverBall.transform.position = Vector2.MoveTowards(moverBall.transform.position, moverWayPoints[2].transform.position, moveSpeed * Time.deltaTime);
                break;
        }

        
    }

    public void StartAttack() 
    {
        targetBall.transform.position = TargetWayPoints1[Random.Range(0, 3)].transform.position;
        moverBall.transform.position = moverWayPoints[0].transform.position;
        nextPoint = 1;
    }

    
}
