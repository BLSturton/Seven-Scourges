using UnityEngine;

public class RougeScript : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] GameObject battleManagerObject;

    [SerializeField] Animator enemyAnimator;
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
    }
}
