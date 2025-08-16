using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int health;

    [SerializeField] BattleManager battleManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }

    
}
