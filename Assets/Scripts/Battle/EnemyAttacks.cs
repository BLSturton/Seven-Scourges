using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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


       
    }

    public void AttackSelector()
    {
        if (battleManager.enemyTurn)
        {
            enemyAnimator.SetBool("attackOn", true);

        }
        if (attackLead)
            {
                if (this.gameObject.CompareTag("GoblinRouge"))
                {
                    StartCoroutine(TripleKnife());
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

    public void AnimationReturn() 
    {
       
            enemyAnimator.SetBool("attackOn", false);

        

    }
    public IEnumerator TripleKnife() 
    {
        yield return new WaitForSeconds(.4f);

        float knifeSpeed = 5f;
        GameObject knifeMain = Instantiate(dagger, transform.position, Quaternion.identity);
        knifeMain.transform.rotation = Quaternion.Euler(0, 0, -180f);

        float timer = 0f;
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            knifeMain.transform.Translate(Vector2.down * knifeSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        battleManager.enemyTurnEnd = true;

    }
    public IEnumerator AttackOneSupport() 
    {
        Debug.Log("I am also attacking!");
        yield return new WaitForSeconds(3);
       
    }

}
