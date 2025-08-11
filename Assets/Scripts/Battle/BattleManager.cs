using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyList;
    [SerializeField] public GameObject[] partyList;
    [SerializeField] public GameObject[] partyDodgeList;
    [SerializeField] public GameObject targetDodger;
    //1 For Cetus, 2 for Ratic
    [SerializeField] public int currentPartyTurn;
    [SerializeField] public bool enemyTurn;
   
    [SerializeField] OptionPicker cetusOptionPicker;
    [SerializeField] OptionPicker raticOptionPicker;

    [SerializeField] GameObject playerBackground;
    [SerializeField] GameObject playerObjects;
    [SerializeField] GameObject enemyBackground;
    [SerializeField] GameObject enemyObjects;

    [SerializeField] GameObject actionBox;
    

    [SerializeField] public bool targetPicked;

    [SerializeField] public bool turnReset;
    [SerializeField] public bool CetusToRatic;
 

    [SerializeField] EnemyAttacks enemyAttacks;

    [SerializeField] public bool enemyTurnEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPartyTurn = 1;
        cetusOptionPicker.myTurn = true;

        playerBackground.SetActive(true);
        playerObjects.SetActive(true);
        enemyBackground.SetActive(false);
        enemyObjects.SetActive(false);
        actionBox.SetActive(false);
        foreach (GameObject obj in partyDodgeList)
        {
            if (obj != null)
            {
                obj.SetActive(false); // Deactivates the GameObject
            }
        }
        if (enemyList.Length == 1)
        {
            enemyList[0].transform.position = new Vector2(6.2f, -.36f);
        }
        if (enemyList.Length == 2)
        {
            enemyList[0].transform.position = new Vector2(6.2f, -.36f);
            enemyList[1].transform.position = new Vector2(5.5f, -2.8f);
        }
        if (enemyList.Length == 3)
        {
            enemyList[0].transform.position = new Vector2(4.9f, -.14f);
            enemyList[1].transform.position = new Vector2(6.3f, -1.53f);
            enemyList[2].transform.position = new Vector2(5.3f, -3.7f);

        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (cetusOptionPicker.endTurn) 
        {
            

            if (partyList.Length == 2) 
            {
                if (!CetusToRatic) 
                {
                    raticOptionPicker.myTurn = true;
                    raticOptionPicker.enabled = true;
                    raticOptionPicker.myTurn = true;
                    raticOptionPicker.endTurn = false;
                    raticOptionPicker.actionPicked = false;
                    CetusToRatic = true;
                }
               
            }
            else 
            {
                Debug.Log("True");
              enemyTurn = true;
                cetusOptionPicker.endTurn = false;
            }
        }
        if (raticOptionPicker.endTurn) 
        {
            raticOptionPicker.myTurn = false;
            enemyTurn = true;
            raticOptionPicker.endTurn = false;
        }

        //Starts enemy turn
        if (enemyTurn)
        {
            


            playerBackground.SetActive(false);
            playerObjects.SetActive(false);
            enemyBackground.SetActive(true);
            enemyObjects.SetActive(true);
            if(enemyList.Length == 1) 
            {
                enemyList[0].transform.position = new Vector2(0f, 3.5f);
            }
            if(enemyList.Length == 2) 
            {
                enemyList[0].transform.position = new Vector2(-1.7f, 3.5f);
                enemyList[1].transform.position = new Vector2(1.4f, 3.5f);
            }
            if (enemyList.Length == 3)
            {
                enemyList[0].transform.position = new Vector2(-3f, 3.5f);
                enemyList[1].transform.position = new Vector2(0f, 3.5f);
                enemyList[2].transform.position = new Vector2(1.6f, 3.5f);
            }
            actionBox.SetActive(true);
            foreach (GameObject obj in partyList)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // Deactivates the GameObject
                }
            }
            if (!targetPicked) 
            {

                targetDodger = partyDodgeList[Random.Range(0, partyDodgeList.Length)];
                targetDodger.SetActive(true);
                targetPicked = true;
            }
           if(enemyList.Length == 1) 
            {
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().attackLead = true;
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().AttackSelector();
               
            }
            enemyTurn = false;
        }
        if (enemyTurnEnd) 
        {
            newPlayerTurn();
            if (enemyTurnEnd) 
            {
                cetusOptionPicker.turnOptionsOn();

            }
            enemyTurnEnd = false;

        }
    }

    public void newPlayerTurn() 
    {
        
        if (enemyList.Length == 1)
            {
                enemyList[0].transform.position = new Vector2(6.2f, -.36f);
            }
            if (enemyList.Length == 2)
            {
                enemyList[0].transform.position = new Vector2(6.2f, -.36f);
                enemyList[1].transform.position = new Vector2(5.5f, -2.8f);
            }
            if (enemyList.Length == 3)
            {
                enemyList[0].transform.position = new Vector2(4.9f, -.14f);
                enemyList[1].transform.position = new Vector2(6.3f, -1.53f);
                enemyList[2].transform.position = new Vector2(5.3f, -3.7f);

            }
        playerBackground.SetActive(true);
        playerObjects.SetActive(true);
        enemyBackground.SetActive(false);
        enemyObjects.SetActive(false);
        actionBox.SetActive(false);
        turnReset = false;
        targetPicked = false;

        targetDodger.SetActive(false);
        actionBox.SetActive(false);
        cetusOptionPicker.myTurn = true;
        cetusOptionPicker.actionPicked = false;
        cetusOptionPicker.attackOn = false;

        foreach (GameObject obj in partyList)
        {
            if (obj != null)
            {
                obj.SetActive(true); 
            }
        }
    }
}
