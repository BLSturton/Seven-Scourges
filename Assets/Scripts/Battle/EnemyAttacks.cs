using System.Collections;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] GameObject battleManagerObject;
    [SerializeField] OptionPicker cetusOptionPicker;

    [SerializeField] Animator enemyAnimator;

    [SerializeField] public bool myTurn;
    [SerializeField] public bool attackStarted;

    [SerializeField] GameObject dagger;

    [SerializeField] public bool attackLead;
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
     
        if (battleManager.enemyTurn == false) 
        {
            enemyAnimator.SetBool("attackOn", false);
            attackStarted = false;

        }

       
    }

    public void AttackSelector()
    {
        
            if (attackLead)
            {
                if (this.gameObject.CompareTag("GoblinRouge"))
                {
                    StartCoroutine(AttackOne());
                }
            }
            else
            {
                if (this.gameObject.CompareTag("GoblinRouge"))
                {
                    StartCoroutine(AttackOneSupport());
                }
            }
        
       
    }
    public IEnumerator AttackOne() 
    {

        Debug.Log("I am attacking now!");
        yield return new WaitForSeconds(3);
        battleManager.enemyTurnEnd = true;
        
    }
    public IEnumerator AttackOneSupport() 
    {
        Debug.Log("I am also attacking!");
        yield return new WaitForSeconds(3);
       
    }

}
