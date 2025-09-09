using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfallFangs : MonoBehaviour
{
    [SerializeField] public List<GameObject> swords;
    [SerializeField] public GameObject goal;
    [SerializeField] public float gravityScale;
    [SerializeField] public GameObject spawn1;
    [SerializeField] public GameObject spawn2;
    [SerializeField] public OptionPicker cetusOptionPicker;
    [SerializeField] public BattleManager battleManager;

    [SerializeField] MoverDetector moverLeft;
    [SerializeField] MoverDetector moverRight;

    [SerializeField] public bool hitLeft;
    [SerializeField] public bool hitRight;
    [SerializeField] public bool missedLeft;
    [SerializeField] public bool missedRight;

    [SerializeField] GameObject target1;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject actionBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        if(moverLeft.hitNow && Input.GetKeyDown(KeyCode.K) && !hitLeft && !missedLeft) 
            {
                hitLeft = true;
                swords.Find(obj => obj.name == "Sword1").SetActive(false);
            }
        if(Input.GetKeyDown(KeyCode.K) && moverLeft.hitNow  == false) 
        {
            missedLeft = true;
        }
        if (moverRight.hitNow && Input.GetKeyDown(KeyCode.L) && !hitRight && !missedRight)
        {
            hitRight = true;
            swords.Find(obj => obj.name == "Sword2").SetActive(false);
            Debug.Log("hot!");
        }
        if (Input.GetKeyDown(KeyCode.L) && moverRight.hitNow == false)
        {
            missedRight = true;
        }

       
    }
    public void StartAttack() 
    {
        hitLeft = false;
        hitRight = false;
        missedLeft = false;
        missedRight = false;
        this.gameObject.SetActive(true);
        foreach (GameObject obj in swords) 
        {
            obj.SetActive(true);

            if (obj == swords[0]) 
            {
                obj.transform.position = spawn1.transform.position;
                
            }
            if (obj == swords[1])
            {
                obj.transform.position = spawn2.transform.position;
            }
            obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        StartCoroutine(bladeDrop());
    }

    public IEnumerator bladeDrop()
    {
       
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            swords[0].GetComponent<Rigidbody2D>().gravityScale = Random.Range(gravityScale, gravityScale + 1f);
            yield return new WaitForSeconds(.2f);
        swords[1].GetComponent<Rigidbody2D>().gravityScale = Random.Range(gravityScale + .5f, gravityScale + 1.5f);
        yield return new WaitForSeconds(1f);
        EndAttack();
    }

    public void EndAttack() 
    {
        if(!hitLeft && !hitRight) 
        {
            Debug.Log("Failed");
        }
        if(hitLeft && !hitRight || hitRight && !hitLeft) 
        {
                target1 = battleManager.enemyList[Random.Range(0, battleManager.enemyList.Count)];
            target1.GetComponent<EnemyHealth>().health = target1.GetComponent<EnemyHealth>().health - 1;
            if (target1.GetComponent<EnemyHealth>().health <= 0)
            {
                battleManager.enemyList.Remove(target1);
            }
            target2 = battleManager.enemyList[Random.Range(0, battleManager.enemyList.Count)];

                target2.GetComponent<EnemyHealth>().health = target2.GetComponent<EnemyHealth>().health - 1;

            
        }
        if(hitLeft && hitRight) 
        {
            target1 = battleManager.enemyList[Random.Range(0, battleManager.enemyList.Count)];
            target1.GetComponent<EnemyHealth>().health = target1.GetComponent<EnemyHealth>().health - 2;
            if (target1.GetComponent<EnemyHealth>().health <= 0) 
            {
                battleManager.enemyList.Remove(target1);
            }
            target2 = battleManager.enemyList[Random.Range(0, battleManager.enemyList.Count)];
            target2.GetComponent<EnemyHealth>().health = target2.GetComponent<EnemyHealth>().health - 2;
        }
        this.gameObject.SetActive(false);
        actionBox.SetActive(false);
        cetusOptionPicker.endTurn = true;
        cetusOptionPicker.myTurn = false;
    }
}
