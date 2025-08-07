using System.Collections;
using UnityEngine;

public class RougeScript : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] GameObject battleManagerObject;

    [SerializeField] Animator enemyAnimator;

    [SerializeField] public bool myTurn;
    [SerializeField] public bool attackEnded;
    [SerializeField] public bool attackStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManagerObject = GameObject.Find("BattleManager");
        battleManager = battleManagerObject.GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn && !attackStarted) 
        {
            
            enemyAnimator.SetBool("attackOn", true);
            StartCoroutine(AttackOne());
            attackStarted = true;
        }
        if (battleManager.enemyTurn == false) 
        {
            enemyAnimator.SetBool("attackOn", false);
        }
    }

    public IEnumerator AttackOne() 
    {
        Debug.Log("I am attacking you now aaaaaa");
        yield return new WaitForSeconds(3);
        attackEnded = true;
        myTurn = false;
        yield return new WaitForSeconds(.1f);
        attackStarted = false;
        attackEnded =false;
    }
}
