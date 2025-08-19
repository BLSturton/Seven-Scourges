using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public GameObject healthObj;
    [SerializeField] public TextMeshPro hpText;
    [SerializeField] BattleManager battleManager;
    [SerializeField] public bool healthShown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManager = transform.Find("BattleManager").gameObject.GetComponent<BattleManager>();
        switch (this.gameObject.tag) 
        {
            case "GoblinRouge":
                health = 2;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)         
        {
            battleManager.enemyList.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
        hpText.text = health.ToString();
        if (battleManager.diagnosisList.Contains(this.gameObject.tag)) 
        {
            this.gameObject.SetActive(true);
        }
    }
    
    
}
