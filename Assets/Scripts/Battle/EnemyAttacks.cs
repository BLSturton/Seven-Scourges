using System.Collections;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] GameObject battleManagerObject;

    [SerializeField] Animator enemyAnimator;

    [SerializeField] public bool myTurn;
    [SerializeField] public bool attackEnded;
    [SerializeField] public bool attackStarted;

    [SerializeField] GameObject dagger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManagerObject = GameObject.Find("BattleManager");
        battleManager = battleManagerObject.GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (battleManager.enemyTurn) 
        {
            enemyAnimator.SetBool("attackOn", true);
        }
        if (myTurn && !attackStarted) 
        {
            attackStarted = true;


            StartCoroutine(AttackOne());
           

        }
        if (battleManager.enemyTurn == false) 
        {
            enemyAnimator.SetBool("attackOn", false);
            attackStarted = false;

        }


    }

    public IEnumerator AttackOne() 
    {
       
        Instantiate(dagger, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), new Quaternion(0, 0, 0, 0));

        yield return new WaitForSeconds(3);
        attackEnded = true;
        myTurn = false;
        
        yield return new WaitForSeconds(.01f);
        
        attackEnded = false;
     
    }
}
