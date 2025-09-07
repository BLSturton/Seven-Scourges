using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
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
        cetusOptionPicker = battleManager.cetusOptionPicker;
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
                    StartCoroutine(GoblinRougeSupport());
                }
            }
        
       
    }

    public void AnimationReturn() 
    {
       
            enemyAnimator.SetBool("attackOn", false);

        

    }
    public IEnumerator TripleKnife() 
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.1f);
      
            float knifeSpeed = 5f;
            GameObject knifeMain = Instantiate(dagger, transform.position, Quaternion.identity);

            // Calculate and store direction
            Vector3 directionMain = (battleManager.targetDodger.transform.position - knifeMain.transform.position).normalized;

            // Rotate knife to face player
            knifeMain.transform.right = directionMain;

            GameObject knifeRight = Instantiate(dagger, transform.position, Quaternion.identity);

            Vector3 directionRight = Quaternion.Euler(0, 0, -15f) * (battleManager.targetDodger.transform.position - knifeRight.transform.position).normalized; float timer = 0f;
            knifeRight.transform.right = directionRight;

            GameObject knifeLeft = Instantiate(dagger, transform.position, Quaternion.identity);
            Vector3 directionLeft = Quaternion.Euler(0, 0, 15f) * (battleManager.targetDodger.transform.position - knifeRight.transform.position).normalized; float timer2 = 0f;
            knifeLeft.transform.right = directionLeft;
            if (battleManager.enemyList.Count != 1)
            {
                Destroy(knifeMain);
            }
            while (timer < 2f)
            {
                timer += Time.deltaTime;

                // Move in the calculated direction
                if (battleManager.enemyList.Count == 1) 
                {
                    knifeMain.transform.position += directionMain * Time.deltaTime * knifeSpeed;

                }

                knifeRight.transform.position += (directionRight * Time.deltaTime * knifeSpeed);
                knifeLeft.transform.position += (directionLeft * Time.deltaTime * knifeSpeed);

                yield return null;
            }
           
        }
        battleManager.enemyTurnEnd = true;
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GoblinRougeAttack");

        // Iterate through the array and destroy each GameObject
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
   
    public IEnumerator GoblinRougeSupport() 
    {
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 3; i++) 
        {
            float knifeSpeed = 5f;
            GameObject knifeMain = Instantiate(dagger, transform.position, Quaternion.identity);

            // Calculate and store direction
            Vector3 directionMain = (battleManager.targetDodger.transform.position - knifeMain.transform.position).normalized;
            knifeMain.transform.right = directionMain;

            float timer = 0f;
            while (timer < 2f)
            {
                timer += Time.deltaTime;

                // Move in the calculated direction
                knifeMain.transform.position += directionMain * Time.deltaTime * knifeSpeed;

                yield return null;
            }
        }
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GoblinRougeAttack");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

    }

}


