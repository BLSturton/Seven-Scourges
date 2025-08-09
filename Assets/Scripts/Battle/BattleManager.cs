using System;
using System.Linq;
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
              enemyTurn = true;

            }
        }
        if (raticOptionPicker.endTurn) 
        {
            raticOptionPicker.myTurn = false;
            enemyTurn = true;

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
            switch (enemyList.Length) 
            {
                case 1:
                    if (enemyList[0].CompareTag("GoblinRouge"))
                    {
                        RougeScript rougeScript = enemyList[0].GetComponent<RougeScript>();
                        rougeScript.myTurn = true;
                        if (rougeScript.attackEnded)
                        {
                            rougeScript.myTurn = false;
                            enemyTurn = false;
                            turnReset = true;
                            newPlayerTurn();
                            enemyTurn = false;

                        }
                    }
                    break;
                case 2: 
                    {

                        if (enemyList[0].CompareTag("GoblinRouge"))
                        {
                            RougeScript rougeScript = enemyList[0].GetComponent<RougeScript>();
                            rougeScript.myTurn = true;

                            if (rougeScript.attackEnded)
                            {
                                rougeScript.myTurn = false;
                                if (enemyList[1].CompareTag("GoblinRouge"))
                                {

                                    rougeScript.myTurn = false;
                                    rougeScript.attackEnded = false;
                                    rougeScript.attackStarted = false;

                                    RougeScript rougeScript2 = enemyList[1].GetComponent<RougeScript>();
                                    rougeScript2.myTurn = true;
                                    if (rougeScript2.attackEnded)
                                    {
                                      

                                        rougeScript2.myTurn = false;
                                        enemyTurn = false;
                                        turnReset = true;
                                        rougeScript2.attackEnded = false;
                                        rougeScript2.attackStarted = false;
                                        newPlayerTurn();
                                        enemyTurn = false;

                                    }
                                }

                            }
                        }
                    }
                    break;
            }
           
        }
       
    }

    public void newPlayerTurn() 
    {
        if (turnReset) 
        {
            playerBackground.SetActive(true);
            playerObjects.SetActive(true);
            enemyBackground.SetActive(false);
            enemyObjects.SetActive(false);
            actionBox.SetActive(false);
            targetDodger.SetActive(false);
            foreach (GameObject obj in partyList)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
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
            cetusOptionPicker.enabled = true;
            cetusOptionPicker.myTurn = true;
            cetusOptionPicker.endTurn = false;
            cetusOptionPicker.actionPicked = false;
            cetusOptionPicker.attackOn = false;

            raticOptionPicker.myTurn = false;
            raticOptionPicker.endTurn = false;
            targetPicked = false;
            turnReset = false;
            CetusToRatic = false;
        }
    }
}
