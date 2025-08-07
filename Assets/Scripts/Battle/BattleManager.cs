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
    }

    // Update is called once per frame
    void Update()
    {
        if (cetusOptionPicker.endTurn) 
        {
            
            if(partyList.Length == 2) 
            {
                raticOptionPicker.myTurn = true;
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
        }
        if (!enemyTurn) 
        {
            playerBackground.SetActive(true);
            playerObjects.SetActive(true);
            enemyBackground.SetActive(false);
            enemyObjects.SetActive(false);
            if (enemyList.Length == 1)
            {

                enemyList[0].transform.position = new Vector2(5f, -.7f);
            }

        }
    }
}
