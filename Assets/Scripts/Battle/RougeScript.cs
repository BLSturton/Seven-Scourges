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
        attackStarted = false;
        Instantiate(dagger, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), new Quaternion(0, 0, 0, 0));

        yield return new WaitForSeconds(3);
        attackEnded = true;
        myTurn = false;
        yield return new WaitForSeconds(.01f);
        attackStarted = false;
        attackEnded =false;
        Debug.Log("Attack over");
    }
}
